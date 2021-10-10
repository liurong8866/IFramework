using System;
using System.Security.Cryptography;

namespace IFramework.Core.Zip.Encryption
{
    /// <summary>
    /// Transforms stream using AES in CTR mode
    /// </summary>
    internal class ZipAesTransform : ICryptoTransform
    {
        private class IncrementalHash : HMACSHA1
        {
            private bool finalised;

            public IncrementalHash(byte[] key) : base(key) { }

            public static IncrementalHash CreateHmac(string n, byte[] key)
            {
                return new IncrementalHash(key);
            }

            public void AppendData(byte[] buffer, int offset, int count)
            {
                TransformBlock(buffer,
                               offset,
                               count,
                               buffer,
                               offset);
            }

            public byte[] GetHashAndReset()
            {
                if (!finalised) {
                    byte[] dummy = new byte[0];
                    TransformFinalBlock(dummy, 0, 0);
                    finalised = true;
                }
                return Hash;
            }
        }

        private static class HashAlgorithmName
        {
            public static readonly string sha1 = null;
        }

        private const int PWD_VER_LENGTH = 2;

        // WinZip use iteration count of 1000 for PBKDF2 key generation
        private const int KEY_ROUNDS = 1000;

        // For 128-bit AES (16 bytes) the encryption is implemented as expected.
        // For 256-bit AES (32 bytes) WinZip do full 256 bit AES of the nonce to create the encryption
        // block but use only the first 16 bytes of it, and discard the second half.
        private const int ENCRYPT_BLOCK = 16;

        private readonly ICryptoTransform encryptor;
        private readonly byte[] counterNonce;
        private readonly byte[] encryptBuffer;
        private int encrPos;
        private readonly IncrementalHash hmacsha1;
        private byte[] authCode;

        private readonly bool writeMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">Password string</param>
        /// <param name="saltBytes">Random bytes, length depends on encryption strength.
        /// 128 bits = 8 bytes, 192 bits = 12 bytes, 256 bits = 16 bytes.</param>
        /// <param name="blockSize">The encryption strength, in bytes eg 16 for 128 bits.</param>
        /// <param name="writeMode">True when creating a zip, false when reading. For the AuthCode.</param>
        ///
        public ZipAesTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
        {
            if (blockSize != 16 && blockSize != 32) // 24 valid for AES but not supported by Winzip
                throw new Exception("Invalid blocksize " + blockSize + ". Must be 16 or 32.");
            if (saltBytes.Length != blockSize / 2) throw new Exception("Invalid salt len. Must be " + blockSize / 2 + " for blocksize " + blockSize);

            // initialise the encryption buffer and buffer pos
            InputBlockSize = blockSize;
            encryptBuffer = new byte[InputBlockSize];
            encrPos = ENCRYPT_BLOCK;

            // Performs the equivalent of derive_key in Dr Brian Gladman's pwd2key.c
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, KEY_ROUNDS);
            Aes rm = Aes.Create();
            rm.Mode = CipherMode.ECB; // No feedback from cipher for CTR mode
            counterNonce = new byte[InputBlockSize];
            byte[] byteKey1 = pdb.GetBytes(InputBlockSize);
            byte[] byteKey2 = pdb.GetBytes(InputBlockSize);
            encryptor = rm.CreateEncryptor(byteKey1, byteKey2);
            PwdVerifier = pdb.GetBytes(PWD_VER_LENGTH);
            //
            hmacsha1 = IncrementalHash.CreateHmac(HashAlgorithmName.sha1, byteKey2);
            this.writeMode = writeMode;
        }

        /// <summary>
        /// Implement the ICryptoTransform method.
        /// </summary>
        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            // Pass the data stream to the hash algorithm for generating the Auth Code.
            // This does not change the inputBuffer. Do this before decryption for read mode.
            if (!writeMode) {
                hmacsha1.AppendData(inputBuffer, inputOffset, inputCount);
            }

            // Encrypt with AES in CTR mode. Regards to Dr Brian Gladman for this.
            int ix = 0;

            while (ix < inputCount) {
                if (encrPos == ENCRYPT_BLOCK) {
                    /* increment encryption nonce   */
                    int j = 0;

                    while (++counterNonce[j] == 0) {
                        ++j;
                    }

                    /* encrypt the nonce to form next xor buffer    */
                    encryptor.TransformBlock(counterNonce,
                                             0,
                                             InputBlockSize,
                                             encryptBuffer,
                                             0);
                    encrPos = 0;
                }
                outputBuffer[ix + outputOffset] = (byte)(inputBuffer[ix + inputOffset] ^ encryptBuffer[encrPos++]);
                //
                ix++;
            }

            if (writeMode) {
                // This does not change the buffer.
                hmacsha1.AppendData(outputBuffer, outputOffset, inputCount);
            }
            return inputCount;
        }

        /// <summary>
        /// Returns the 2 byte password verifier
        /// </summary>
        public byte[] PwdVerifier { get; }

        /// <summary>
        /// Returns the 10 byte AUTH CODE to be checked or appended immediately following the AES data stream.
        /// </summary>
        public byte[] GetAuthCode()
        {
            if (authCode == null) {
                authCode = hmacsha1.GetHashAndReset();
            }
            return authCode;
        }

        #region ICryptoTransform Members

        /// <summary>
        /// Not implemented.
        /// </summary>
        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            throw new NotImplementedException("ZipAESTransform.TransformFinalBlock");
        }

        /// <summary>
        /// Gets the size of the input data blocks in bytes.
        /// </summary>
        public int InputBlockSize { get; }

        /// <summary>
        /// Gets the size of the output data blocks in bytes.
        /// </summary>
        public int OutputBlockSize => InputBlockSize;

        /// <summary>
        /// Gets a value indicating whether multiple blocks can be transformed.
        /// </summary>
        public bool CanTransformMultipleBlocks => true;

        /// <summary>
        /// Gets a value indicating whether the current transform can be reused.
        /// </summary>
        public bool CanReuseTransform => true;

        /// <summary>
        /// Cleanup internal state.
        /// </summary>
        public void Dispose()
        {
            encryptor.Dispose();
        }

        #endregion
    }
}
