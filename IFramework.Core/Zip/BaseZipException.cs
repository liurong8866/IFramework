using System;

namespace IFramework.Core.Zip
{
    /// <summary>
    /// BaseZipException is the base exception class for SharpZipLib.
    /// All library exceptions are derived from this.
    /// </summary>
    /// <remarks>NOTE: Not all exceptions thrown will be derived from this class.
    /// A variety of other exceptions are possible for example <see cref="ArgumentNullException"></see></remarks>
    public class BaseZipException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BaseZipException class.
        /// </summary>
        public BaseZipException() { }

        /// <summary>
        /// Initializes a new instance of the BaseZipException class with a specified error message.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        public BaseZipException(string message)
                : base(message) { }

        /// <summary>
        /// Initializes a new instance of the BaseZipException class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        /// <param name="innerException">The inner exception</param>
        public BaseZipException(string message, Exception innerException)
                : base(message, innerException) { }
    }
}
