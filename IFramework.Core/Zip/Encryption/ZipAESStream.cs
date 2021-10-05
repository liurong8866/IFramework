using System;
using System.IO;
using System.Security.Cryptography;

namespace IFramework.Core.Zip.Encryption
{
    /// <summary>
    /// Encrypts and decrypts AES ZIP
    /// </summary>
    /// <remarks>
    /// Based on information from http://www.winzip.com/aes_info.htm
    /// and http://www.gladman.me.uk/cryptography_technology/fileencrypt/
    /// </remarks>
    internal class ZipAesStream : CryptoStream
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
        /// <param name="transform">Instance of ZipAESTransform</param>
        /// <param name="mode">Read or Write</param>
        public ZipAesStream(Stream stream, ZipAesTransform transform, CryptoStreamMode mode) : base(stream, transform, mode)
        {
            this.stream = stream;
            this.transform = transform;
            slideBuffer = new byte[1024];
            blockAndAuth = CRYPTO_BLOCK_SIZE + AUTH_CODE_LENGTH;

            // mode:
            //  CryptoStreamMode.Read means we read from "stream" and pass decrypted to our Read() method.
            //  Write bypasses this stream and uses the Transform directly.
            if (mode != CryptoStreamMode.Read) { throw new Exception("ZipAESStream only for read"); }
        }

        // The final n bytes of the AES stream contain the Auth Code.
        private const int AUTH_CODE_LENGTH = 10;

        private readonly Stream stream;
        private readonly ZipAesTransform transform;
        private readonly byte[] slideBuffer;
        private int slideBufStartPos;

        private int slideBufFreePos;

        // Blocksize is always 16 here, even for AES-256 which has transform.InputBlockSize of 32.
        private const int CRYPTO_BLOCK_SIZE = 16;
        private readonly int blockAndAuth;

        /// <summary>
        /// Reads a sequence of bytes from the current CryptoStream into buffer,
        /// and advances the position within the stream by the number of bytes read.
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int nBytes = 0;

            while (nBytes < count) {
                // Calculate buffer quantities vs read-ahead size, and check for sufficient free space
                int byteCount = slideBufFreePos - slideBufStartPos;

                // Need to handle final block and Auth Code specially, but don't know total data length.
                // Maintain a read-ahead equal to the length of (crypto block + Auth Code). 
                // When that runs out we can detect these final sections.
                int lengthToRead = blockAndAuth - byteCount;

                if (slideBuffer.Length - slideBufFreePos < lengthToRead) {
                    // Shift the data to the beginning of the buffer
                    int iTo = 0;

                    for (int iFrom = slideBufStartPos; iFrom < slideBufFreePos; iFrom++, iTo++) { slideBuffer[iTo] = slideBuffer[iFrom]; }
                    slideBufFreePos -= slideBufStartPos; // Note the -=
                    slideBufStartPos = 0;
                }
                int obtained = stream.Read(slideBuffer, slideBufFreePos, lengthToRead);
                slideBufFreePos += obtained;

                // Recalculate how much data we now have
                byteCount = slideBufFreePos - slideBufStartPos;

                if (byteCount >= blockAndAuth) {
                    // At least a 16 byte block and an auth code remains.
                    transform.TransformBlock(slideBuffer, slideBufStartPos, CRYPTO_BLOCK_SIZE, buffer, offset);
                    nBytes += CRYPTO_BLOCK_SIZE;
                    offset += CRYPTO_BLOCK_SIZE;
                    slideBufStartPos += CRYPTO_BLOCK_SIZE;
                } else {
                    // Last round.
                    if (byteCount > AUTH_CODE_LENGTH) {
                        // At least one byte of data plus auth code
                        int finalBlock = byteCount - AUTH_CODE_LENGTH;
                        transform.TransformBlock(slideBuffer, slideBufStartPos, finalBlock, buffer, offset);
                        nBytes += finalBlock;
                        slideBufStartPos += finalBlock;
                    } else if (byteCount < AUTH_CODE_LENGTH) throw new Exception("Internal error missed auth code"); // Coding bug

                    // Final block done. Check Auth code.
                    byte[] calcAuthCode = transform.GetAuthCode();

                    for (int i = 0; i < AUTH_CODE_LENGTH; i++) {
                        if (calcAuthCode[i] != slideBuffer[slideBufStartPos + i]) { throw new Exception("AES Authentication Code does not match. This is a super-CRC check on the data in the file after compression and encryption. \r\n" + "The file may be damaged."); }
                    }
                    break; // Reached the auth code
                }
            }
            return nBytes;
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream. </param>
        /// <param name="offset">The byte offset in buffer at which to begin copying bytes to the current stream. </param>
        /// <param name="count">The number of bytes to be written to the current stream. </param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            // ZipAESStream is used for reading but not for writing. Writing uses the ZipAESTransform directly.
            throw new NotImplementedException();
        }
    }
}
