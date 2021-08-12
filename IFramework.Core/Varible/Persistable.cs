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

using System;
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的变量
    /// </summary>
    public class Persistable<T> : Property<T>
    {
        protected string key;
        
        protected T defaultValue;
        
        public Persistable()
        {
            this.key = typeof(T).GetHashCode().ToString();
        }

        public Persistable(string key)
        {
            this.key = key;
        }
        
        public Persistable(string key, T value)
        {
            this.key = key;
            this.defaultValue = value;
        }
        
        protected override T GetValue()
        {
            T result = defaultValue;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                if (PlayerPrefs.HasKey(key))
                {
                    result = (T)Convert.ChangeType(PlayerPrefs.GetInt(key),typeof(T));
                }
                else
                {
                    result =  (T)Convert.ChangeType(0,typeof(T));
                }
            }
            else if (type == typeof(float))
            {
                if (PlayerPrefs.HasKey(key))
                {
                    result = (T)Convert.ChangeType(PlayerPrefs.GetFloat(key),typeof(T));
                }
                else
                {
                    result =  (T)Convert.ChangeType(0,typeof(T));
                }
            }
            else if (type == typeof(string))
            {
                if (PlayerPrefs.HasKey(key))
                {
                    result = (T) Convert.ChangeType(PlayerPrefs.GetString(key), typeof(T));
                }
                else
                {
                    result =  (T)Convert.ChangeType(string.Empty,typeof(T));
                }
            }

            return result;
        }

        protected override void SetValue(T value)
        {
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                PlayerPrefs.SetInt(key, value.ToInt());
            }
            else if (type == typeof(float))
            {
                PlayerPrefs.SetFloat(key, value.ToFloat());
            }
            else if (type == typeof(string))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }
        }
    }
}