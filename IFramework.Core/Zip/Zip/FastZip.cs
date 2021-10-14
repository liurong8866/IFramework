using System;
using System.Collections;
using System.IO;
using IFramework.Core.Zip.Zip.Compression;

namespace IFramework.Core.Zip.Zip
{
    /// <summary>
    /// FastZipEvents supports all events applicable to <see cref="FastZip">FastZip</see> operations.
    /// </summary>
    public class FastZipEvents
    {
        /// <summary>
        /// Delegate to invoke when processing directories.
        /// </summary>
        public event EventHandler<DirectoryEventArgs> ProcessDirectory;

        /// <summary>
        /// Delegate to invoke when processing files.
        /// </summary>
        public ProcessFileHandler processFile;

        /// <summary>
        /// Delegate to invoke during processing of files.
        /// </summary>
        public ProgressHandler progress;

        /// <summary>
        /// Delegate to invoke when processing for a file has been completed.
        /// </summary>
        public CompletedFileHandler completedFile;

        /// <summary>
        /// Delegate to invoke when processing directory failures.
        /// </summary>
        public DirectoryFailureHandler directoryFailure;

        /// <summary>
        /// Delegate to invoke when processing file failures.
        /// </summary>
        public FileFailureHandler fileFailure;

        /// <summary>
        /// Raise the <see cref="directoryFailure">directory failure</see> event.
        /// </summary>
        /// <param name="directory">The directory causing the failure.</param>
        /// <param name="e">The exception for this event.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnDirectoryFailure(string directory, Exception e)
        {
            bool result = false;
            DirectoryFailureHandler handler = directoryFailure;
            if (handler != null) {
                ScanFailureEventArgs args = new ScanFailureEventArgs(directory, e);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// Fires the <see cref="fileFailure"> file failure handler delegate</see>.
        /// </summary>
        /// <param name="file">The file causing the failure.</param>
        /// <param name="e">The exception for this failure.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnFileFailure(string file, Exception e)
        {
            FileFailureHandler handler = fileFailure;
            bool result = handler != null;
            if (result) {
                ScanFailureEventArgs args = new ScanFailureEventArgs(file, e);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// Fires the <see cref="processFile">ProcessFile delegate</see>.
        /// </summary>
        /// <param name="file">The file being processed.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnProcessFile(string file)
        {
            bool result = true;
            ProcessFileHandler handler = processFile;
            if (handler != null) {
                ScanEventArgs args = new ScanEventArgs(file);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// Fires the <see cref="completedFile"/> delegate
        /// </summary>
        /// <param name="file">The file whose processing has been completed.</param>
        /// <returns>A boolean indicating if execution should continue or not.</returns>
        public bool OnCompletedFile(string file)
        {
            bool result = true;
            CompletedFileHandler handler = completedFile;
            if (handler != null) {
                ScanEventArgs args = new ScanEventArgs(file);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// Fires the <see cref="ProcessDirectory">process directory</see> delegate.
        /// </summary>
        /// <param name="directory">The directory being processed.</param>
        /// <param name="hasMatchingFiles">Flag indicating if the directory has matching files as determined by the current filter.</param>
        /// <returns>A <see cref="bool"/> of true if the operation should continue; false otherwise.</returns>
        public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            bool result = true;
            EventHandler<DirectoryEventArgs> handler = ProcessDirectory;
            if (handler != null) {
                DirectoryEventArgs args = new DirectoryEventArgs(directory, hasMatchingFiles);
                handler(this, args);
                result = args.ContinueRunning;
            }
            return result;
        }

        /// <summary>
        /// The minimum timespan between <see cref="progress"/> events.
        /// </summary>
        /// <value>The minimum period of time between <see cref="progress"/> events.</value>
        /// <seealso cref="progress"/>
        /// <remarks>The default interval is three seconds.</remarks>
        public TimeSpan ProgressInterval { get; set; } = TimeSpan.FromSeconds(3);

        #region Instance Fields

        #endregion
    }

    /// <summary>
    /// FastZip provides facilities for creating and extracting zip files.
    /// </summary>
    public class FastZip
    {
        #region Enumerations

        /// <summary>
        /// Defines the desired handling when overwriting files during extraction.
        /// </summary>
        public enum Overwrite
        {
            /// <summary>
            /// Prompt the user to confirm overwriting
            /// </summary>
            Prompt,

            /// <summary>
            /// Never overwrite files.
            /// </summary>
            Never,

            /// <summary>
            /// Always overwrite files.
            /// </summary>
            Always
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a default instance of <see cref="FastZip"/>.
        /// </summary>
        public FastZip() { }

        /// <summary>
        /// Initialise a new instance of <see cref="FastZip"/>
        /// </summary>
        /// <param name="events">The <see cref="FastZipEvents">events</see> to use during operations.</param>
        public FastZip(FastZipEvents events)
        {
            this.events = events;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get/set a value indicating wether empty directories should be created.
        /// </summary>
        public bool CreateEmptyDirectories { get; set; }

        /// <summary>
        /// Get / set the password value.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Get or set the <see cref="INameTransform"></see> active when creating Zip files.
        /// </summary>
        /// <seealso cref="EntryFactory"></seealso>
        public INameTransform NameTransform { get => entryFactory.NameTransform; set => entryFactory.NameTransform = value; }

        /// <summary>
        /// Get or set the <see cref="IEntryFactory"></see> active when creating Zip files.
        /// </summary>
        public IEntryFactory EntryFactory {
            get => entryFactory;
            set {
                if (value == null) {
                    entryFactory = new ZipEntryFactory();
                }
                else {
                    entryFactory = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the setting for <see cref="UseZip64">Zip64 handling when writing.</see>
        /// </summary>
        /// <remarks>
        /// The default value is dynamic which is not backwards compatible with old
        /// programs and can cause problems with XP's built in compression which cant
        /// read Zip64 archives. However it does avoid the situation were a large file
        /// is added and cannot be completed correctly.
        /// NOTE: Setting the size for entries before they are added is the best solution!
        /// By default the EntryFactory used by FastZip will set fhe file size.
        /// </remarks>
        public UseZip64 UseZip64 { get; set; } = UseZip64.Dynamic;

        /// <summary>
        /// Get/set a value indicating wether file dates and times should
        /// be restored when extracting files from an archive.
        /// </summary>
        /// <remarks>The default value is false.</remarks>
        public bool RestoreDateTimeOnExtract { get; set; }

        /// <summary>
        /// Get/set a value indicating whether file attributes should
        /// be restored during extract operations
        /// </summary>
        public bool RestoreAttributesOnExtract { get; set; }

        /// <summary>
        /// Get/set the Compression Level that will be used
        /// when creating the zip
        /// </summary>
        public Deflater.CompressionLevel CompressionLevel { get; set; } = Deflater.CompressionLevel.DEFAULT_COMPRESSION;

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate called when confirming overwriting of files.
        /// </summary>
        public delegate bool ConfirmOverwriteDelegate(string fileName);

        #endregion

        #region CreateZip

        /// <summary>
        /// Create a zip file.
        /// </summary>
        /// <param name="zipFileName">The name of the zip file to create.</param>
        /// <param name="sourceDirectory">The directory to source files from.</param>
        /// <param name="recurse">True to recurse directories, false for no recursion.</param>
        /// <param name="fileFilter">The <see cref="PathFilter">file filter</see> to apply.</param>
        /// <param name="directoryFilter">The <see cref="PathFilter">directory filter</see> to apply.</param>
        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
        }

        /// <summary>
        /// Create a zip file/archive.
        /// </summary>
        /// <param name="zipFileName">The name of the zip file to create.</param>
        /// <param name="sourceDirectory">The directory to obtain files and directories from.</param>
        /// <param name="recurse">True to recurse directories, false for no recursion.</param>
        /// <param name="fileFilter">The file filter to apply.</param>
        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
        }

        /// <summary>
        /// Create a zip archive sending output to the <paramref name="outputStream"/> passed.
        /// </summary>
        /// <param name="outputStream">The stream to write archive data to.</param>
        /// <param name="sourceDirectory">The directory to source files from.</param>
        /// <param name="recurse">True to recurse directories, false for no recursion.</param>
        /// <param name="fileFilter">The <see cref="PathFilter">file filter</see> to apply.</param>
        /// <param name="directoryFilter">The <see cref="PathFilter">directory filter</see> to apply.</param>
        /// <remarks>The <paramref name="outputStream"/> is closed after creation.</remarks>
        public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            NameTransform = new ZipNameTransform(sourceDirectory);
            this.sourceDirectory = sourceDirectory;
            using (this.outputStream = new ZipOutputStream(outputStream)) {
                this.outputStream.SetLevel((int)CompressionLevel);
                if (Password != null) {
                    this.outputStream.Password = Password;
                }
                this.outputStream.UseZip64 = UseZip64;
                FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
                scanner.processFile += ProcessFile;
                if (CreateEmptyDirectories) {
                    scanner.ProcessDirectory += ProcessDirectory;
                }
                if (events != null) {
                    if (events.fileFailure != null) {
                        scanner.fileFailure += events.fileFailure;
                    }
                    if (events.directoryFailure != null) {
                        scanner.directoryFailure += events.directoryFailure;
                    }
                }
                scanner.Scan(sourceDirectory, recurse);
            }
        }

        #endregion

        #region ExtractZip

        /// <summary>
        /// Extract the contents of a zip file.
        /// </summary>
        /// <param name="zipFileName">The zip file to extract from.</param>
        /// <param name="targetDirectory">The directory to save extracted information in.</param>
        /// <param name="fileFilter">A filter to apply to files.</param>
        public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
        {
            ExtractZip(zipFileName, targetDirectory, Overwrite.Always, null, fileFilter, null, RestoreDateTimeOnExtract);
        }

        /// <summary>
        /// Extract the contents of a zip file.
        /// </summary>
        /// <param name="zipFileName">The zip file to extract from.</param>
        /// <param name="targetDirectory">The directory to save extracted information in.</param>
        /// <param name="overwrite">The style of <see cref="Overwrite">overwriting</see> to apply.</param>
        /// <param name="confirmDelegate">A delegate to invoke when confirming overwriting.</param>
        /// <param name="fileFilter">A filter to apply to files.</param>
        /// <param name="directoryFilter">A filter to apply to directories.</param>
        /// <param name="restoreDateTime">Flag indicating whether to restore the date and time for extracted files.</param>
        public void ExtractZip(string zipFileName, string targetDirectory, Overwrite overwrite, ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime)
        {
            Stream inputStream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            ExtractZip(inputStream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true);
        }

        /// <summary>
        /// Extract the contents of a zip file held in a stream.
        /// </summary>
        /// <param name="inputStream">The seekable input stream containing the zip to extract from.</param>
        /// <param name="targetDirectory">The directory to save extracted information in.</param>
        /// <param name="overwrite">The style of <see cref="Overwrite">overwriting</see> to apply.</param>
        /// <param name="confirmDelegate">A delegate to invoke when confirming overwriting.</param>
        /// <param name="fileFilter">A filter to apply to files.</param>
        /// <param name="directoryFilter">A filter to apply to directories.</param>
        /// <param name="restoreDateTime">Flag indicating whether to restore the date and time for extracted files.</param>
        /// <param name="isStreamOwner">Flag indicating whether the inputStream will be closed by this method.</param>
        public void ExtractZip(Stream inputStream, string targetDirectory, Overwrite overwrite, ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner)
        {
            if (overwrite == Overwrite.Prompt && confirmDelegate == null) {
                throw new ArgumentNullException(nameof(confirmDelegate));
            }
            continueRunning = true;
            this.overwrite = overwrite;
            this.confirmDelegate = confirmDelegate;
            extractNameTransform = new WindowsNameTransform(targetDirectory);
            this.fileFilter = new NameFilter(fileFilter);
            this.directoryFilter = new NameFilter(directoryFilter);
            RestoreDateTimeOnExtract = restoreDateTime;
            using (zipFile = new ZipFile(inputStream)) {
                if (Password != null) {
                    zipFile.Password = Password;
                }
                zipFile.IsStreamOwner = isStreamOwner;
                IEnumerator enumerator = zipFile.GetEnumerator();
                while (continueRunning && enumerator.MoveNext()) {
                    ZipEntry entry = (ZipEntry)enumerator.Current;
                    if (entry.IsFile) {
                        // TODO Path.GetDirectory can fail here on invalid characters.
                        if (this.directoryFilter.IsMatch(Path.GetDirectoryName(entry.Name)) && this.fileFilter.IsMatch(entry.Name)) {
                            ExtractEntry(entry);
                        }
                    }
                    else if (entry.IsDirectory) {
                        if (this.directoryFilter.IsMatch(entry.Name) && CreateEmptyDirectories) {
                            ExtractEntry(entry);
                        }
                    }
                }
            }
        }

        #endregion

        #region Internal Processing

        private void ProcessDirectory(object sender, DirectoryEventArgs e)
        {
            if (!e.HasMatchingFiles && CreateEmptyDirectories) {
                if (events != null) {
                    events.OnProcessDirectory(e.Name, e.HasMatchingFiles);
                }
                if (e.ContinueRunning) {
                    if (e.Name != sourceDirectory) {
                        ZipEntry entry = entryFactory.MakeDirectoryEntry(e.Name);
                        outputStream.PutNextEntry(entry);
                    }
                }
            }
        }

        private void ProcessFile(object sender, ScanEventArgs e)
        {
            if (events != null && events.processFile != null) {
                events.processFile(sender, e);
            }
            if (e.ContinueRunning) {
                try {
                    // The open below is equivalent to OpenRead which gaurantees that if opened the
                    // file will not be changed by subsequent openers, but precludes opening in some cases
                    // were it could succeed. ie the open may fail as its already open for writing and the share mode should reflect that.
                    using (FileStream stream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                        ZipEntry entry = entryFactory.MakeFileEntry(e.Name);
                        outputStream.PutNextEntry(entry);
                        AddFileContents(e.Name, stream);
                    }
                }
                catch (Exception ex) {
                    if (events != null) {
                        continueRunning = events.OnFileFailure(e.Name, ex);
                    }
                    else {
                        continueRunning = false;
                        throw;
                    }
                }
            }
        }

        private void AddFileContents(string name, Stream stream)
        {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            if (buffer == null) {
                buffer = new byte[4096];
            }
            if (events != null && events.progress != null) {
                StreamUtils.Copy(stream, outputStream, buffer, events.progress, events.ProgressInterval, this, name);
            }
            else {
                StreamUtils.Copy(stream, outputStream, buffer);
            }
            if (events != null) {
                continueRunning = events.OnCompletedFile(name);
            }
        }

        private void ExtractFileEntry(ZipEntry entry, string targetName)
        {
            bool proceed = true;
            if (overwrite != Overwrite.Always) {
                if (File.Exists(targetName)) {
                    if (overwrite == Overwrite.Prompt && confirmDelegate != null) {
                        proceed = confirmDelegate(targetName);
                    }
                    else {
                        proceed = false;
                    }
                }
            }
            if (proceed) {
                if (events != null) {
                    continueRunning = events.OnProcessFile(entry.Name);
                }
                if (continueRunning) {
                    try {
                        using (FileStream outputStream = File.Create(targetName)) {
                            if (buffer == null) {
                                buffer = new byte[4096];
                            }
                            if (events != null && events.progress != null) {
                                StreamUtils.Copy(zipFile.GetInputStream(entry), outputStream, buffer, events.progress, events.ProgressInterval, this, entry.Name, entry.Size);
                            }
                            else {
                                StreamUtils.Copy(zipFile.GetInputStream(entry), outputStream, buffer);
                            }
                            if (events != null) {
                                continueRunning = events.OnCompletedFile(entry.Name);
                            }
                        }
                        if (RestoreDateTimeOnExtract) {
                            File.SetLastWriteTime(targetName, entry.DateTime);
                        }
                        if (RestoreAttributesOnExtract && entry.IsDosEntry && entry.ExternalFileAttributes != -1) {
                            FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
                            // TODO: FastZip - Setting of other file attributes on extraction is a little trickier.
                            fileAttributes &= FileAttributes.Archive | FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden;
                            File.SetAttributes(targetName, fileAttributes);
                        }
                    }
                    catch (Exception ex) {
                        if (events != null) {
                            continueRunning = events.OnFileFailure(targetName, ex);
                        }
                        else {
                            continueRunning = false;
                            throw;
                        }
                    }
                }
            }
        }

        private void ExtractEntry(ZipEntry entry)
        {
            bool doExtraction = entry.IsCompressionMethodSupported();
            string targetName = entry.Name;
            if (doExtraction) {
                if (entry.IsFile) {
                    targetName = extractNameTransform.TransformFile(targetName);
                }
                else if (entry.IsDirectory) {
                    targetName = extractNameTransform.TransformDirectory(targetName);
                }
                doExtraction = !string.IsNullOrEmpty(targetName);
            }

            // TODO: Fire delegate/throw exception were compression method not supported, or name is invalid?
            string dirName = null;
            if (doExtraction) {
                if (entry.IsDirectory) {
                    dirName = targetName;
                }
                else {
                    dirName = Path.GetDirectoryName(Path.GetFullPath(targetName));
                }
            }
            if (doExtraction && !Directory.Exists(dirName)) {
                if (!entry.IsDirectory || CreateEmptyDirectories) {
                    try {
                        Directory.CreateDirectory(dirName);
                    }
                    catch (Exception ex) {
                        doExtraction = false;
                        if (events != null) {
                            if (entry.IsDirectory) {
                                continueRunning = events.OnDirectoryFailure(targetName, ex);
                            }
                            else {
                                continueRunning = events.OnFileFailure(targetName, ex);
                            }
                        }
                        else {
                            continueRunning = false;
                            throw;
                        }
                    }
                }
            }
            if (doExtraction && entry.IsFile) {
                ExtractFileEntry(entry, targetName);
            }
        }

        private static int MakeExternalAttributes(FileInfo info)
        {
            return (int)info.Attributes;
        }

        private static bool NameIsValid(string name)
        {
            return !string.IsNullOrEmpty(name) && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
        }

        #endregion

        #region Instance Fields

        private bool continueRunning;
        private byte[] buffer;
        private ZipOutputStream outputStream;
        private ZipFile zipFile;
        private string sourceDirectory;
        private NameFilter fileFilter;
        private NameFilter directoryFilter;
        private Overwrite overwrite;
        private ConfirmOverwriteDelegate confirmDelegate;

        private readonly FastZipEvents events;
        private IEntryFactory entryFactory = new ZipEntryFactory();
        private INameTransform extractNameTransform;

        #endregion
    }
}
