using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Services.Repositories;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models.Cose;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services;

namespace NHSCovidPassVerifier.Tests.RepositoryTests
{

    [TestFixture]
    public class QrDecoderServiceTests
    {
        #region Setup
        private readonly IQrDecoderService _qrDecoderService;

        private readonly Mock<IQrValidatorService> _mockQrValidatorService;
        private readonly Mock<ILoggingService> _loggingService;
        private readonly IBase45Service _base45Service;
        private readonly IZLibService _zLibService;

        // First 3 test cases obtained from http://dgc.a-sit.at/ehn/testsuite
        private static readonly IEnumerable<string> InternationalValidQrVaccinationTestCases = new List<string>
        {
            @"HC1:NCFOXN%TS3DH3ZSUZK+.V0ETD%65NL-AH%TAIOOP-I3VSI71GJLWYCI.4+QI6M8SA3/-2E%5VR5VVB9ZILAPIZI.EJJ14B2MZ8DC8COVD9VC/MJK.A+ C/8DXEDM8CC8CHOJXJAUYCOS2QW6%PQRZMPK9I+0MCIKYJRDCDKB%2DFKB YJW CXBDW33+ CD8CQ8C0EC%*TGHD1KT0NDPST7KDQHDN8TSVD2NDB*S6ECMR3:XIBEIVG395EV3EVCK09D5WC.XIR$JU$SB%T7ISCGS2JBZJAZIJFVA.QO5VA81K0ECM8CXVDC8C 1JI7JSTNCA799MC$Q3R69 OZDP*DPK+Q92SYW6MIRNCQIW6B95CAMQ:2OZ6I65M65TWAS4WW4NOXU3$UCPACPI2YUFJ6LX3+KG% BTVB3UQFJ6GL28LHXOAYJAUVPQRHIY1VS1NQ1PRAAUICO10W5X98PF6BSHKDLQKR-M5YGCLVJLLM0-V$RDWNJ%ULP-GXRA3A258E+8O2TPZM96KSCLADLEB$UJ5J3X71A2MGO1QA3MNB-648H$4FMLGQ:EL2UP5M7CCHKG", 
            @"HC1:NCF170WF0/3WUWGVLK7993JB4HBOWEA+NH479CKM603XK2F3JFL-CP2F3J0L CP/IC6TAY50.FK6ZK7:EDOLFVC*70B$D% D3IA4W5646946846.966KCN9E%961A69L6QW6B46XJCCWENF6OF63W5KF60A6WJCT3ETB8WJC0FDU56:KEPH7M/ESDD746IG77TA$96T476:6/Q6M*8CR63Y8R46WX8F46VL6/G8SF6DR64S8+96D7AQ$D.UDRYA 96NF6L/5SW6Y57+EDB.D+Y9V09HM9HC8 QE*KE0ECKQEPD09WEQDD+Q6TW6FA7C46TPCBEC8ZKW.CNWE.Y92OAGY82+8UB8-R7/0A1OA1C9K09UIAW.CE$E7%E7WE KEVKER EB39W4N*6K3/D5$CMPCG/DA8DBB85IAAY8WY8I3DA8D0EC*KE: CZ CO/EZKEZ96446C56GVC*JC1A6NA73W5KF6TF6FBBE00MLMT-B:KJUWQ3/5O%7NL5J64Q18RK8:4G%OJ51N03PFAUG5C1L9-IVJWSQ3VKXKB:O7I7C6II5OTKO9J1 .0J97VEQNG2H1NU-HT2T/8UA8W3FKN-CB$C.2L8A3*F8XVEDD8+NQ3X22X4-L6KDH5%E2OEH$DT1S-R0M:V2$JOR3W+GS92IW6F7F3J9621I3A$*I85M-RLPZU/XD.IR4-HESN-UQMGKZ.AW4HWO4J6CN28MVL:+QK065-2KKB1H3YZBEJSR804W7N:K7GRQNB*WAONCTJB-2E:EL20C+SP.37F.HFOJV9PHZ1+30I717MRZ8NL-OCQJU1J*-H/4EOK60TRSED DQ0027CMINBR-DSP2*10F:A1UP-5QBO5U:3UCFI3", 
            @"HC1:NCF*90*C0/3WUWGVLK*49CYLZQO3DB100H479CKM603XK2F3JFL-CP2F3J0L CP/IC6TAY50.FK6ZK7:EDOLFVC*70B$D% D3IA4W5646946846.966KCN9E%961A69L6QW6B46XJCCWENF6OF63W5KF60A6WJCT3ETB8WJC0FDU56:KEPH7M/ESDD746IG77TA$96T476:6/Q6M*8CR63Y8R46WX8F46VL6/G8SF6DR64S8+96D7AQ$D.UDRYA 96NF6L/5SW6Y57+EDB.D+Y9V09HM9HC8 QE*KE0ECKQEPD09WEQDD+Q6TW6FA7C46TPCBEC8ZKW.CNWE.Y92OAGY82+8UB8-R7/0A1OA1C9K09UIAW.CE$E7%E7WE KEVKER EB39W4N*6K3/D5$CMPCG/DA8DBB85IAAY8WY8I3DA8D0EC*KE: CZ CO/EZKEZ96446C56GVC*JC1A6NA73W5KF6TF6FBB7BG:A1AI2N.U T1VJA44BJ*EZ$JMR5ZZU7*S479BM2UDBWLJ%E81GWM%T+.2FEBWUG .N:FRC6C232WMLBTLF2I-45ZM72HI/3M83A%477PFBJT%EK- N$BFTN4IFDAE8.D1Q1O%H4GMEG4B*ACJ-B+80O0J$0V%BPHWA:5PLFOOX9SXC36R32OO QYC4J-1TGHXZORV8D+R2IV*JP3BND%L-/C+3OBQTSEEC*MH7QCY5M359TVTSFR 2Q98:K940L$CL4MCBKP/Z2D+CROQOTL42PR47PEGUJ4S*HAPPF T$H5V%1SYFW/0.KTQ8B05A+ZHA*C$KC3JN:.8XY5:CHM0HFYQ/EB NRQRJ21RNWC00J:*9U-58LUJVUW F1W7JOFOVPZ*PEC3R8AU6C:C9K.BSARI$8MG7ZFQP11VYLACW3*A%2NYU0NDWBFBXOQ/O49+7GJJRY8K*G5+DL.C:ISHLH8 J *2MT7KP1MWS+CK9-P%V9/707V6M31SZMHAF$.FG$G64T9.0.P4 PGY/DKU6O6SFZQ2EP7-FK.H/+P0YT5SAU49E0D DRJULL97UXRAIN:0", 
            @"HC1:NCFT604G0A2WF.9.60+O4W79F%GZM9NTVO KFBBJR2*70NS8FN0O9C055WY0K9CJP6D97TK0H90GECFAGYWEKECTHGWJC0FDRX5:KEPH7.$E8UCMA7JOC1UC:R64W5407$CC2S6F-C7X5Z57C-CJ-C/TC5:6GPCRW6G-CWJCT3ENS8XJC$+DXJCCWENF6OF63W5$Q6OF6%JC+QE P3CEC+EDG7D%69AECAWEP34IECOCCHC8AWE04E-EDOFFB$D% D3IA4W5646946%96X47.JCP9EJY8L/5M/5546.96D463KC.SC4KCD3DX47B46IL6646H*6Z/E5JD%96IA74R6646407GVC*JC1A6 473W5NW6A46TPCBEC7ZKW.CZ-C04EAWEY CI3DZ-CZKEA4FJQEW.C8WERS8GIA669MPCG/DZ-C1IAL1BHOAO/EZKEZ96446256V50+$KJL24Y5GPC9 P2E0.2EVGAHQ3KIG /HI6KV3A1U5$NUUWNRAV M4Q3AYAR1BIAOGIU7-SF5 HTAUE5O-.GIBHTPJG7UULB/07GB5OUPK K+ER268D0VL%U.FLFZ6+KO2JRIZPQ/11O1K88U.FWJTLQ8X+HSLKPXOIFWV$ILGQ1-L.MK3K5$BQK MDXJACE22PR5R9GF-5UB35QRN$25OI1-9FNAC.JAU$V0413EN$*E:2MDF290NC0EC:AZ%5VMA4VA76OLSSC9QWRAZM7NUCU15*W1/GA$22M+PPEJSML2IQ7%OQANO76NKBRGR4 BT11K3MC4E6D8T80N6V28Q2QB5E9/QQN8HV974AA.MOL4B/L0%I5ZJN*7T-S0676P716US", 
        };

