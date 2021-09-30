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
using System.Text;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Viarable {
    public class ViarableTest : MonoBehaviour {

        private Bindable<int> health = new Bindable<int>();

        private BindInt bindInt = new BindInt();
        private BindFloat bindFloat = new BindFloat();
        private BindShort bindShort = new BindShort();
        private BindLong bindLong = new BindLong();
        private BindDouble bindDouble = new BindDouble();
        private BindDecimal bindDecimal = new BindDecimal();

        private BindString bindString = new BindString();
        private BindBool bindBool = new BindBool();
        private BindChar bindChar = new BindChar();

        private ConfigInt loginNum = new ConfigInt("loginNum");
        private ConfigInt taobaoNum = new ConfigInt("taobaoNum");
        private ConfigFloat money = new ConfigFloat("money");
        private ConfigBool isOpen = new ConfigBool("isOpen", true);

        private void Start() {
            // ConverterTest();
            // forAdd();

            // ConfigIntTest();
            // ConfigFloatTest();
            ConfigBoolTest();

            // TeatInt();
            // TestIntCompare();
            // TeatFloat();
            // TestFloatCompare();
            // TestString();
            // TestBool();

            // 奇怪的是 float equals检查 int类型 true
            // int aa = 2;
            // Log.Info(aa.Equals(2.0f));
            //
            // float bb = 2.0f;
            // Log.Info(bb.Equals(2));
            //
            // Log.Info(bb.Equals(2.0d));
            //
            // Log.Info(bb.Equals(2f))
        }

        private void ForAdd() {
            int a = 1;
            BindInt b = new BindInt(1);

            // Debug.Log(b++);
            // Debug.Log(b--);
            // Debug.Log(++b);
            // Debug.Log(--b);
            Debug.Log(a++);
            Debug.Log(a--);
            Debug.Log(++a);
            Debug.Log(--a);
            a = b.ToInt();
        }

        /// <summary>
        /// 高精度可以接收低精度 +；
        /// </summary>
        private void ConverterTest() {
            {
                object a1 = 0.0f;
                object a2 = 0.00000d;
                object a3 = 0.000000001d;
                a1.ToDecimal().LogInfo();
                if (a1.ToDecimal() == 0) {
                    Log.Info("hehe");
                }
                a2.ToDecimal().LogInfo();
                if (a2.ToDecimal() == 0) {
                    Log.Info("hehe");
                }
                a3.ToDecimal().LogInfo();
                if (a3.ToDecimal() == 0) {
                    Log.Info("hehe");
                }
                decimal aa1 = 1 / a3.ToDecimal();
                // aa1= 1 / a1.ToDecimal();
                // aa1= 1 / a2.ToDecimal();

                // long aa22 = 0;
                // long aa2 = 10 % aa22;
            }
            ;
            {
                decimal conv = 1234.123456789012345678901234567890m;
                // float conv1 = 1234.123456789012345678901234567890f;
                // double conv2 = 1234.123456789012345678901234567890d;
                // decimal conv3 = 1234.123456789012345678901234567890m;
                int result1 = conv.ToInt();
                short result2 = conv.ToShort();
                long result3 = conv.ToLong();
                float result4 = conv.ToFloat();
                double result5 = conv.ToDouble();
                decimal result6 = conv.ToDecimal();
                int result11 = conv.ToInt();
                short result22 = conv.ToShort();
                long result33 = conv.ToLong();
                float result44 = conv.ToFloat();
                double result55 = conv.ToDouble();
                decimal result66 = conv.ToDecimal();
                result11 = result1 % 10;
                result11 = (int) (result1 % 10.0f);
                result11 = (int) (result1 % 10.0d);
                result11 = (int) (result1 % 10.0m);
                result22 = (short) (result2 % 10);
                result22 = (short) (result2 % 10.10d);
                result22 = (short) (result2 % 10.10d);
                result22 = (short) (result2 % 10.10m);
                result33 = result3 % 10;
                result33 = (long) (result3 % 10.0f);
                result33 = (long) (result3 % 10.0d);
                result33 = (long) (result3 % 10.0m);
                result44 = result4 % 10;
                result44 = result4 % 10.0f;
                result44 = (float) (result4 % 10.0d);
                // result44 = (float)(result4 % 10.0m);  // 未实现 % 
                result55 = result5 % 10;
                result55 = result5 % 10.0f;
                result55 = result5 % 10.0d;
                // result55 = (double)(result5 % 10.0m); // 未实现 % 
                result66 = result6 % 10;
                // result66 = (decimal)(result6 % 10.0f); // 未实现 % 
                // result66 = (decimal)(result6 % 10.0d); // 未实现 % 
                result66 = result6 % 10.0m;
            }
            {
                double d = 500;
                double d1 = 233.84;
                double d2 = d - d1;
                //d2=266.15999999999997
            }
            {
                double d = 0.4;
                double d1 = d + d + d;
                //d1=1.2000000000000002
                double d2 = d * 3;
                //d2=1.2000000000000002
                bindDouble.Value = 1.2d;
                bool aa = bindDouble == 1.2000000000000002d;
            }
            {
                double d = 1.2;
                double d1 = d / 0.4;
                //d1=2.9999999999999996
            }
            int a = 10;
            float b = 10.00001f;
            double c = 10.000000001f;

            // a = a + b;
            // a = a + c;
            b = b + a;
            // b = b + c;
            c = c + a;
            c = c + b;

            // a = a - b;
            // a = a - c;
            b = b - a;
            // b = b - c;
            c = c - a;
            c = c - b;

            // a = a * b;
            // a = a * c;
            b = b * a;
            // b = b * c;
            c = c * a;
            c = c * b;

            // a = a / b;
            // a = a / c;
            b = b / a;
            // b = b / c;
            c = c / a;
            c = c / b;
        }

        private void ConfigIntTest() {
            ConfigInt intNum = new ConfigInt("intNum");
            ConfigInt intNum2 = new ConfigInt("intNum2", 10, true);
            intNum++;
            intNum2++;
            intNum--;
            intNum2--;
            intNum.Value++;
            // intNum = intNum + 10;
            // intNum += 10;
            intNum.LogInfo();
            intNum2.Value++;
            intNum2.LogInfo();
            // intNum2.Value = 2;
            Log.Info(intNum % intNum2);
            Log.Info(20 % intNum);

            // intNum = intNum + intNum2;
            // intNum = intNum2 + 10;
            // intNum = 10 + intNum2;
            //
            // intNum.Value = 100;
            // intNum = intNum - intNum2;
            // intNum = intNum2 - 10;
            // intNum = 10 - intNum2;
            //
            // intNum.Value = 100;
            // intNum = intNum * intNum2;
            // intNum = intNum2 * 10;
            // intNum = 10 * intNum2;
            //
            //
            // intNum.Value = 100;
            // intNum = intNum / intNum2;
            // intNum = intNum2 / 10;
            // intNum = 10 / intNum2;
            //
            // intNum.Value = 100;
            // intNum = intNum % intNum2;
            // intNum = intNum2 % 10;
            // intNum = 10 % intNum2;
            ConfigInt configInt = new ConfigInt("hh");
            configInt.Add(10);
            configInt++;
            int a = configInt + 1;
            int b = configInt + configInt;
            configInt.Add(1).Sub(2).Mul(2).Div(10);
        }

        private void ConfigFloatTest() {
            // AbstractConfigNumeric<int> intNum = new ConfigInt("intNum");
            // AbstractConfigNumeric<int> intNum2 = new ConfigInt("intNum2", 10);
            ConfigFloat intNum = new ConfigFloat("ConfigFloat");
            ConfigFloat intNum2 = new ConfigFloat("ConfigFloat", 10);
            intNum.Value = 100.1234f;
            // intNum = intNum + intNum2;
            // intNum = intNum2 + 10;
            // intNum = 10 + intNum2;
            //
            // intNum.Value = 100.1234f;
            // intNum = intNum - intNum2;
            // intNum = intNum2 - 10;
            // intNum = 10 - intNum2;
            //
            // intNum.Value = 100.1234f;
            // intNum = intNum * intNum2;
            // intNum = intNum2 * 10;
            // intNum = 10 * intNum2;
            //
            // intNum.Value = 100.1234f;
            // intNum = intNum / intNum2;
            // intNum = intNum2 / 10;
            // intNum = 10 / intNum2;
            //
            // intNum.Value = 100;
            // intNum = intNum % intNum2;
            // intNum = intNum2 % 10;
            // intNum = 10 % intNum2;
        }

        private void ConfigBoolTest() {
            ConfigBool configBool = new ConfigBool("configBool", false);
            ConfigBool configBool2 = new ConfigBool("configBool", true);
            configBool.Value.LogInfo("设置前{0}");
            configBool.Value = false;
            configBool.Value.LogInfo("设置后{0}");
            if (configBool) {
                configBool.LogInfo();
            }
            if (!configBool) {
                configBool.LogInfo();
            }
            if (configBool & true) {
                configBool.LogInfo();
            }
            if (true & configBool) {
                configBool.LogInfo();
            }
            if (configBool & configBool2) {
                configBool.LogInfo();
            }
            if (configBool | true) {
                configBool.LogInfo();
            }
            if (true | configBool) {
                configBool.LogInfo();
            }
            if (configBool | configBool2) {
                configBool.LogInfo();
            }
        }

        private void TeatInt() {
            BindInt bindInt = new BindInt(100);
            BindInt bindInt2 = new BindInt(100);
            Log.Info(bindInt);
            Log.Info(bindInt++);
            Log.Info(++bindInt);
            Log.Info(bindInt--);
            Log.Info(--bindInt);
            Log.Info("--------1 + - * / ----------");
            bindInt.Value = 100;
            Log.Info("原数" + bindInt);
            bindInt.Value = bindInt + 10;
            Log.Info(bindInt);
            bindInt.Value = bindInt - 10;
            Log.Info(bindInt);
            bindInt.Value = bindInt * 10;
            Log.Info(bindInt);
            bindInt.Value = bindInt / 10;
            Log.Info(bindInt);
            // bindInt.Value = bindInt / 0;
            Log.Info("--------2 + - * / ----------");
            bindInt.Value = 100;
            Log.Info("原数" + bindInt);
            bindInt.Value = 10 + bindInt;
            Log.Info(bindInt);
            bindInt.Value = 10 - bindInt;
            Log.Info(bindInt);
            bindInt.Value = 10 * bindInt;
            Log.Info(bindInt);
            bindInt.Value = 10 / bindInt;
            Log.Info(bindInt);

            // bindInt.Value = 0;
            // bindInt.Value = 10 / bindInt;
            // Log.Info(bindInt);
            Log.Info("--------3 + - * / ----------");
            bindInt.Value = 100;
            bindInt2.Value = 10;
            Log.Info("原数" + bindInt);
            bindInt.Value = bindInt2 + bindInt;
            Log.Info(bindInt);
            bindInt.Value = bindInt2 - bindInt;
            Log.Info(bindInt);
            bindInt.Value = bindInt2 * bindInt;
            Log.Info(bindInt);
            bindInt.Value = bindInt2 / bindInt;
            Log.Info(bindInt);

            // bindInt.Value = 0;
            // bindInt.Value = bindInt2 / bindInt;
            // Log.Info(bindInt);
            Log.Info("--------4 + - * / ----------");
            bindInt.Value = 100;
            bindInt2.Value = 10;
            Log.Info("原数" + bindInt);
            bindInt.Value = bindInt + bindInt2;
            Log.Info(bindInt);
            bindInt.Value = bindInt - bindInt2;
            Log.Info(bindInt);
            bindInt.Value = bindInt * bindInt2;
            Log.Info(bindInt);
            bindInt.Value = bindInt / bindInt2;
            Log.Info(bindInt);
            bindInt2.Value = 0;
            bindInt.Value = bindInt / bindInt2;
            Log.Info(bindInt);
        }

        private void TestIntCompare() {
            BindInt opratorInt = new BindInt();
            int comp = 2; // 采用基础类型 用于对比
            Log.Info("--------检查同类型数据----------");
            comp = 2; // 用于对比
            opratorInt.Value = 2;
            Log.Info(opratorInt == 2);
            Log.Info(comp == 2);
            Log.Info(opratorInt.Equals(2));
            Log.Info(comp.Equals(2));
            Log.Info(opratorInt != 2);
            Log.Info(comp != 2);
            Log.Info("---------检查不同类型数据---------");
            comp = 2;
            opratorInt.Value = 2;
            Log.Info(opratorInt == 2.0f);
            Log.Info(comp == 2.0f);
            Log.Info(opratorInt.Equals(2.0f));
            Log.Info(comp.Equals(2.0f));
            Log.Info(opratorInt != 2.0f);
            Log.Info(comp != 2.0f);
            Log.Info("--------检查同类型比较 大于----------");
            comp = 4;
            opratorInt.Value = 4;
            Log.Info(opratorInt > 2);
            Log.Info(comp > 2);
            Log.Info(opratorInt >= 2);
            Log.Info(comp >= 2);
            Log.Info(opratorInt < 2);
            Log.Info(comp < 2);
            Log.Info(opratorInt <= 2);
            Log.Info(comp <= 2);
            Log.Info("--------检查同类型比较 小于----------");
            comp = 1;
            opratorInt.Value = 1;
            Log.Info(opratorInt > 2);
            Log.Info(comp > 2);
            Log.Info(opratorInt >= 2);
            Log.Info(comp >= 2);
            Log.Info(opratorInt < 2);
            Log.Info(comp < 2);
            Log.Info(opratorInt <= 2);
            Log.Info(comp <= 2);
            Log.Info("--------检查同类型比较 等于----------");
            comp = 2;
            opratorInt.Value = 2;
            Log.Info(opratorInt > 2);
            Log.Info(comp > 2);
            Log.Info(opratorInt >= 2);
            Log.Info(comp >= 2);
            Log.Info(opratorInt < 2);
            Log.Info(comp < 2);
            Log.Info(opratorInt <= 2);
            Log.Info(comp <= 2);
            Log.Info("--------检查不同类型比较 大于----------");
            comp = 4;
            opratorInt.Value = 4;
            Log.Info(opratorInt > 2.0f);
            Log.Info(comp > 2.0f);
            Log.Info(opratorInt >= 2.0f);
            Log.Info(comp >= 2.0f);
            Log.Info(opratorInt < 2.0f);
            Log.Info(comp < 2.0f);
            Log.Info(opratorInt <= 2.0f);
            Log.Info(comp <= 2.0f);
            Log.Info("--------检查不同类型比较 小于----------");
            comp = 1;
            opratorInt.Value = 1;
            Log.Info(opratorInt > 2.0f);
            Log.Info(comp > 2.0f);
            Log.Info(opratorInt >= 2.0f);
            Log.Info(comp >= 2.0f);
            Log.Info(opratorInt < 2.0f);
            Log.Info(comp < 2.0f);
            Log.Info(opratorInt <= 2.0f);
            Log.Info(comp <= 2.0f);
            Log.Info("--------检查不同类型比较 等于----------");
            comp = 2;
            opratorInt.Value = 2;
            Log.Info(opratorInt > 2.0f);
            Log.Info(comp > 2.0f);
            Log.Info(opratorInt >= 2.0f);
            Log.Info(comp >= 2.0f);
            Log.Info(opratorInt < 2.0f);
            Log.Info(comp < 2.0f);
            Log.Info(opratorInt <= 2.0f);
            Log.Info(comp <= 2.0f);
            Log.Info(opratorInt > 1.9999f);
            Log.Info(comp > 1.9999f);
            Log.Info(opratorInt >= 2.0001f);
            Log.Info(comp >= 2.0001f);
            Log.Info(opratorInt >= 2.0000f);
            Log.Info(comp >= 2.0000f);
        }

        private void TeatFloat() {
            BindFloat bindFloat = new BindFloat(100.00f);
            BindFloat bindFloat2 = new BindFloat(100.00f);
            Log.Info(bindFloat);
            Log.Info("--------1 + - * / ----------");
            bindFloat.Value = 100;
            Log.Info("原数" + bindFloat);
            bindFloat.Value = bindFloat + 10;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat - 10;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat * 10;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat / 10;
            Log.Info(bindFloat);
            // bindFloat.Value = bindFloat / 0;
            // Log.Info(bindFloat);
            Log.Info("--------2 + - * / ----------");
            bindFloat.Value = 100;
            Log.Info("原数" + bindFloat);
            bindFloat.Value = 10 + bindFloat;
            Log.Info(bindFloat);
            bindFloat.Value = 10 - bindFloat;
            Log.Info(bindFloat);
            bindFloat.Value = 10 * bindFloat;
            Log.Info(bindFloat);
            bindFloat.Value = 10 / bindFloat;
            Log.Info(bindFloat);

            // bindFloat.Value = 0;
            // bindFloat.Value = 10 / bindFloat;
            // Log.Info(bindFloat);
            Log.Info("--------3 + - * / ----------");
            bindFloat.Value = 100;
            bindFloat2.Value = 10;
            Log.Info("原数" + bindFloat);
            bindFloat.Value = bindFloat2 + bindFloat;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat2 - bindFloat;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat2 * bindFloat;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat2 / bindFloat;
            Log.Info(bindFloat);

            // bindFloat.Value = 0;
            // bindFloat.Value = bindFloat2 / bindFloat;
            // Log.Info(bindFloat);
            Log.Info("--------4 + - * / ----------");
            bindFloat.Value = 100;
            bindFloat2.Value = 10;
            Log.Info("原数" + bindFloat);
            bindFloat.Value = bindFloat + bindFloat2;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat - bindFloat2;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat * bindFloat2;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat / bindFloat2;
            Log.Info(bindFloat);

            // bindFloat2.Value = 0;
            // bindFloat.Value = bindFloat / bindFloat2;
            // Log.Info(bindFloat);
            Log.Info("--------5 + - * / ----------");
            bindFloat.Value = 100.1003f;
            bindFloat2.Value = 10.0123f;
            Log.Info("原数" + bindFloat);
            bindFloat.Value = bindFloat + bindFloat2;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat - bindFloat2;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat * bindFloat2;
            Log.Info(bindFloat);
            bindFloat.Value = bindFloat / bindFloat2;
            Log.Info(bindFloat);
            bindFloat2.Value = 0;
            bindFloat.Value = bindFloat / bindFloat2;
            Log.Info(bindFloat);
        }

        private void TestFloatCompare() {
            BindFloat bindFloat = new BindFloat();
            float comp = 2; // 采用基础类型 用于对比
            Log.Info("--------检查同类型数据----------");
            comp = 2.2f; // 用于对比
            bindFloat.Value = 2.2f;
            Log.Info(bindFloat == 2.2f);
            Log.Info(comp == 2.2f);
            Log.Info(bindFloat.Equals(2.2f));
            Log.Info(comp.Equals(2.2f));
            Log.Info(bindFloat != 2.2f);
            Log.Info(comp != 2.2f);
            Log.Info("---------检查不同类型数据---------");
            comp = 2.0f;
            bindFloat.Value = 2.0f;
            Log.Info(bindFloat == 2);
            Log.Info(comp == 2);

            // TODO  奇怪的是 float equals检查 int类型 true, 源码逻辑不一样！！
            Log.Info(bindFloat.Equals(2));
            Log.Info(comp.Equals(2));
            Log.Info("------------------");
            Log.Info(bindFloat != 2);
            Log.Info(comp != 2);
            Log.Info(bindFloat == 2.2d);
            Log.Info(comp == 2.2d);
            Log.Info(bindFloat.Equals(2.2d));
            Log.Info(comp.Equals(2.2d));
            Log.Info(bindFloat != 2.2d);
            Log.Info(comp != 2.2d);
            Log.Info("--------检查同类型比较 大于----------");
            comp = 4.5f;
            bindFloat.Value = 4.5f;
            Log.Info(bindFloat > 2.2f);
            Log.Info(comp > 2.2f);
            Log.Info(bindFloat >= 2.2f);
            Log.Info(comp >= 2.2f);
            Log.Info(bindFloat < 2.2f);
            Log.Info(comp < 2.2f);
            Log.Info(bindFloat <= 2.2f);
            Log.Info(comp <= 2.2f);
            Log.Info("--------检查同类型比较 小于----------");
            comp = 1.9f;
            bindFloat.Value = 1.9f;
            Log.Info(bindFloat > 2.2f);
            Log.Info(comp > 2.2f);
            Log.Info(bindFloat >= 2.2f);
            Log.Info(comp >= 2.2f);
            Log.Info(bindFloat < 2.2f);
            Log.Info(comp < 2.2f);
            Log.Info(bindFloat <= 2.2f);
            Log.Info(comp <= 2.2f);
            Log.Info("--------检查同类型比较 等于----------");
            comp = 2.2f;
            bindFloat.Value = 2.2f;
            Log.Info(bindFloat > 2.2f);
            Log.Info(comp > 2.2f);
            Log.Info(bindFloat >= 2.2f);
            Log.Info(comp >= 2.2f);
            Log.Info(bindFloat < 2.2f);
            Log.Info(comp < 2.2f);
            Log.Info(bindFloat <= 2.2f);
            Log.Info(comp <= 2.2f);
            Log.Info("--------检查不同类型比较 大于----------");
            comp = 4f;
            bindFloat.Value = 4f;
            Log.Info(bindFloat > 2.0f);
            Log.Info(comp > 2.0f);
            Log.Info(bindFloat >= 2.0f);
            Log.Info(comp >= 2.0f);
            Log.Info(bindFloat < 2.0f);
            Log.Info(comp < 2.0f);
            Log.Info(bindFloat <= 2.0f);
            Log.Info(comp <= 2.0f);
            Log.Info("--------检查不同类型比较 小于----------");
            comp = 1f;
            bindFloat.Value = 1f;
            Log.Info(bindFloat > 2.0f);
            Log.Info(comp > 2.0f);
            Log.Info(bindFloat >= 2.0f);
            Log.Info(comp >= 2.0f);
            Log.Info(bindFloat < 2.0f);
            Log.Info(comp < 2.0f);
            Log.Info(bindFloat <= 2.0f);
            Log.Info(comp <= 2.0f);
            Log.Info("--------检查不同类型比较 等于----------");
            comp = 2.0f;
            bindFloat.Value = 2.0f;
            Log.Info(bindFloat > 2.0f);
            Log.Info(comp > 2.0f);
            Log.Info(bindFloat >= 2.0f);
            Log.Info(comp >= 2.0f);
            Log.Info(bindFloat < 2.0f);
            Log.Info(comp < 2.0f);
            Log.Info(bindFloat <= 2.0f);
            Log.Info(comp <= 2.0f);
            Log.Info(bindFloat > 1.9999f);
            Log.Info(comp > 1.9999f);
            Log.Info(bindFloat >= 2.0001f);
            Log.Info(comp >= 2.0001f);
            Log.Info(bindFloat >= 2.0000f);
            Log.Info(comp >= 2.0000f);
        }

        private void TestString() {
            string a = "hello world";
            string b = "hello world";
            string c = new StringBuilder("hello world").ToString();
            char[] cTemp = { 'h', 'e', 'l', 'o', ' ', 'w', 'o', 'r', 'l', 'd' };
            String d = new String(cTemp);
            BindString bindString1 = new BindString("hello world");
            BindString bindString2 = new BindString("hello world");
            Log.Info("1--------------");
            Log.Info(a == b);
            Log.Info(bindString1 == b);
            Log.Info(a.Equals(b));
            Log.Info(bindString1.Equals(b));
            Log.Info("2--------------");
            Log.Info(a == c);
            Log.Info(bindString1 == c);
            Log.Info(a.Equals(c));
            Log.Info(bindString1.Equals(c));
            Log.Info("3--------------");
            Log.Info(a == d);
            Log.Info(bindString1 == d);
            Log.Info(a.Equals(d));
            Log.Info(bindString1.Equals(d));
            Log.Info("4--------------");
            Log.Info(a == bindString2);
            Log.Info(a.Equals(bindString2));
            Log.Info(bindString1 == bindString2);
            Log.Info(bindString1.Equals(bindString2));
            Log.Info("5--------------");
            // Log.Info(a == a);
            // Log.Info(bindString1 == bindString1);
            // Log.Info(a.Equals(a));
            Log.Info(bindString1.Equals(bindString1));
            Log.Info("6--------------");
            Log.Info(a == bindString1);
            Log.Info(a.Equals(bindString1));
            Log.Info(bindString1.Equals(a));
        }

        private void TestBool() {
            BindBool bindBool = new BindBool(true);
            BindBool bindBool2 = new BindBool(true);
            BindBool bindBool3 = new BindBool(false);
            bindBool.Value.LogInfo("设置前{0}");
            bindBool.Value = false;
            bindBool.Value.LogInfo("设置后{0}");
            if (bindBool == true) {
                Debug.Log("1");
            }
            if (bindBool == false) {
                Debug.Log("2");
            }
            if (true == bindBool) {
                Debug.Log("3");
            }
            if (false == bindBool) {
                Debug.Log("4");
            }
            if (bindBool == bindBool2) {
                Debug.Log("5");
            }
            if (bindBool == bindBool3) {
                Debug.Log("6");
            }
            if (bindBool.Equals(true)) {
                Debug.Log("7");
            }
            if (true.Equals(bindBool)) {
                Debug.Log("8");
            }
        }

        private void TestConfigNumber() {
            health.OnChange = i => { Log.Info("我变了" + i); };
            health.Value++;
            health.Value++;
            health.Value++;
            health.Value--;
            loginNum.Value.LogInfo();
            loginNum.Value++;
            loginNum.Value++;
            loginNum.Value.LogInfo();
            taobaoNum.Value++;
            Log.Info("淘宝数量：" + taobaoNum.Value);
            Log.Info("淘宝数量：" + taobaoNum.Value);
            money.Value++;
            money.LogInfo("支付宝钱：{0}");
            isOpen.Value = true;
            isOpen.Value.LogInfo();
        }

    }
}
