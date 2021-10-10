using System.IO;
using UnityEngine;

namespace IFramework.Test.IO
{
    public class IoTest : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            string path = "test1/test2";
            string result = "";
            result = Path.GetExtension(path);
            result = Path.GetFileName(path);
            result = Path.GetFullPath(path);
            result = Path.GetDirectoryName(path);
            result = Path.GetPathRoot(path);
            result = Path.GetTempPath();
            result = Path.GetRandomFileName();
            result = Path.GetFileNameWithoutExtension(path);
            char[] a = Path.GetInvalidPathChars();

            // 如果不是绝对路径，那么创建的项目将在项目根目录
            // DirectoryUtility.New("test1");

            // 直接生成目录
            // DirectoryUtility.New("test1/test2");

            // 递归创建路径
            // DirectoryUtility.NewPath("test1/test2/test");

            // DirectoryUtility.New("Assets/test1/test1/test2/test");

            // DirectoryUtility.New("Assets/test1/aa");

            // DirectoryUtility.New("test1/test2");

            // DirectoryUtility.Remove("test1");

            // DirectoryUtility.Clear("test1/test2");

            // DirectoryUtility.Copy("test1", "test2", true);
        }
    }
}
