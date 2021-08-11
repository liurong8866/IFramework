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

using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace IFramework.Core
{
    public static class FileUtility
    {
        #region 文件读操作

        /// <summary>
        /// 读取文件Byte型数组
        /// </summary>
        /// <param name="fileName">文件全路径名</param>
        /// <param name="data">接收数组</param>
        /// <returns></returns>
        public static bool ReadByte (string fileName, ref byte[] data )
        {
            
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    fs.Read(data, 0, data.Length);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 读取文件Char型数组
        /// </summary>
        /// <param name="fileName">文件全路径名</param>
        /// <param name="data">接收数组</param>
        /// <returns></returns>
        public static bool ReadChar ( string fileName, ref char[] data )
        {
            byte[] byteData = new byte[data.Length];

            if (!ReadByte(fileName, ref byteData))
            {
                return false;
            }

            Decoder d = Encoding.UTF8.GetDecoder();

            d.GetChars(byteData, 0, byteData.Length, data, 0);

            return true;
        }

        /// <summary>
        /// 以数据流形式逐行读取文件
        /// </summary>
        /// <param name="fileName">文件全路径名</param>
        /// <returns></returns>
        public static IList ReadStream ( string fileName )
        {
            IList list = new ArrayList();

            using (StreamReader read = new StreamReader(fileName))
            {   
                string line;

                while((line = read.ReadLine()) != null)
                {
                    list.Add(line);
                }

                read.Close();
            }
            return list;
        }

        /// <summary>
        /// 文件流读取数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFileSream ( string fileName )
        {
            string result = "";

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                if (fs.CanRead)
                {
                    for (int i = 0; (i = fs.ReadByte()) != -1; i++)
                    {
                        result += (char)i;
                    }
                    fs.Flush();
                }
                return result;
            }
        }

        #endregion

        #region 文件写操作

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName">文件全路径名</param>
        /// <param name="content">写入内容</param>
        /// <param name="fileMode">写入模式</param>
        public static void WriteByte (string fileName, string content, FileMode fileMode)
        {
            if (fileMode == FileMode.Create
                || fileMode == FileMode.Append
                || fileMode == FileMode.Truncate
                || fileMode == FileMode.OpenOrCreate)
            {
                using (FileStream fs = new FileStream(fileName, fileMode))
                {
                    //获得字节数组
                    byte[] data = new UTF8Encoding().GetBytes(content);

                    //开始写入
                    fs.Write(data, 0, data.Length);

                    //清空缓冲区、关闭流
                    fs.Flush();

                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName">文件全路径名</param>
        /// <param name="content">写入内容</param>
        /// <param name="fileMode">写入模式</param>
        public static void WriteStream ( string fileName, string content, FileMode fileMode )
        {
            if (fileMode == FileMode.Create
                || fileMode == FileMode.Append
                || fileMode == FileMode.Truncate
                || fileMode == FileMode.OpenOrCreate)
            {
                using (FileStream fs = new FileStream(fileName, fileMode))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(content);

                        //清空缓冲区、关闭流
                        sw.Flush();

                        sw.Close();
                    }
                    fs.Close();
                }
            }
        }

        #endregion

        #region 文件操作
        /// <summary>
        /// 文件创建
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        /// <param name="overwrite">是否覆盖原文件</param>
        public static void New ( string fileName, bool overwrite = false)
        {
            string path = String.Empty;

            if (!System.IO.File.Exists(fileName) || overwrite == true)
            {
                path = fileName.Substring(0, fileName.LastIndexOf('\\'));

                //如果不存在路径 则需要创建
                DirectoryUtility.New(path);

                //创建文件
                File.Create(fileName);
            }
        }

        /// <summary>
        /// 文件复制
        /// </summary>
        /// <param name="fromName">从文件</param>
        /// <param name="toName">到文件</param>
        /// <param name="overwrite">是否覆盖原文件</param>
        public static bool Copy ( string fromName, string toName, bool overwrite = false)
        {
            try
            {
                System.IO.File.Copy(fromName, toName, overwrite);
            }
            catch 
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 文件删除
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        public static bool Delete ( string fileName )
        {
            try
            {
                System.IO.File.Delete(fileName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Move ( string fromName, string toName, bool overwrite = false )
        {
            try
            {
                //如果覆盖，可以删除现有数据
                if (overwrite)
                {
                    File.Delete(toName);
                }

                System.IO.File.Move(fromName, toName);
            }
            catch
            {
                return false;
            }

            return true;
        }


        #endregion

        #region 文件压缩、解压缩

        public static void Compress ( string fileName, string newName="" )
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile = File.Create((newName == "" ? fi.FullName : newName) + ".gz"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);
                        }
                    }
                }
            }
        }

        public static void Decompress ( string fileName)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);
                    }
                }
            }
        }

        #endregion
    }
}