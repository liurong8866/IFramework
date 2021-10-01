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
    /// 可持久化的Int类型
    /// </summary>
    public sealed class ConfigInt : AbstractConfigNumeric<int>
    {
        public ConfigInt(string key) : base(key, 0) { }

        public ConfigInt(string key, int value) : base(key, value) { }

        public ConfigInt(string key, int value, bool overwrite) : base(key, value) {
            if (overwrite) {
                Save(value);
            }
        }

        public override int Get() {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : value;
        }

        public override void Save(int value) {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        //重载运算符"++"
        public static ConfigInt operator ++(ConfigInt self) {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static ConfigInt operator --(ConfigInt self) {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }
}
