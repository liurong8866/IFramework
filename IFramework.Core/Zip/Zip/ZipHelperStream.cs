using System;
using System.IO;

namespace IFramework.Core.Zip.Zip
{
    /// <summary>
    /// Holds data pertinent to a data descriptor.
    /// </summary>
    public class DescriptorData
    {
        /// <summary>
        /// Get /set the compressed size of data.
        /// </summary>
        public long CompressedSize { get; set; }

        /// <summary>
        /// Get / set the uncompressed size of data
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Get /set the crc value.
        /// </summary>
        public long Crc {
            get => crc;
            set => crc = value & 0xffffffff;
        }

        #region Instance Fields

        private long crc;

        #endregion
    }

    internal class EntryPatchData
    {
        public long SizePatchOffset { get; set; }

        public long CrcPatchOffset { get; set; }

        #region Instance Fields

        #endregion
    }

    /// <summary>
    /// This class assists with writing/reading from Zip files.
    /// </summary>
    internal class ZipHelperStream : Stream
    {
        #region Constructors

        /// <summary>
        /// Initialise an instance of this class.
        /// </summary>
        /// <param name="name">The name of the file to open.</param>
        public ZipHelperStream(string name)
        {
            stream = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            IsStreamOwner = true;
        }

        /// <summary>
        /// Initialise a new instance of <see cref="ZipHelperStream"/>.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        public ZipHelperStream(Stream stream)
        {
            this.stream = stream;
        }

        #endregion

        /// <summary>
        /// Get / set a value indicating wether the the underlying stream is owned or not.
        /// </summary>
        /// <remarks>If the stream is owned it is closed when this instance is closed.</remarks>
        public bool IsStreamOwner { get; set; }

        #region Base Stream Methods

        public override bool CanRead => stream.CanRead;

        public override bool CanSeek => stream.CanSeek;

        public override bool CanTimeout => stream.CanTimeout;

        public override long Length => stream.Length;

        public override long Position {
            get => stream.Position;
            set => stream.Position = value;
        }

        public override bool CanWrite => stream.CanWrite;

        public override void Flush()
        {
            stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return stream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
        }

        /// <summary>
        /// Close the stream.
        /// </summary>
        /// <remarks>
        /// The underlying stream is closed only if <see cref="IsStreamOwner"/> is true.
        /// </remarks>
        protected override void Dispose(bool disposing)
        {
            Stream toClose = stream;
            stream = null;

            if (IsStreamOwner && toClose != null) {
                IsStreamOwner = false;
                toClose.Dispose();
            }
        }

        #endregion

        // Write the local file header
        // TODO: ZipHelperStream.WriteLocalHeader is not yet used and needs checking for ZipFile and ZipOuptutStream usage
        private void WriteLocalHeader(ZipEntry entry, EntryPatchData patchData)
        {
            CompressionMethod method = entry.CompressionMethod;
            bool headerInfoAvailable = true; // How to get this?
            bool patchEntryHeader = false;
            WriteLeInt(ZipConstants.LOCAL_HEADER_SIGNATURE);
            WriteLeShort(entry.Version);
            WriteLeShort(entry.Flags);
            WriteLeShort((byte)method);
            WriteLeInt((int)entry.DosTime);

            if (headerInfoAvailable) {
                WriteLeInt((int)entry.Crc);

                if (entry.LocalHeaderRequiresZip64) {
                    WriteLeInt(-1);
                    WriteLeInt(-1);
                }
                else {
                    WriteLeInt(entry.IsCrypted ? (int)entry.CompressedSize + ZipConstants.CRYPTO_HEADER_SIZE : (int)entry.CompressedSize);
                    WriteLeInt((int)entry.Size);
                }
            }
            else {
                if (patchData != null) {
                    patchData.CrcPatchOffset = stream.Position;
                }
                WriteLeInt(0); // Crc

                if (patchData != null) {
                    patchData.SizePatchOffset = stream.Position;
                }

                // For local header both sizes appear in Zip64 Extended Information
                if (entry.LocalHeaderRequiresZip64 && patchEntryHeader) {
                    WriteLeInt(-1);
                    WriteLeInt(-1);
                }
                else {
                    WriteLeInt(0); // Compressed size
                    WriteLeInt(0); // Uncompressed size
                }
            }
            byte[] name = ZipConstants.ConvertToArray(entry.Flags, entry.Name);

            if (name.Length > 0xFFFF) {
                throw new ZipException("Entry name too long.");
            }
            ZipExtraData ed = new ZipExtraData(entry.ExtraData);

            if (entry.LocalHeaderRequiresZip64 && (headerInfoAvailable || patchEntryHeader)) {
                ed.StartNewEntry();

                if (headerInfoAvailable) {
                    ed.AddLeLong(entry.Size);
                    ed.AddLeLong(entry.CompressedSize);
                }
                else {
                    ed.AddLeLong(-1);
                    ed.AddLeLong(-1);
                }
                ed.AddNewEntry(1);

                if (!ed.Find(1)) {
                    throw new ZipException("Internal error cant find extra data");
                }

                if (patchData != null) {
                    patchData.SizePatchOffset = ed.CurrentReadIndex;
                }
            }
            else {
                ed.Delete(1);
            }
            byte[] extra = ed.GetEntryData();
            WriteLeShort(name.Length);
            WriteLeShort(extra.Length);

            if (name.Length > 0) {
                stream.Write(name, 0, name.Length);
            }

            if (entry.LocalHeaderRequiresZip64 && patchEntryHeader) {
                patchData.SizePatchOffset += stream.Position;
            }

            if (extra.Length > 0) {
                stream.Write(extra, 0, extra.Length);
            }
        }

