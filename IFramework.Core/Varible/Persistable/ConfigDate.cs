using System;
using UnityEngine;

namespace IFramework.Core
{
    public sealed class ConfigDateTime : AbstractConfig<DateTime>
    {
        private long timestamp;

        public ConfigDateTime(string key) : base(key, DateTime.Now) { }

        public ConfigDateTime(string key, DateTime value) : base(key, value) { }

        public ConfigDateTime(string key, DateTime value, bool overwrite) : base(key, value)
        {
            if (overwrite) {
                Save(value);
            }
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        public override DateTime Get()
        {
            string timeValue = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : "";
            return timeValue.NotEmpty() ? timeValue.ToLong().ToDateTimeByMilliseconds() : value;
        }

        /// <summary>
        /// 保存时间
        /// </summary>
        public override void Save(DateTime value)
        {
            timestamp = value.ToUnixMilliseconds();
            PlayerPrefs.SetString(key, timestamp.ToString());
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 运行秒数
        /// </summary>
        public long DeltaSeconds => DateTime.Now.ToUnixSeconds() - Get().ToUnixSeconds();

        /// <summary>
        /// 运行毫秒数
        /// </summary>
        public long DeltaMilliseconds => DateTime.Now.ToUnixMilliseconds() - Get().ToUnixMilliseconds();

        /// <summary>
        /// 清除缓存
        /// </summary>
        public override void Clear()
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
