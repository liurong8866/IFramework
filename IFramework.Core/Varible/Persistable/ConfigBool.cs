using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的Bool类型
    /// </summary>
    public sealed class ConfigBool : AbstractConfig<bool>
    {
        public ConfigBool(string key, bool value) : base(key, value) { }

        public ConfigBool(string key, bool value, bool overwrite) : base(key, value)
        {
            if (overwrite) {
                Save(value);
            }
        }

        public override bool Get()
        {
            if (PlayerPrefs.HasKey(key)) {
                return PlayerPrefs.GetInt(key) == 1;
            }
            return value;
        }

        public override void Save(bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        //重载运算符"true"
        public static bool operator true(ConfigBool value) { return value.Value; }

        //重载运算符"false"
        public static bool operator false(ConfigBool value) { return value.Value; }

        //重载运算符"!"
        public static bool operator !(ConfigBool value) { return !value.Value; }

        //重载运算符"&"
        public static bool operator &(ConfigBool m, ConfigBool n) { return m.Value & n.Value; }

        public static bool operator &(ConfigBool m, bool n) { return m.Value & n; }

        public static bool operator &(bool m, ConfigBool n) { return m & n.Value; }

        //重载运算符"|"
        public static bool operator |(ConfigBool m, ConfigBool n) { return m.Value | n.Value; }

        public static bool operator |(ConfigBool m, bool n) { return m.Value | n; }

        public static bool operator |(bool m, ConfigBool n) { return m | n.Value; }
    }
}