        /// <summary>
        /// Locates a block with the desired <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature">The signature to find.</param>
        /// <param name="endLocation">Location, marking the end of block.</param>
        /// <param name="minimumBlockSize">Minimum size of the block.</param>
        /// <param name="maximumVariableData">The maximum variable data.</param>
        /// <returns>Eeturns the offset of the first byte after the signature; -1 if not found</returns>
        public long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
        {
            long pos = endLocation - minimumBlockSize;

            if (pos < 0) {
                return -1;
            }
            long giveUpMarker = Math.Max(pos - maximumVariableData, 0);

            // TODO: This loop could be optimised for speed.
            do {
                if (pos < giveUpMarker) {
                    return -1;
                }
                Seek(pos--, SeekOrigin.Begin);
            } while (ReadLeInt() != signature);
            return Position;
        }

        /// <summary>
        /// Write Zip64 end of central directory records (File header and locator).
        /// </summary>
        /// <param name="noOfEntries">The number of entries in the central directory.</param>
        /// <param name="sizeEntries">The size of entries in the central directory.</param>
        /// <param name="centralDirOffset">The offset of the dentral directory.</param>
        public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
        {
            long centralSignatureOffset = stream.Position;
            WriteLeInt(ZipConstants.ZIP64_CENTRAL_FILE_HEADER_SIGNATURE);
            WriteLeLong(44);                            // Size of this record (total size of remaining fields in header or full size - 12)
            WriteLeShort(ZipConstants.VERSION_MADE_BY); // Version made by
            WriteLeShort(ZipConstants.VERSION_ZIP64);   // Version to extract
            WriteLeInt(0);                              // Number of this disk
            WriteLeInt(0);                              // number of the disk with the start of the central directory
            WriteLeLong(noOfEntries);                   // No of entries on this disk
            WriteLeLong(noOfEntries);                   // Total No of entries in central directory
            WriteLeLong(sizeEntries);                   // Size of the central directory
            WriteLeLong(centralDirOffset);              // offset of start of central directory
            // zip64 extensible data sector not catered for here (variable size)

            // Write the Zip64 end of central directory locator
            WriteLeInt(ZipConstants.ZIP64_CENTRAL_DIR_LOCATOR_SIGNATURE);

            // no of the disk with the start of the zip64 end of central directory
            WriteLeInt(0);

            // relative offset of the zip64 end of central directory record
            WriteLeLong(centralSignatureOffset);

            // total number of disks
            WriteLeInt(1);
        }

        /// <summary>
        /// Write the required records to end the central directory.
        /// </summary>
        /// <param name="noOfEntries">The number of entries in the directory.</param>
        /// <param name="sizeEntries">The size of the entries in the directory.</param>
        /// <param name="startOfCentralDirectory">The start of the central directory.</param>
        /// <param name="comment">The archive comment.  (This can be null).</param>
        public void WriteEndOfCentralDirectory(long noOfEntries, long sizeEntries, long startOfCentralDirectory, byte[] comment)
        {
            if (noOfEntries >= 0xffff || startOfCentralDirectory >= 0xffffffff || sizeEntries >= 0xffffffff) {
                WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
            }
            WriteLeInt(ZipConstants.END_OF_CENTRAL_DIRECTORY_SIGNATURE);

            // TODO: ZipFile Multi disk handling not done
            WriteLeShort(0); // number of this disk
            WriteLeShort(0); // no of disk with start of central dir

            // Number of entries
            if (noOfEntries >= 0xffff) {
                WriteLeUshort(0xffff); // Zip64 marker
                WriteLeUshort(0xffff);
            }
            else {
                WriteLeShort((short)noOfEntries); // entries in central dir for this disk
                WriteLeShort((short)noOfEntries); // total entries in central directory
            }

            // Size of the central directory
            if (sizeEntries >= 0xffffffff) {
                WriteLeUint(0xffffffff); // Zip64 marker
            }
            else {
                WriteLeInt((int)sizeEntries);
            }

            // offset of start of central directory
            if (startOfCentralDirectory >= 0xffffffff) {
                WriteLeUint(0xffffffff); // Zip64 marker
            }
            else {
                WriteLeInt((int)startOfCentralDirectory);
            }
            int commentLength = comment != null ? comment.Length : 0;

            if (commentLength > 0xffff) {
                throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", commentLength));
            }
            WriteLeShort(commentLength);

            if (commentLength > 0) {
                Write(comment, 0, comment.Length);
            }
        }

