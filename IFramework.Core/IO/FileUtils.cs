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

            while ((count = fileStream.Read(buffer, 0, size)) != 0) { sb.Append(Encoding.UTF8.GetString(buffer, 0, count)); }
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

            while ((count = Encoding.UTF8.GetBytes(content, 0, size, buffer, 1)) != 0) {
                fileStream.Write(buffer, 0, count);
                fileStream.Flush();
            }
        }
    }
}