        // First test case obtained from http://dgc.a-sit.at/ehn/qrc/recovery
        private static readonly IEnumerable<string> InternationalValidQrRecoveryTestCases = new List<string>
            {
                @"HC1:NCFOXN%TS3DH3ZSUZK+.V0ETD%65NL-AH5*SIOOP-IOQP/Y68WA VUGSL+QI6M8SA3/-2E%5UR5+VBZXIFNJ1VCSWC1QDRCKH9CJZIS/1VY9X5Q/36MZ5BTMUW5-5QNF6O M9R1VNM8.MM.EM M%/EJ.E%+MXM6Q*ER56CL6F8E4IMI%6NJM3VE$%H6PPL4Q0RUS-EBHU68E1W5XC52T9PF5RBQ746B46O1N646EN9NM50GRZ9C.PDMCIWKI71T 10SKE MCTPI8%MIMIBDSUES$8RZ6N*8PBL3C7GKGS$0AY6F*DK8%MRIAKDK/HL*DD2IHJSN37HMX3.7KO7JDKB:ZJ83BDPSCFTB.SBVTHOJ92KNNSQBJGZIGOJ6NJF0JEYI1DLNCKUCI5OI9YI:8DGCD75NST5VD8I:DRMURC7*DT 6V:KE34EE/HE9QLM1.$F2ISPPO6WL+16G2Q36H141%4V:-QD5JL/R0NQ:MOAVQ%6FQ+KV5UL2O+*BSZC767/R9250+AGT5",
            };

