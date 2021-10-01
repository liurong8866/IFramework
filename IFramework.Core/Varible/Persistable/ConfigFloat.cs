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

        public ConfigFloat(string key, float value, bool overwrite) : base(key, value) {
            if (overwrite) {
                Save(value);
            }
        }

        public override float Get() {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : value;
        }

        public override void Save(float value) {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        //重载运算符"++"
        public static ConfigFloat operator ++(ConfigFloat m) {
            m.Value = Addition(m.Value, 1.0f);
            return m;
        }

        //重载运算符"--"
        public static ConfigFloat operator --(ConfigFloat m) {
            m.Value = Subtraction(m.Value, 1.0f);
            return m;
        }
    }
}
