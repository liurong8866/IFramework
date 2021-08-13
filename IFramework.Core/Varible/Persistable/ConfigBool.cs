/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using UnityEditor;
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
            if (overwrite)
            {
                Save(value);
            }
        }
        
        public override bool Get()
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key) == 1 ? true : false;
                
            }
            return this.value;
        }

        public override void Save(bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        
        //重载运算符"true"
        public static bool operator true(ConfigBool value)
        {
            return value.Value;
        }
        
        //重载运算符"false"
        public static bool operator false(ConfigBool value)
        {
            return value.Value;
        }
        
        //重载运算符"!"
        public static bool operator ! (ConfigBool value)
        {
            return !value.Value;
        }
        
        //重载运算符"&"
        public static bool operator & (ConfigBool m, ConfigBool n)
        {
            return m.Value & n.Value;
        }
        
        public static bool operator & (ConfigBool m, bool n)
        {
            return m.Value & n;
        }
        
        public static bool operator & (bool m, ConfigBool n)
        {
            return m & n.Value;
        }
        
        //重载运算符"|"
        public static bool operator | (ConfigBool m, ConfigBool n)
        {
            return m.Value | n.Value;
        }
        
        public static bool operator | (ConfigBool m, bool n)
        {
            return m.Value | n;
        }
        
        public static bool operator | (bool m, ConfigBool n)
        {
            return m | n.Value;
        }
    }
}