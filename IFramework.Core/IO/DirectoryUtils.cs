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
            if (!Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        // public static void CreatPath(string path)
        // {
        //     string subPath = "";
        //
        //     string[] direactor = path.Split(Path.PathSeparator);
        //     
        //     foreach(string dir in direactor)
        //     {
        //         subPath = subPath + (subPath==""?"": Path.PathSeparator.ToString()) + dir;
        //
        //         Create(subPath);
        //     }
        // }

        /// <summary>
        /// 删除文件夹，包括其中的文件
        /// </summary>
        public static void Remove(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
        
        /// <summary>
        /// 清空目录（保留路径）
        /// </summary>
        public static void Clear(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
        }
        
        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        public static bool Exists (string path)
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
        public static void Copy (string sourcePath, string destPath, bool recursion = true)
        {
            // 如果数据源路径不存在，则抛出异常
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException(sourcePath);
            }
            
            DirectoryInfo directory = new DirectoryInfo(sourcePath);
            
            //获取目录下所有文件、文件夹
            DirectoryInfo[] directories = directory.GetDirectories();

            // 如果目标文件夹不存在，则创建
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            // 获取当前文件夹下所有文件
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                // 组合文件路径
                string filePath = Path.Combine(destPath, file.Name);

                // 复制文件到指定目录
                file.CopyTo(filePath, false);
            }

            // 如果需要拷贝子目录，则递归
            if (recursion)
            {
                foreach (DirectoryInfo subdir in directories)
                {
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
        /// <param name="isContainsSubfolder">是否包含子文件夹</param>
        /// <returns></returns>
        public static IList<string> GetFiles(string folderPath, bool isContainsSubfolder)
        {
            IList<string> fileList = null;

            if (!isContainsSubfolder)
            {
                fileList = System.IO.Directory.GetFiles(folderPath).ToList<string>();
            }
            else
            {
                fileList = System.IO.Directory.GetFiles(folderPath).ToList<string>();

                //找出所有子文件夹
                string[] folders = System.IO.Directory.GetDirectories(folderPath);

                foreach (string folder in folders)
                {
                    IList<string> filesSub = GetFiles(folder, isContainsSubfolder);

                    fileList = fileList.Concat<string>(filesSub).ToList<string>();
                }
            }

            return fileList;
        }
    }
}