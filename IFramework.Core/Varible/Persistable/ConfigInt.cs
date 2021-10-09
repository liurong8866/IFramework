using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的Int类型
    /// </summary>
    public sealed class ConfigInt : AbstractConfigNumeric<int>
    {
        public ConfigInt(string key) : base(key, 0) { }

        public ConfigInt(string key, int value) : base(key, value) { }

        public ConfigInt(string key, int value, bool overwrite) : base(key, value)
        {
            if (overwrite) { Save(value); }
        }

        /// <summary>
        /// 获取整数
        /// </summary>
        public override int Get()
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : value;
        }

        public override void Save(int value)
        {
            PlayerPrefs.SetInt(key, value);
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
        public static ConfigInt operator ++(ConfigInt self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static ConfigInt operator --(ConfigInt self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }
}
