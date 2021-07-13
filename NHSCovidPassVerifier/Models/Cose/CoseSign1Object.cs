using System;
using System.Collections.Generic;
using System.Diagnostics;
using NHSCovidPassVerifier.Services.ErrorHandlers;
using NHSCovidPassVerifier.Services.Utils;
using Org.BouncyCastle.Security;
using PeterO.Cbor;

///MIT License
///Copyright 2021 Myndigheten f�r digital f�rvaltning (DIGG)
namespace NHSCovidPassVerifier.Models.Cose
{
    ///@author Henrik Bengtsson(extern.henrik.bengtsson @digg.se)
    ///@author Martin Lindstr�m(martin @idsec.se)
    ///@author Henric Norlander(extern.henric.norlander @digg.se)
    /// <summary>
    ///A representation of a COSE_Sign1 object.
    /// </summary>
    public class CoseSign1Object
    {
        public static int MESSAGE_TAG = 18;
        private bool includeMessageTag = false;
        private CBORObject protectedAttributes;
        private byte[] protectedAttributesEncoding;
        private CBORObject unprotectedAttributes;
        private byte[] content;
        private byte[] signature;
        private static byte[] externalData = new byte[0];
        private static string contextString = "Signature1";

        public CoseSign1Object()
        {
            protectedAttributes = CBORObject.NewMap();
            unprotectedAttributes = CBORObject.NewMap();
        }
        /// <summary>
        /// Constructor that accepts the binary representation of a signed COSE_Sign1 object. throws CBORException for invalid data
        /// </summary>
        /// <param name="data"> the binary representation of the COSE_Sign1 object</param>
        public CoseSign1Object(byte[] data)
        {
            var message = CBORObject.DecodeFromBytes(data);
            if (message.Type != CBORType.Array)
            {
                throw new CBORException("Supplied message is not a valid COSE security object");
            }

            if (message.IsTagged) {
                if (message.GetAllTags().Length != 1) {
                    throw new CBORException("Invalid object - too many tags");
                }
                if (MESSAGE_TAG != message.MostInnerTag.ToInt32Unchecked())
                {
                    throw new CBORException(
                        $"Invalid COSE_Sign1 structure - Expected {MESSAGE_TAG} tag - but was {message.MostInnerTag.ToInt32Unchecked()}");
                }
            }

            if (message.Count != 4)
            {
                throw new CBORException(
                    $"Invalid COSE_Sign1 structure - Expected an array of 4 items - but array has {message.Count} items");
            }
            if (message[0].Type == CBORType.ByteString)
            {
                this.protectedAttributesEncoding = message[0].GetByteString();

                if (message[0].GetByteString().Length == 0)
                {
                    this.protectedAttributes = CBORObject.NewMap();
                }
                else
                {
                    this.protectedAttributes = CBORObject.DecodeFromBytes(this.protectedAttributesEncoding);
                    if (this.protectedAttributes.Count == 0)
                    {
                        this.protectedAttributesEncoding = new byte[0];
                    }
                }
            }
            else
            {
                throw new CBORException(
                    $"Invalid COSE_Sign1 structure -  Expected item at position 1/4 to be a bstr which is the encoding of the protected attributes, but was {message[0].Type}");
            }

            if (message[1].Type == CBORType.Map || message[1].Type == CBORType.ByteString)
            {
                this.unprotectedAttributes = message[1];
            }
            else
            {
                throw new CBORException(
                    $"Invalid COSE_Sign1 structure - Expected item at position 2/4 to be a Map for unprotected attributes, but was {message[1].Type}");
            }

            if (message[2].Type == CBORType.ByteString)
            {
                this.content = message[2].GetByteString();
            }
            else if (!message[2].IsNull)
            {
                throw new CBORException(
                    $"Invalid COSE_Sign1 structure - Expected item at position 3/4 to be a bstr holding the payload, but was {message[2].Type}");
            }

            if (message[3].Type == CBORType.ByteString)
            {
                this.signature = message[3].GetByteString();
            }
            else
            {
                throw new CBORException(
                    $"Invalid COSE_Sign1 structure - Expected item at position 4/4 to be a bstr holding the signature, but was {message[3].Type}");
            }
        }
        public static CoseSign1Object Decode(byte[] data)
        {
            return new CoseSign1Object(data);
        }

        public byte[] GetKeyIdentifier()
        {
            var kid = protectedAttributes[HeaderParameterKey.KID] ?? unprotectedAttributes[HeaderParameterKey.KID];

            return kid?.GetByteString();
        }

        public string GetJson()
        {
            return content == null ? null : CborUtils.ToJson(content);
        }
        public void VerifySignature(byte[] publicKey)
        {
            if (signature == null) {
                throw new SignatureVerificationException("Object is not signed");
            }

            var obj = CBORObject.NewArray();
            obj.Add(contextString);
            obj.Add(this.protectedAttributesEncoding);
            obj.Add(externalData);
            if (this.content != null) {
                obj.Add(this.content);
            }
            else {
                obj.Add(null);
            }

            var signedData = obj.EncodeToBytes();

            var registeredAlgorithm = protectedAttributes[HeaderParameterKey.ALGORITHIM];
            if (registeredAlgorithm == null)
            {
                throw new SignatureVerificationException("/ Protected / {[]} No alg found");
            }
            
            var signatureToVerify = signature;
            if (!SignatureAlgorithm.IsSupportedAlgorithm(registeredAlgorithm))
            {
                throw new SignatureVerificationException(
                    $" / Protected / { SignatureAlgorithm.GetAlgorithmName(registeredAlgorithm)} not supported.");
            }

            if (registeredAlgorithm == SignatureAlgorithm.ES256
                || registeredAlgorithm == SignatureAlgorithm.ES384
                || registeredAlgorithm == SignatureAlgorithm.ES512)
            {

                signatureToVerify = ConvertToDer(this.signature);
            }

            var key = PublicKeyFactory.CreateKey(publicKey);

            var verifier = SignerUtilities.GetSigner(SignatureAlgorithm.GetAlgorithmName(registeredAlgorithm));
            verifier.Init(false, key);
            verifier.BlockUpdate(signedData, 0, signedData.Length);
            var result = verifier.VerifySignature(signatureToVerify);

            if (!result)
            {
                throw new SignatureVerificationException($"/signature/ : Invalid {Convert.ToBase64String(signatureToVerify)}");
            }
        }

        /// <summary>
        /// Given a signature according to section 8.1 in RFC8152 its corresponding DER encoding is returned.
        /// </summary>
        /// <param name="rsConcat">the ECDSA signature</param>
        /// <returns>DER-encoded signature</returns>
        private static byte[] ConvertToDer(byte[] rsConcat)
        {
            var len = rsConcat.Length / 2;
            var r = new byte[len];
            var s = new byte[len];
            Array.Copy(rsConcat, r, len);
            Array.Copy(rsConcat, len, s, 0, len);

            var seq = new List<byte[]> {Asn1Utils.ToUnsignedInteger(r), Asn1Utils.ToUnsignedInteger(s)};

            return Asn1Utils.ToSequence(seq);
        }

    }
}