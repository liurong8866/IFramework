using UnityEngine;

namespace IFramework.Core
{
    public sealed class ConfigString : AbstractConfig<string>
    {
        public ConfigString(string key) : base(key, "") { }

        public ConfigString(string key, string value) : base(key, value) { }

        public ConfigString(string key, string value, bool overwrite) : base(key, value)
        {
            if (overwrite) { Save(value); }
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        public override string Get()
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : value;
        }

        public override void Save(string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// 清除缓存
        /// </summary>
        public override void Clear()
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
