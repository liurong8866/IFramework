using System;
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 自定义类型配置
    /// </summary>
    public class ConfigGeneric<T> : AbstractConfig<T>
    {
        private Func<T> mValueGetter = null;

        private Action<T> mValueSetter = null;

        public ConfigGeneric(string key, T value) : base(key, value) { }

        public override T Get()
        {
            if (PlayerPrefs.HasKey(key)) {
                return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
            }
            else {
                return value;
            }
        }

        public override void Save(T value)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
            PlayerPrefs.Save();
        }

        public override void Clear()
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