        private static readonly IEnumerable<string> InternationalValidQrTestCases = 
            InternationalValidQrVaccinationTestCases.Union(InternationalValidQrRecoveryTestCases);


        public QrDecoderServiceTests()
        {
            _mockQrValidatorService = new Mock<IQrValidatorService>();
            _loggingService = new Mock<ILoggingService>();
            _base45Service = new Base45Service();
            _zLibService = new ZLibService();

            _qrDecoderService = new QrDecoderService(
                _mockQrValidatorService.Object, 
                _loggingService.Object, 
                _base45Service, 
                _zLibService);
        }

        #endregion

        #region GenerateDomesticCertificateTests
        [TestCase("firstname lastname", "01/21/2021 14:00:00")]
        [TestCase("long name                                                 end of long name", "01/21/2015 14:00:00")]
        public void ShouldGenerateValidCertificate(string name, DateTime expirationDT)
        {
            _mockQrValidatorService.Setup(x => x.ValidateQrCodeForDomesticCertificate(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>())).ReturnsAsync(true);
            var expirationString = expirationDT.ToString("yyMMddHHmm");
            if (name.Length > 60)
            {
                name = name.Substring(0, 60);
            }
            var throwaway = Base64UrlEncoder.Encode("0");
            var payload = $"{throwaway}.{Base64UrlEncoder.Encode("1" + expirationString + name)}.{throwaway}";
            _qrDecoderService.ValidateQrCode(payload);

            var expected = new DomesticCertificate()
            {
                Expiry = expirationDT,
                FullName = name
            };

            var actual = (DomesticCertificate) _qrDecoderService.GenerateCertificateFromQrCode();

            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(expected.Expiry, actual.Expiry);
        }

