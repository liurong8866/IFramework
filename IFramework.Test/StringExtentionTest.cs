using System;
using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test
{
    public class StringExtentionTest
    {
        [Test]
        public void StringTest()
        {
            // string path = "Settings/Environment/Environment.cs";
            //
            // Assert.True("Settings".Equals(path.Left(8)));
            // Assert.True(DirectoryUtils.GetFiles(path).Equals("Settings/Environment"));
            //
            string path = FileUtils.GetFileNameByPath("", false);
            path = FileUtils.GetFileNameByPath("/.", false);
            path = FileUtils.GetFileNameByPath("/.png", false);
            path = FileUtils.GetFileNameByPath("/1.png", false);
            path = FileUtils.GetFileNameByPath("/1.png.txt", false);
            path = FileUtils.GetFileNameByPath("/1.png/txt", false);
            
            string path2 = "Settings/Environment/Environment.cs";
            string searcher = "/Environment";
            //
            path = DirectoryUtils.GetPathByFullName(path2);
            
            path = DirectoryUtils.GetParentPath(path2);
            path = DirectoryUtils.GetLastDirectoryName(path2);
            path = DirectoryUtils.GetPathByFullName(path2);
            //
            path = path2.Left(-1);
            path = path2.Left(0);
            path = path2.Left(3);
            path = path2.Left(100);
            path = path2.Left(100, true);
            //
            path = path2.Left(searcher);
            path = path2.Left(searcher, true);
            path = path2.Left(searcher, false, true);
            path = path2.Left(searcher, true, true);
            //
            path = path2.Left(null);
            path = path2.Left(null, true);
            path = path2.Left(null, false, true);
            path = path2.Left(null, true, true);
            //
            path = path2.Left("");
            path = path2.Left("", true);
            path = path2.Left("", false, true);
            path = path2.Left("", true, true);
            
            //
            path = path2.Right(-1);
            path = path2.Right(0);
            path = path2.Right(3);
            path = path2.Right(100);
            path = path2.Right(100, true);
            //
            path = path2.Right(searcher);
            path = path2.Right(searcher, true);
            path = path2.Right(searcher, false, true);
            path = path2.Right(searcher, true, true);
            //
            path = path2.Right(null);
            path = path2.Right(null, true);
            path = path2.Right(null, false, true);
            path = path2.Right(null, true, true);
            //
            path = path2.Right("");
            path = path2.Right("", true);
            path = path2.Right("", false, true);
            path = path2.Right("", true, true);
            
        }
    }
}
