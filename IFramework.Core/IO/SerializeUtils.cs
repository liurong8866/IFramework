using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace IFramework.Core
{
    public static class SerializeUtils
    {
        /// <summary>
        /// 把对象序列化为字节数组
        /// </summary>
        public static byte[] SerializeToBytes(object obj)
        {
            if (obj == null) {
                Log.Error("序列化失败：需要序列化的对象为NULL");
                return null;
            }
            using MemoryStream memory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memory, obj);
            return memory.ToArray();
        }

        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public static T DeserializeFromBytes<T>(byte[] bytes) where T : class
        {
            if (bytes == null) {
                Log.Error("反序列化失败：需要序列化的字节数组为NULL");
                return null;
            }
            using MemoryStream memory = new MemoryStream(bytes) {
                Position = 0
            };
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(memory) as T;
        }

        /// <summary>
        /// 把文件序列化成对象
        /// </summary>
        public static void SerializeToFile(string fileName, object obj, FileMode fileMode = FileMode.Create)
        {
            using FileStream fileStream = new FileStream(fileName, fileMode);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, obj);
        }

        /// <summary>
        /// 把文件反序列化成对象
        /// </summary>
        public static T DeserializeFromFile<T>(string fileName) where T : class
        {
            using FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(fileStream) as T;
        }

        /// <summary>
        /// 把流反序列化成对象
        /// </summary>
        public static T DeserializeFromStream<T>(Stream stream) where T : class
        {
            if (stream == null) {
                Log.Error("反序列化失败：需要序列化的流为NULL");
                return null;
            }
            if (!stream.CanRead) {
                Log.Error("反序列化失败：需要序列化的流不可读");
                return null;
            }
            using (stream) {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as T;
            }
        }

        /// <summary>
        /// 把对象序列化到文件(AES加密)
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="key">密钥(16位)</param>
        /// <param name="fileMode"></param>
        public static void SerializeToFile(string fileName, object obj, string key, FileMode fileMode = FileMode.Create)
        {
            using AesCryptoServiceProvider crypt = new AesCryptoServiceProvider();
            crypt.Key = Encoding.ASCII.GetBytes(key);
            crypt.IV = Encoding.ASCII.GetBytes(key);
            using ICryptoTransform transform = crypt.CreateEncryptor();
            FileStream fileStream = new FileStream(fileName, fileMode);
            using CryptoStream cryptoStream = new CryptoStream(fileStream, transform, CryptoStreamMode.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(cryptoStream, obj);
        }

        /// <summary>
        /// 把文件反序列化成对象(AES加密)
        /// </summary>
        /// <param name="fileName">待序列化的对象</param>
        /// <param name="key">密钥(16位)</param>
        public static T DeserializeFromFile<T>(string fileName, string key) where T : class
        {
            using AesCryptoServiceProvider crypt = new AesCryptoServiceProvider();
            crypt.Key = Encoding.ASCII.GetBytes(key);
            crypt.IV = Encoding.ASCII.GetBytes(key);
            using ICryptoTransform transform = crypt.CreateDecryptor();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using CryptoStream cryptoStream = new CryptoStream(fileStream, transform, CryptoStreamMode.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(cryptoStream) as T;
        }

        /// <summary>
        /// 把流反序列化成对象(AES加密)
        /// </summary>
        /// <param name="stream">待序列化的文件流</param>
        /// <param name="key">密钥(16位)</param>
        public static T DeserializeFromStream<T>(Stream stream, string key) where T : class
        {
            if (stream == null) {
                Log.Error("反序列化失败：需要序列化的流为NULL");
                return null;
            }
            if (!stream.CanRead) {
                Log.Error("反序列化失败：需要序列化的流不可读");
                return null;
            }
            using AesCryptoServiceProvider crypt = new AesCryptoServiceProvider();
            crypt.Key = Encoding.ASCII.GetBytes(key);
            crypt.IV = Encoding.ASCII.GetBytes(key);
            using ICryptoTransform transform = crypt.CreateDecryptor();
            using CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(cryptoStream) as T;
        }
    }
}