        [TestCase("firstname lastname")]
        [TestCase("2101011200firstname lastname")]
        [TestCase("1firstname lastname2101011200")]
        [TestCase("1210101firstname lastname")]
        [TestCase("12101011200")]
        public async Task IncorrectQrShouldReturnDefaultCertificate(string qr)
        {
            _mockQrValidatorService.Setup(x => x.ValidateQrCodeForDomesticCertificate(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>())).ReturnsAsync(true);

            var throwaway = Base64UrlEncoder.Encode("0");
            var payload = $"{throwaway}.{Base64UrlEncoder.Encode(qr)}.{throwaway}";
            await _qrDecoderService.ValidateQrCode(payload);

            DomesticCertificate expected = default;
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();

            Assert.AreEqual(expected, actual);
        }

        [TestCase("firstname lastname")]
        [TestCase("12101011200firstname lastname")]
        public async Task InvalidSignatureShouldReturnDefaultCertificate(string qr)
        {
            _mockQrValidatorService.Setup(x => x.ValidateQrCodeForDomesticCertificate(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<string>())).ReturnsAsync(false);

            var throwaway = Base64UrlEncoder.Encode("0");
            var payload = $"{throwaway}.{Base64UrlEncoder.Encode(qr)}.{throwaway}";
            await _qrDecoderService.ValidateQrCode(payload);

            DomesticCertificate expected = default;
            var actual = (DomesticCertificate)_qrDecoderService.GenerateCertificateFromQrCode();

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region ValidateDomesticQrCodeTests
        [TestCase("123456")]
        [TestCase("1.2")]
        [TestCase("1.2.3.4")]
        public async Task InvalidFormatShouldReturnFalseValidation(string qr)
        {
            var actual = await _qrDecoderService.ValidateQrCode(qr);

            Assert.False(actual);
        }
        [TestCase("MQ.MTIxMDEyMTEwMTJEb21pbmljIFJhd2xpbnM.2qNUk6lN09BLxABXcBHoXTtfiBOaTRFmcUJAxy-350Q8QdFeppa4AL-h3ZaZqaKDNW-4SbugRj6F6CzGjL0ziQ")]
        [TestCase("SS.dsa873ADSW7sds.2qNUk6lN09BLxABXcBHoXTtfiBOaTRFmcUJdsjsaddsa-SDHjsdasaiziQ")]
        public async Task ValidQrShouldBePassedToValidatorCorrectly(string validQr)
        {
            var split = validQr.Split(".");

            var hasher = new SHA256CryptoServiceProvider();
            var digest = hasher.ComputeHash(Encoding.Unicode.GetBytes($"{split[0]}.{split[1]}"));
            var header = split[0];
            var sigBytes = Base64UrlEncoder.DecodeBytes(split[2]);

            _mockQrValidatorService.Setup(x => x.ValidateQrCodeForDomesticCertificate(digest, sigBytes, header)).ReturnsAsync(true);

            var actual = await _qrDecoderService.ValidateQrCode(validQr);

            Assert.IsTrue(actual);
        }
        #endregion

        #region InternationalQrTests
        
        [TestCaseSource(nameof(InternationalValidQrTestCases))]
        public void ValidQrStringReturnsValidCoseObject(string qrString)
        {
            var base45String = qrString.Substring(4);
            var func = new TestDelegate(() => _qrDecoderService.DecodeToCose(base45String));
            Assert.DoesNotThrow(func);
        }
        
        [TestCaseSource(nameof(InternationalValidQrTestCases))]
        public async Task ValidQrStringSuccessfullyValidates(string qrString)
        {
            var actual = await _qrDecoderService.ValidateQrCode(qrString);
            Assert.IsTrue(actual);
        }

        [TestCaseSource(nameof(InternationalValidQrTestCases))]
        public async Task ValidQrStringContainsRequiredData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var certificateType = actual.GetCertificateType();
            Assert.AreEqual(CertificateType.International, certificateType);
            var actualInternationalCertificate = actual as InternationalCertificate;
            Assert.IsNotNull(actualInternationalCertificate);

            var actualDecodedModel = actualInternationalCertificate.DecodedModel;
            Assert.IsNotNull(actualDecodedModel);
            Assert.IsNotNull(actualDecodedModel.iat);
            Assert.IsNotNull(actualDecodedModel.exp);

            var actualHCertModel = actualDecodedModel.hcert;
            Assert.IsNotNull(actualHCertModel);

            var actualEuHCertSchema = actualHCertModel.euHcertV1Schema;
            Assert.IsNotNull(actualEuHCertSchema);
            Assert.IsNotNull(actualEuHCertSchema.Version);
            Assert.IsNotNull(actualEuHCertSchema.InternationalCertificateSubject);
            Assert.IsNotNull(actualEuHCertSchema.DateOfBirth);

            var actualSubject = actualEuHCertSchema.InternationalCertificateSubject;
            Assert.IsNotNull(actualSubject);
            Assert.IsNotNull(actualSubject.FamilyNameTransliterated);
        }
        
