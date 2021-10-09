using System.IO;

namespace IFramework.Core
{
    // ReSharper disable once InconsistentNaming
    public static class IOExtension
    {
        /// <summary>
        /// 创建目录,如果存在则不动作，支持创建多级路径
        /// </summary>
        public static string Create(this string path)
        {
            DirectoryUtils.Create(path);
            return path;
        }

        /// <summary>
        /// 删除文件夹，包括其中的文件
        /// </summary>
        public static void Remove(this string path)
        {
            DirectoryUtils.Remove(path);
        }

        /// <summary>
        /// 清空目录（保留路径）
        /// </summary>
        public static void Clear(string path)
        {
            DirectoryUtils.Clear(path);
        }

        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        public static bool Exists(string path)
        {
            return DirectoryUtils.Exists(path);
        }

        /// <summary>
        /// 合并两个路径
        /// </summary>
        public static string CombinePath(this string self, string path)
        {
            return DirectoryUtils.CombinePath(self, path);
        }

        /// <summary>
        /// 把对象序列化为字节数组
        /// </summary>
        public static byte[] SerializeToBytes(this object obj)
        {
            return SerializeUtils.SerializeToBytes(obj);
        }

        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public static T DeserializeFromBytes<T>(byte[] bytes) where T : class
        {
            return SerializeUtils.DeserializeFromBytes<T>(bytes);
        }
        
        /// <summary>
        /// 把文件序列化成对象
        /// </summary>
        public static void SerializeToFile(string fileName, object obj, FileMode fileMode = FileMode.Create)
        {
            SerializeUtils.SerializeToFile(fileName, obj, fileMode);
        }

        /// <summary>
        /// 把文件反序列化成对象
        /// </summary>
        public static T DeserializeFromFile<T>(string fileName) where T : class
        {
            return SerializeUtils.DeserializeFromFile<T>(fileName);
        }

        /// <summary>
        /// 把流反序列化成对象
        /// </summary>
        public static T DeserializeFromStream<T>(Stream stream) where T : class
        {
            return SerializeUtils.DeserializeFromStream<T>(stream);
        }

        /// <summary>
        /// 把对象序列化到文件(AES加密)
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="key">密钥(16位)</param>
        /// <param name="fileMode"></param>
        public static void SerializeToFile(this string fileName, object obj, string key, FileMode fileMode = FileMode.Create)
        {
            SerializeUtils.SerializeToFile(fileName, obj, key, fileMode);
        }

        /// <summary>
        /// 把文件反序列化成对象(AES加密)
        /// </summary>
        /// <param name="fileName">待序列化的对象</param>
        /// <param name="key">密钥(16位)</param>
        public static T DeserializeFromFile<T>(string fileName, string key) where T : class
        {
            return SerializeUtils.DeserializeFromFile<T>(fileName, key);
        }
        
        /// <summary>
        /// 把流反序列化成对象(AES加密)
        /// </summary>
        /// <param name="stream">待序列化的文件流</param>
        /// <param name="key">密钥(16位)</param>
        public static T DeserializeFromStream<T>(Stream stream, string key) where T : class
        {
            return SerializeUtils.DeserializeFromStream<T>(stream, key);
        }
    }
}
