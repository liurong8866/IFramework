using System;

namespace IFramework.Core.Zip.GZip
{
    /// <summary>
    /// GZipException represents exceptions specific to GZip classes and code.
    /// </summary>
    public class GZipException : BaseZipException
    {
        /// <summary>
        /// Initialise a new instance of <see cref="GZipException" />.
        /// </summary>
        public GZipException() { }

        /// <summary>
        /// Initialise a new instance of <see cref="GZipException" /> with its message string.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the error.</param>
        public GZipException(string message) : base(message) { }

        /// <summary>
        /// Initialise a new instance of <see cref="GZipException" />.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that caused this exception.</param>
        public GZipException(string message, Exception innerException) : base(message, innerException) { }
    }
}
