using System;
using System.IO;
using System.Text;

namespace IFramework.Core
{
    public static class FileUtils
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
        
        /// <summary>
        /// 读取文件
        /// </summary>
        public static string Read(string path)
        {
            return Encoding.UTF8.GetString(ReadByte(path));
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        public static byte[] ReadByte(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            // 读取文件到字节数组
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            return data;
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
        /// 写入文件
        /// </summary>
        public static void Write(string path, byte[] data, FileMode fileMode = FileMode.Create)
        {
            using FileStream fileStream = new FileStream(path, fileMode, FileAccess.Write);
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

            while ((count = fileStream.Read(buffer, 0, size)) != 0) {
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

            while ((count = Encoding.UTF8.GetBytes(content,
                                                   0,
                                                   size,
                                                   buffer,
                                                   1))
                 != 0) {
                fileStream.Write(buffer, 0, count);
                fileStream.Flush();
            }
        }
        
        /// <summary>
        /// 获取资源名称，默认不包含扩展名
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="extend">是否包含扩展名</param>
        /// <returns></returns>
        public static string GetFileNameByPath(string path, bool extend = true)
        {
            path = path.Replace(@"\", "/");

            // 找到最后一个/
            int startIndex = path.LastIndexOf("/", StringComparison.Ordinal) + 1;

            // 如果不需要扩展名，则截取
            if (!extend) {
                // 找到最后一个.
                int length = path.LastIndexOf(".", StringComparison.Ordinal) - startIndex;

                // 如果. 在 / 前面，说明不是后缀扩展名，不处理
                if (length >= 0) {
                    return path.Substring(startIndex, length);
                }
            }
            return path.Substring(startIndex);
        }

    }
}
