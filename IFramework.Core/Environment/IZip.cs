using System.Collections.Generic;

namespace IFramework.Core
{
    public interface IZip
    {
        public List<string> GetFileInInner(string fileName);
    }
}
