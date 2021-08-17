/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System.IO;
using System.Collections.Generic;
using IFramework.Core.Zip.Zip;

namespace IFramework.Core.Environment
{
    public class Zip
    {
        private List<string> searchDirList = new List<string>();
   
        private ZipFile zipFile = null;

        public ZipFile ZipFile => zipFile;

        public Zip()
        {
            searchDirList.Add(PlatformSetting.PersistentDataPath);
#if (UNITY_ANDROID) && !UNITY_EDITOR
			if (zipFile == null)
			{
			    zipFile = new ZipFile(File.Open(UnityEngine.Application.dataPath, FileMode.Open, FileAccess.Read));
			}
#endif
        }
        
        ~Zip()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			if (zipFile != null)
			{
			    zipFile.Close();
			    zipFile = null;
			}
#endif
        }
        
        /// <summary>
        /// 在包内查找是否有改资源
        /// </summary>
        private bool FindResourceInAppInternal(string fileRelativePath)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
			string absoluteFilePath = FindFilePathInternal(fileRelativePath);
            return absoluteFilePath.IsNotNullOrEmpty();
#elif UNITY_ANDROID && !UNITY_EDITOR
			int entryIndex = zipFile.FindEntry(string.Format("assets/{0}", fileRelativePath), false);
			return entryIndex != -1;
#else
            string absoluteFilePath = Path.Combine(PlatformSetting.StreamingAssetsPath, fileRelativePath);
            return File.Exists(absoluteFilePath);
#endif
        }

        private void AddSearchPath(string dir)
        {
            searchDirList.Add(dir);
        }

        public bool FileExists(string fileRelativePath)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
			string absoluteFilePath = FindFilePath(fileRelativePath);
			return (absoluteFilePath.IsNotNullOrEmpty() && File.Exists(absoluteFilePath));
#elif UNITY_ANDROID && !UNITY_EDITOR
			string absoluteFilePath = FindFilePathInExteral(fileRelativePath);
			//先到外存去找
			if (absoluteFilePath.IsNotNullOrEmpty())
			{
			    return File.Exists(absoluteFilePath);
			}
			else
			{
			    if (zipFile == null)
			    {
			        return false;
			    }
                return zipFile.FindEntry(string.Format("assets/{0}", fileRelativePath), true) >= 0;
			}
#else
            string filePathStandalone = Path.Combine(PlatformSetting.StreamingAssetsPath, fileRelativePath);
            return (filePathStandalone.IsNotNullOrEmpty() && File.Exists(filePathStandalone));
#endif
        }

        public Stream OpenReadStream(string absFilePath)
        {
            if (absFilePath.IsNullOrEmpty())
            {
                return null;
            }

#if UNITY_ANDROID && !UNITY_EDITOR
			//Android 包内
			if (absFilePath.Contains(".apk/"))
			{
			    return OpenStreamInZip(absFilePath);
			}
#endif
            FileInfo fileInfo = new FileInfo(absFilePath);

            if (!fileInfo.Exists)
            {
                return null;
            }

            return fileInfo.OpenRead();
        }

        public void GetFileInInner(string fileName, List<string> outResult)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			//Android 包内
			GetFileInZip(zipFile, fileName, outResult);
			return;
#endif
            outResult = DirectoryUtils.GetFiles(PlatformSetting.StreamingAssetsPath, fileName, true);
        }

        public byte[] ReadSync(string fileRelativePath)
        {
            string absoluteFilePath = FindFilePathInExteral(fileRelativePath);
            if (!string.IsNullOrEmpty(absoluteFilePath))
            {
                return ReadSyncExtenal(fileRelativePath);
            }

            return ReadSyncInternal(fileRelativePath);
        }

        public byte[] ReadSyncByAbsoluteFilePath(string absoluteFilePath)
        {
            if (File.Exists(absoluteFilePath))
            {
                FileInfo fileInfo = new FileInfo(absoluteFilePath);
                return ReadFile(fileInfo);
            }
            else
            {
                return null;
            }
        }

        private byte[] ReadSyncExtenal(string fileRelativePath)
        {
            string absoluteFilePath = FindFilePathInExteral(fileRelativePath);

            if (!string.IsNullOrEmpty(absoluteFilePath))
            {
                FileInfo fileInfo = new FileInfo(absoluteFilePath);
                return ReadFile(fileInfo);
            }

            return null;
        }

        private byte[] ReadSyncInternal(string fileRelativePath)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return ReadDataInAndriodApk(fileRelativePath);
#else
            string absoluteFilePath = FindFilePathInternal(fileRelativePath);

            if (!string.IsNullOrEmpty(absoluteFilePath))
            {
                FileInfo fileInfo = new FileInfo(absoluteFilePath);
                return ReadFile(fileInfo);
            }
#endif

            return null;
        }
        
        private byte[] ReadFile(FileInfo fileInfo)
        {
            using (FileStream fileStream = fileInfo.OpenRead())
            {
                byte[] byteData = new byte[fileStream.Length];
                fileStream.Read(byteData, 0, byteData.Length);
                return byteData;
            }
        }

        private string FindFilePathInExteral(string file)
        {
            string filePath;
            for (int i = 0; i < searchDirList.Count; ++i)
            {
                filePath = Path.Combine(searchDirList[i], file);
                if (File.Exists(filePath))
                {
                    return filePath;
                }
            }
            return string.Empty;
        }

        private string FindFilePath(string file)
        {
            // 先到搜索列表里找
            string filePath = FindFilePathInExteral(file);
            if (!string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            // 在包内找
            filePath = FindFilePathInternal(file);
            if (!string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            return null;
        }

        private string FindFilePathInternal(string file)
        {
            string filePath = Path.Combine(PlatformSetting.StreamingAssetsPath, file);

            if (File.Exists(filePath))
            {
                return filePath;
            }

            return null;
        }
        
        private Stream OpenStreamInZip(string absPath)
        {
            string tag = "!/assets/";
            string androidFolder = absPath.Substring(0, absPath.IndexOf(tag));

            int startIndex = androidFolder.Length + tag.Length;
            string relativePath = absPath.Substring(startIndex, absPath.Length - startIndex);

            ZipEntry zipEntry = zipFile.GetEntry(string.Format("assets/{0}", relativePath));

            if (zipEntry != null)
            {
                return zipFile.GetInputStream(zipEntry);
            }
            else
            {
                Log.Error("未找到文件: {0}", absPath);
            }

            return null;
        }

        public void GetFileInZip(ZipFile zipFile, string fileName, List<string> outResult)
        {
            int totalCount = 0;

            foreach (var entry in zipFile)
            {
                ++totalCount;
                ZipEntry e = entry as ZipEntry;
                if (e != null)
                {
                    if (e.IsFile)
                    {
                        if (e.Name.EndsWith(fileName))
                        {
                            outResult.Add(zipFile.Name + "/!/" + e.Name);
                        }
                    }
                }
            }
        }
        
        private byte[] ReadDataInAndriodApk(string fileRelativePath)
        {
            byte[] byteData = null;
            if (zipFile == null)
            {
                Log.Error("未能打开apk");
                return null;
            }

            ZipEntry zipEntry = zipFile.GetEntry(string.Format("assets/{0}", fileRelativePath));
            
            if (zipEntry != null)
            {
                var stream = zipFile.GetInputStream(zipEntry);
                byteData = new byte[zipEntry.Size];
                stream.Read(byteData, 0, byteData.Length);
                stream.Close();
            }
            else
            {
                Log.Error("未找到文件: {0}", fileRelativePath);
            }

            return byteData;
        }
    }
}