using System.Collections.Generic;

namespace IFramework.Editor
{
    public class RootControllerInfo
    {
        public string GameObjectName;

        public Dictionary<string, string> NameToFullNameDic = new Dictionary<string, string>();

        public readonly List<BindInfo> BindInfoList = new List<BindInfo>();

        public readonly List<ElementInfo> ElementInfoList = new List<ElementInfo>();

        public string Identifier { get; set; }

        public bool Changed { get; set; }

        public IEnumerable<string> ForeignKeys { get; private set; }
    }
}
