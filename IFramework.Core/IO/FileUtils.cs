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
using System.Net.Mime;
using System.Text;

namespace IFramework.Core
{
    public static class FileUtils
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        public static string Read(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            
            // 读取文件到字节数组
            byte[] data = new byte[fileStream.Length];

            int count = fileStream.Read(data, 0, data.Length);

            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        public static void Write(string path, string content, FileMode fileMode = FileMode.Create)
        {
            using FileStream fileStream = new FileStream(path, fileMode, FileAccess.Write);

            byte[] data = Encoding.UTF8.GetBytes(content);
            
            fileStream.Write(data, 0, data.Length);
            
            fileStream.Flush();
        }
        
        /// <summary>
        /// 分段写入文件，适用于大数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size">缓冲区大小</param>
        /// <returns></returns>
        public static string ReadSeek(string path, int size = 1024)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            
            StringBuilder sb = new StringBuilder();

            // 定义缓冲区，默认1024k
            byte[] buffer = new byte[size];

            // 读取数据的实际长度
            int count = 0;
            
            while ((count = fileStream.Read(buffer, 0, size)) != 0)
            {
                sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            
            return sb.ToString();
            
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        public static void WriteSeek(string path, string content, int size, FileMode fileMode = FileMode.Create)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write);
            
            // 定义缓冲区，默认1024k
            byte[] buffer = new byte[size];
            
            // 读取数据的实际长度
            int count = 0;
            
            while ((count = Encoding.UTF8.GetBytes(content, 0, size, buffer, 1)) != 0)
            {
                fileStream.Write(buffer, 0, count);
                fileStream.Flush();
            }
        }
    }
}