        #region LE value reading/writing

        /// <summary>
        /// Read an unsigned short in little endian byte order.
        /// </summary>
        /// <returns>Returns the value read.</returns>
        /// <exception cref="IOException">
        /// An i/o error occurs.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The file ends prematurely
        /// </exception>
        public int ReadLeShort()
        {
            int byteValue1 = stream.ReadByte();

            if (byteValue1 < 0) {
                throw new EndOfStreamException();
            }
            int byteValue2 = stream.ReadByte();

            if (byteValue2 < 0) {
                throw new EndOfStreamException();
            }
            return byteValue1 | (byteValue2 << 8);
        }

        /// <summary>
        /// Read an int in little endian byte order.
        /// </summary>
        /// <returns>Returns the value read.</returns>
        /// <exception cref="IOException">
        /// An i/o error occurs.
        /// </exception>
        /// <exception cref="System.IO.EndOfStreamException">
        /// The file ends prematurely
        /// </exception>
        public int ReadLeInt()
        {
            return ReadLeShort() | (ReadLeShort() << 16);
        }

        /// <summary>
        /// Read a long in little endian byte order.
        /// </summary>
        /// <returns>The value read.</returns>
        public long ReadLeLong()
        {
            return (uint)ReadLeInt() | ((long)ReadLeInt() << 32);
        }

        /// <summary>
        /// Write an unsigned short in little endian byte order.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLeShort(int value)
        {
            stream.WriteByte((byte)(value & 0xff));
            stream.WriteByte((byte)((value >> 8) & 0xff));
        }

        /// <summary>
        /// Write a ushort in little endian byte order.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLeUshort(ushort value)
        {
            stream.WriteByte((byte)(value & 0xff));
            stream.WriteByte((byte)(value >> 8));
        }

        /// <summary>
        /// Write an int in little endian byte order.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLeInt(int value)
        {
            WriteLeShort(value);
            WriteLeShort(value >> 16);
        }

        /// <summary>
        /// Write a uint in little endian byte order.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLeUint(uint value)
        {
            WriteLeUshort((ushort)(value & 0xffff));
            WriteLeUshort((ushort)(value >> 16));
        }

        /// <summary>
        /// Write a long in little endian byte order.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLeLong(long value)
        {
            WriteLeInt((int)value);
            WriteLeInt((int)(value >> 32));
        }

        /// <summary>
        /// Write a ulong in little endian byte order.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLeUlong(ulong value)
        {
            WriteLeUint((uint)(value & 0xffffffff));
            WriteLeUint((uint)(value >> 32));
        }

        #endregion

        /// <summary>
        /// Write a data descriptor.
        /// </summary>
        /// <param name="entry">The entry to write a descriptor for.</param>
        /// <returns>Returns the number of descriptor bytes written.</returns>
        public int WriteDataDescriptor(ZipEntry entry)
        {
            if (entry == null) {
                throw new ArgumentNullException(nameof(entry));
            }
            int result = 0;

            // Add data descriptor if flagged as required
            if ((entry.Flags & (int)GeneralBitFlags.Descriptor) != 0) {
                // The signature is not PKZIP originally but is now described as optional
                // in the PKZIP Appnote documenting trhe format.
                WriteLeInt(ZipConstants.DATA_DESCRIPTOR_SIGNATURE);
                WriteLeInt(unchecked((int)entry.Crc));
                result += 8;

                if (entry.LocalHeaderRequiresZip64) {
                    WriteLeLong(entry.CompressedSize);
                    WriteLeLong(entry.Size);
                    result += 16;
                }
                else {
                    WriteLeInt((int)entry.CompressedSize);
                    WriteLeInt((int)entry.Size);
                    result += 8;
                }
            }
            return result;
        }

        /// <summary>
        /// Read data descriptor at the end of compressed data.
        /// </summary>
        /// <param name="zip64">if set to <c>true</c> [zip64].</param>
        /// <param name="data">The data to fill in.</param>
        /// <returns>Returns the number of bytes read in the descriptor.</returns>
        public void ReadDataDescriptor(bool zip64, DescriptorData data)
        {
            int intValue = ReadLeInt();

            // In theory this may not be a descriptor according to PKZIP appnote.
            // In practise its always there.
            if (intValue != ZipConstants.DATA_DESCRIPTOR_SIGNATURE) {
                throw new ZipException("Data descriptor signature not found");
            }
            data.Crc = ReadLeInt();

            if (zip64) {
                data.CompressedSize = ReadLeLong();
                data.Size = ReadLeLong();
            }
            else {
                data.CompressedSize = ReadLeInt();
                data.Size = ReadLeInt();
            }
        }

        #region Instance Fields

        private Stream stream;

        #endregion
    }
}
