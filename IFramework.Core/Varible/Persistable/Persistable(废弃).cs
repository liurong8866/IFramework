// /*****************************************************************************
//  * MIT License
//  * 
//  * Copyright (c) 2021 liurong
//  * 
//  * Permission is hereby granted, free of charge, to any person obtaining a copy
//  * of this software and associated documentation files (the "Software"), to deal
//  * in the Software without restriction, including without limitation the rights
//  * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  * copies of the Software, and to permit persons to whom the Software is
//  * furnished to do so, subject to the following conditions:
//  * 
//  * The above copyright notice and this permission notice shall be included in all
//  * copies or substantial portions of the Software.
//  * 
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  * SOFTWARE.
//  *****************************************************************************/
//
// using System;
// using UnityEngine;
//
// namespace IFramework.Core
// {
//     /// <summary>
//     /// 可持久化的变量
//     /// </summary>
//     public abstract class Persistable<T> : AbstractConfigNumeric<T> where T : IConvertible, IComparable
//     {
//         protected string key;
//         
//         public Persistable(string key)
//         {
//             this.key = key;
//         }
//         
//         public Persistable(string key, T value)
//         {
//             this.key = key;
//             this.value = value;
//         }
//         
//         // 获得值
//         protected override T GetValue()
//         {
//             T result = default(T);
//             
//             Type type = typeof(T);
//             
//             if (type == typeof(int))
//             {
//                 if (PlayerPrefs.HasKey(key))
//                 {
//                     result = (T)Convert.ChangeType(PlayerPrefs.GetInt(key),typeof(T));
//                 }
//                 else
//                 {
//                     result =  (T)Convert.ChangeType(0,typeof(T));
//                 }
//             }
//             else if (type == typeof(float))
//             {
//                 if (PlayerPrefs.HasKey(key))
//                 {
//                     result = (T)Convert.ChangeType(PlayerPrefs.GetFloat(key),typeof(T));
//                 }
//                 else
//                 {
//                     result =  (T)Convert.ChangeType(0,typeof(T));
//                 }
//             }
//             else if (type == typeof(string))
//             {
//                 if (PlayerPrefs.HasKey(key))
//                 {
//                     result = (T) Convert.ChangeType(PlayerPrefs.GetString(key), typeof(T));
//                 }
//                 else
//                 {
//                     result =  (T)Convert.ChangeType(string.Empty,typeof(T));
//                 }
//             }
//             else if (type == typeof(bool))
//             {
//                 if (PlayerPrefs.HasKey(key))
//                 {
//                     result = (T) Convert.ChangeType(PlayerPrefs.GetInt(key), typeof(T));
//                 }
//                 else
//                 {
//                     result =  (T)Convert.ChangeType(true,typeof(T));
//                 }
//             }
//
//             return result;
//         }
//
//         // 设置值
//         protected override void SetValue(T value)
//         {
//             if (!IsValueChanged(value)) return;
//
//             setted = true;
//             
//             Type type = typeof(T);
//             
//             if (type == typeof(int))
//             {
//                 PlayerPrefs.SetInt(key, value.ToInt());
//                 
//             }
//             else if (type == typeof(float))
//             {
//                 PlayerPrefs.SetFloat(key, value.ToFloat());
//             }
//             else if (type == typeof(string))
//             {
//                 PlayerPrefs.SetString(key, value.ToString());
//             }
//             else if (type == typeof(bool))
//             {
//                 PlayerPrefs.SetInt(key, Convert.ToBoolean(value) ? 1: 0);
//             }
//             else
//             {
//                 setted = false;
//                 Log.Error("保存失败：仅支持 Int， Float， String 类型的持久化");
//             }
//         }
//     }
//     
//     // 基础类型扩展
//     public class ConfigInt : Persistable<int>
//     {
//         public ConfigInt(string key) : base(key) { }
//         public ConfigInt(string key, int value) : base(key, value) { }
//     }
//     public class ConfigFloat: Persistable<float>{
//         public ConfigFloat(string key) : base(key) { }
//         public ConfigFloat(string key, float value) : base(key, value) { }
//     }
//     public class ConfigString: Persistable<string>{
//         public ConfigString(string key) : base(key) { }
//         public ConfigString(string key, string value) : base(key, value) { }
//     }
//     public class ConfigBool: Persistable<bool>{
//         public ConfigBool(string key) : base(key) { }
//         public ConfigBool(string key, bool value) : base(key, value) { }
//     }
//     
// }


