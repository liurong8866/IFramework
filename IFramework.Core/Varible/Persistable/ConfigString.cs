using UnityEngine;

namespace IFramework.Core
{
    public sealed class ConfigString : AbstractConfig<string>
    {
        public ConfigString(string key) : base(key, "") { }

        public ConfigString(string key, string value) : base(key, value) { }

        public ConfigString(string key, string value, bool overwrite) : base(key, value)
        {
            if (overwrite) {
                Save(value);
            }
        }

        public override string Get() { return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : value; }

        public override void Save(string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
    }
}
