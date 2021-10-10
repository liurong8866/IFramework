using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的Float类型
    /// </summary>
    public sealed class ConfigFloat : AbstractConfigNumeric<float>
    {
        public ConfigFloat(string key) : base(key, 0.0f) { }

        public ConfigFloat(string key, float value) : base(key, value) { }

        public ConfigFloat(string key, float value, bool overwrite) : base(key, value)
        {
            if (overwrite) {
                Save(value);
            }
        }

        /// <summary>
        /// 获取浮点数值
        /// </summary>
        public override float Get()
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : value;
        }

        public override void Save(float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public override void Clear()
        {
            PlayerPrefs.DeleteKey(key);
        }

        //重载运算符"++"
        public static ConfigFloat operator ++(ConfigFloat m)
        {
            m.Value = Addition(m.Value, 1.0f);
            return m;
        }

        //重载运算符"--"
        public static ConfigFloat operator --(ConfigFloat m)
        {
            m.Value = Subtraction(m.Value, 1.0f);
            return m;
        }
    }
}