        [TestCaseSource(nameof(InternationalValidQrTestCases))]
        public async Task ValidQrStringCorrectlyEncodesRequiredData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var certificateType = actual.GetCertificateType();
            Assert.AreEqual(CertificateType.International, certificateType);
            var actualInternationalCertificate = actual as InternationalCertificate;
            Assert.IsNotNull(actualInternationalCertificate);

            var actualDecodedModel = actualInternationalCertificate.DecodedModel;
            Assert.IsNotNull(actualDecodedModel);
            Assert.Positive(actualDecodedModel.iat);
            Assert.Positive(actualDecodedModel.exp);

            var actualEuHCertSchema = actualDecodedModel.hcert.euHcertV1Schema;
            Assert.That(actualEuHCertSchema.Version, Does.Match("^\\d+.\\d+.\\d+$"));
            Assert.IsNotNull(actualEuHCertSchema.InternationalCertificateSubject);

            var actualSubject = actualEuHCertSchema.InternationalCertificateSubject;
            Assert.IsNotNull(actualSubject);
            Assert.That(actualSubject.FamilyNameTransliterated, Does.Match("^[A-Z<]{0,50}$"));
        }

        [TestCaseSource(nameof(InternationalValidQrTestCases))]
        public async Task ValidQrStringCorrectlyEncodesOptionalData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var certificateType = actual.GetCertificateType();
            Assert.AreEqual(CertificateType.International, certificateType);
            var actualInternationalCertificate = actual as InternationalCertificate;
            Assert.IsNotNull(actualInternationalCertificate);

            var actualDecodedModel = actualInternationalCertificate.DecodedModel;
            Assert.That(actualDecodedModel.iss, Is.Empty.Or.Match("^[a-zA-Z]{2}$"));

            var actualHCertModel = actualDecodedModel.hcert;

            var actualEuHCertSchema = actualHCertModel.euHcertV1Schema;

