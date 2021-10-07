using System.Collections.Generic;

namespace IFramework.Editor
{
    public class ElementInfo
    {
        public BindInfo BindInfo;

        public string BehaviourName;

        public Dictionary<string, string> NameToFullNameDic = new Dictionary<string, string>();

        public readonly List<BindInfo> BindInfoList = new List<BindInfo>();

        public readonly List<ElementInfo> ElementInfoList = new List<ElementInfo>();
    }
}
