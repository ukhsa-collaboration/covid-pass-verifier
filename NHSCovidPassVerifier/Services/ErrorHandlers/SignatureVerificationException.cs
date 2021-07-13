using System;

namespace NHSCovidPassVerifier.Services.ErrorHandlers
{
    public sealed class SignatureVerificationException : Exception
    {
        /// <summary>Initializes a new instance of the
        public SignatureVerificationException()
        {
        }

        /// <summary>Initializes a new instance of the
        /// <param name="message">The parameter <paramref name="message" /> is a
        /// text string.</param>
        public SignatureVerificationException(string message)
            : base(message)
        {
            
        }

        /// <summary>Initializes a new instance of the
        /// message and inner exception.</summary>
        /// <param name="message">The parameter <paramref name="message" /> is a
        /// text string.</param>
        /// <param name="innerException">The parameter <paramref name="innerException" /> is an Exception object.</param>
        public SignatureVerificationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
