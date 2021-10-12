using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IFramework.Core
{
    public static class DirectoryUtils
    {
        /// <summary>
        /// 创建目录,如果存在则不动作，支持创建多级路径
        /// </summary>
        public static void Create(string path)
        {
            if (!Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 删除文件夹，包括其中的文件
        /// </summary>
        public static void Remove(string path)
        {
            if (Directory.Exists(path)) {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// 清空目录（保留路径）
        /// </summary>
        public static void Clear(string path)
        {
            if (Directory.Exists(path)) {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 合并两个路径
        /// </summary>
        public static string CombinePath(string first, string second)
        {
            return Path.Combine(first, second);
        }

        /// <summary>
        /// 拷贝文件夹及其子目录
        /// </summary>
        /// <param name="sourcePath">数据源</param>
        /// <param name="destPath">目标文件夹</param>
        /// <param name="recursion">是否拷贝子目录</param>
        public static void Copy(string sourcePath, string destPath, bool recursion = true)
        {
            // 如果数据源路径不存在，则抛出异常
            if (!Directory.Exists(sourcePath)) {
                throw new DirectoryNotFoundException(sourcePath);
            }
            DirectoryInfo directory = new DirectoryInfo(sourcePath);

            //获取目录下所有文件、文件夹
            DirectoryInfo[] directories = directory.GetDirectories();

            // 如果目标文件夹不存在，则创建
            if (!Directory.Exists(destPath)) {
                Directory.CreateDirectory(destPath);
            }

            // 获取当前文件夹下所有文件
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files) {
                // 组合文件路径
                string filePath = Path.Combine(destPath, file.Name);

                // 复制文件到指定目录
                file.CopyTo(filePath, false);
            }

            // 如果需要拷贝子目录，则递归
            if (recursion) {
                foreach (DirectoryInfo subdir in directories) {
                    // 组合文件夹路径
                    string dirPath = Path.Combine(destPath, subdir.Name);

                    // 递归调用
                    Copy(subdir.FullName, dirPath, recursion);
                }
            }
        }

        /// <summary>
        /// 获取文件夹下所有文件路径
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="recursion">是否递归</param>
        /// <returns></returns>
        public static List<string> GetFiles(string folderPath, bool recursion = true)
        {
            List<string> fileList;

            if (!recursion) {
                fileList = Directory.GetFiles(folderPath).ToList();
            }
            else {
                fileList = Directory.GetFiles(folderPath).ToList();

                //找出所有子文件夹
                string[] folders = Directory.GetDirectories(folderPath);

                foreach (string folder in folders) {
                    List<string> filesSub = GetFiles(folder, recursion);
                    fileList = fileList.Concat(filesSub).ToList();
                }
            }
            return fileList;
        }

        /// <summary>
        /// 获取文件夹下所有文件路径
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="fileName">要查找的文件名</param>
        /// <param name="recursion">是否递归</param>
        /// <returns></returns>
        public static List<string> GetFiles(string folderPath, string fileName, bool recursion = true)
        {
            List<string> fileList = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(folderPath);

            if (!directory.Exists) {
                Log.Error("文件路径不存在：" + folderPath);
                return null;
            }

            if (directory.Parent != null && directory.Attributes.ToString().IndexOf("System", StringComparison.Ordinal) > -1) {
                return null;
            }
            FileInfo[] fileInfos = directory.GetFiles(fileName);
            fileList.AddRange(fileInfos?.Select(file => file.FullName));

            if (recursion) {
                //找出所有子文件夹
                string[] folders = Directory.GetDirectories(folderPath);

                foreach (string folder in folders) {
                    // 递归调用
                    List<string> filesSub = GetFiles(folder, fileName, recursion);
                    fileList = fileList.Concat(filesSub).ToList();
                }
            }
            return fileList;
        }

        /// <summary>
        /// 获取父目录
        /// </summary>
        /// <param name="path">文件、文件夹路径</param>
        /// <param name="floor">向上第几层</param>
        public static string GetParentPath(string path, int floor = 1)
        {
            string parentPath = path;

            for (int i = 0; i < floor; ++i) {
                int last = parentPath.LastIndexOf('/');
                parentPath = parentPath.Substring(0, last);
            }
            return parentPath;
        }
        
        /// <summary>
        /// 获取文件路径，不包含文件名
        /// </summary>
        public static string GetPathByFullName(string fullName)
        {
            // 找到最后一个/
            return fullName.Substring(0, fullName.Replace(@"\", "/").LastIndexOf("/", StringComparison.Ordinal));
        }
        
        /// <summary>
        /// 查找文件路径的最后一个文件夹名称
        /// </summary>
        /// <param name="fullName">文件全路径</param>
        public static string GetLastDirectoryName(string fullName)
        {
            string[] dirs = fullName.Replace(@"\", "/").Split('/');

            if (dirs.Length < 2) {
                throw new ArgumentException("文件路径格式不正确！" + fullName);
            }

            // 取倒数第二个值
            return dirs[dirs.Length - 2];
        }
    }
}
