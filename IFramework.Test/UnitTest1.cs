using System;
using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test {
    public class Tests {

        [SetUp]
        public void Setup() { }

        [Test]
        public void Test1() {
            string fileNameByPath = Platform.GetFileNameByPath("", false);
            fileNameByPath = Platform.GetFileNameByPath("/.", false);
            fileNameByPath = Platform.GetFileNameByPath("/.png", false);
            fileNameByPath = Platform.GetFileNameByPath("/1.png", false);
            fileNameByPath = Platform.GetFileNameByPath("/1.png.txt", false);
            fileNameByPath = Platform.GetFileNameByPath("/1.png/txt", false);
        }

    }
}