            var actualSubject = actualEuHCertSchema.InternationalCertificateSubject;
            Assert.IsNotNull(actualSubject);
            Assert.IsTrue(actualSubject.GivenName.Length <= 50);
            Assert.That(actualSubject.GivenNameTransliterated, Does.Match("^[A-Z<]{0,50}$"));
            Assert.IsTrue(actualSubject.FamilyName.Length <= 50);
        }

        [TestCaseSource(nameof(InternationalValidQrVaccinationTestCases))]
        public async Task ValidQrVaccinationStringContainsRequiredVaccinationData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var actualEuHCertSchema = ((InternationalCertificate) actual).DecodedModel.hcert.euHcertV1Schema;
            Assert.IsNotNull(actualEuHCertSchema);
            Assert.IsNotNull(actualEuHCertSchema.Vaccinations);
            Assert.That(actualEuHCertSchema.Vaccinations, Is.Null.Or.Not.Empty);

            foreach (var vaccination in actualEuHCertSchema.Vaccinations.DefaultIfEmpty())
            {
                Assert.IsNotNull(vaccination);
                Assert.IsNotNull(vaccination.CertificateId);
                Assert.IsNotNull(vaccination.ProductCode);
                Assert.IsNotNull(vaccination.Manufacturer);
                Assert.IsNotNull(vaccination.DoseNumber);
                Assert.IsNotNull(vaccination.TotalNumberOfDose);
                Assert.IsNotNull(vaccination.DateOfVaccination);
                Assert.IsNotNull(vaccination.Country);
                Assert.IsNotNull(vaccination.CertificateIssuer);
                Assert.IsNotNull(vaccination.DiseaseTargeted);
                Assert.IsNotNull(vaccination.VaccineTypeCode);
            }
        }
        
        [TestCaseSource(nameof(InternationalValidQrVaccinationTestCases))]
        public async Task ValidQrVaccinationStringCorrectlyEncodesVaccinationData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var actualEuHCertSchema = ((InternationalCertificate) actual).DecodedModel.hcert.euHcertV1Schema;
            Assert.IsNotNull(actualEuHCertSchema);
            Assert.IsNotNull(actualEuHCertSchema.Vaccinations);
            Assert.That(actualEuHCertSchema.Vaccinations, Is.Null.Or.Not.Empty);

            foreach (var vaccination in actualEuHCertSchema.Vaccinations.DefaultIfEmpty())
            {
                Assert.Positive(vaccination.DoseNumber);
                Assert.Positive(vaccination.TotalNumberOfDose);
                Assert.LessOrEqual(vaccination.DateOfVaccination, DateTime.UtcNow);
                Assert.That(vaccination.Country, Does.Match("^[A-Z]{1,10}$"));
                Assert.IsTrue(vaccination.CertificateIssuer.Length <= 50);
                Assert.IsTrue(vaccination.CertificateId.Length <= 50);
            }
        }

        [TestCaseSource(nameof(InternationalValidQrRecoveryTestCases))]
        public async Task ValidQrRecoveryStringContainsRequiredRecoveryData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var actualEuHCertSchema = ((InternationalCertificate) actual).DecodedModel.hcert.euHcertV1Schema;
            Assert.IsNotNull(actualEuHCertSchema);
            Assert.IsNotNull(actualEuHCertSchema.Recovery);
            Assert.That(actualEuHCertSchema.Recovery, Is.Null.Or.Not.Empty);

            foreach (var recovery in actualEuHCertSchema.Recovery.DefaultIfEmpty())
            {
                Assert.IsNotNull(recovery);
                Assert.IsNotNull(recovery.DiseaseTargeted);
                Assert.IsNotNull(recovery.DateOfFirstPositiveResult);
                Assert.IsNotNull(recovery.CountryOfTest);
                Assert.IsNotNull(recovery.CertificateIssuer);
                Assert.IsNotNull(recovery.DateValidFrom);
                Assert.IsNotNull(recovery.DateValidUntil);
                Assert.IsNotNull(recovery.CertificateId);
            }
        }
        
        [TestCaseSource(nameof(InternationalValidQrRecoveryTestCases))]
        public async Task ValidQrRecoveryStringCorrectlyEncodesRecoveryData(string qrString)
        {
            // Given
            _mockQrValidatorService
                .Setup(x => x.ValidateQrCodeForInternationalCertificate(It.IsAny<CoseSign1Object>()))
                .ReturnsAsync(true);
            
            // When
            await _qrDecoderService.ValidateQrCode(qrString);
            var actual = _qrDecoderService.GenerateCertificateFromQrCode();
            
            // Then
            var actualEuHCertSchema = ((InternationalCertificate) actual).DecodedModel.hcert.euHcertV1Schema;
            Assert.IsNotNull(actualEuHCertSchema);
            Assert.IsNotNull(actualEuHCertSchema.Recovery);
            Assert.That(actualEuHCertSchema.Recovery, Is.Null.Or.Not.Empty);

            foreach (var recovery in actualEuHCertSchema.Recovery.DefaultIfEmpty())
            {
                Assert.LessOrEqual(recovery.DateOfFirstPositiveResult, DateTime.UtcNow);
                Assert.That(recovery.CountryOfTest, Does.Match("^[A-Z]{1,10}$"));
                Assert.IsTrue(recovery.CertificateIssuer.Length <= 50);
                Assert.LessOrEqual(recovery.DateValidFrom, recovery.DateValidUntil);
                Assert.IsTrue(recovery.CertificateId.Length <= 50);
            }
        }

        #endregion
    }
}
