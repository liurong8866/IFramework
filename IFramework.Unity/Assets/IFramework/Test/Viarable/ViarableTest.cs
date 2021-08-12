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
using IFramework.Core;
using IFramework.Test.Model;
using UnityEngine;

namespace IFramework.Test.Viarable
{
    public class ViarableTest : MonoBehaviour
    {
        private Bindable<int> health = new Bindable<int>();

        private BindInt opratorInt = new BindInt();
        private BindFloat opratorFloat = new BindFloat();
        private BindShort opratorShort = new BindShort();
        private BindLong opratorLong = new BindLong();
        private BindDouble opratorDouble = new BindDouble();
        private BindDecimal opratorDecimal = new BindDecimal();

        private ConfigInt loginNum = new ConfigInt("loginNum");
        private ConfigInt taobaoNum = new ConfigInt("taobaoNum");
        private ConfigFloat money = new ConfigFloat("money");
        private ConfigBool isOpen = new ConfigBool("isOpen");
        
        private void Start()
        {
            
            health.OnChange = i =>
            {
                Log.Info("我变了" + i);
            };

            opratorInt.OnChange += i => { opratorInt.LogInfo("opratorInt运算符重载{0}"); };
            opratorFloat.OnChange += i => { opratorFloat.LogInfo("opratorFloat运算符重载{0}"); };
            opratorShort.OnChange += i => { opratorShort.LogInfo("opratorShort运算符重载{0}"); };
            opratorLong.OnChange += i => { opratorLong.LogInfo("opratorLong运算符重载{0}"); };
            opratorDouble.OnChange += i => { opratorDouble.LogInfo("opratorDouble运算符重载{0}"); };
            opratorDecimal.OnChange += i => { opratorDecimal.LogInfo("opratorDecimal运算符重载{0}"); };
            
            health.Value++;
            health.Value++;
            health.Value++;
            health.Value--;
            
            opratorInt.Value = 2;
            
            // Log.Info(opratorInt == 1);
            // Log.Info(opratorInt != 1);
            Log.Info(opratorInt > 1);
            Log.Info(opratorInt < 1);
            Log.Info(opratorInt >= 2);
            Log.Info(opratorInt <= 1);
            
            
            // opratorInt.Value++;
            // opratorInt.Value = opratorInt + opratorInt;
            // opratorInt.Value = opratorInt + 1;
            // opratorInt.Value = 1 + opratorInt;
            //
            // opratorFloat.Value++;
            // opratorFloat.Value = opratorFloat + opratorFloat;
            // opratorFloat.Value = opratorFloat + 1;
            // opratorFloat.Value = 1 + opratorFloat;
            //
            // opratorShort.Value++;
            // opratorShort.Value = opratorShort + opratorShort;
            // opratorShort.Value = opratorShort + 1;
            // opratorShort.Value = 1 + opratorShort;
            //
            // opratorLong.Value++;
            // opratorLong.Value = opratorLong + opratorLong;
            // opratorLong.Value = opratorLong + 1;
            // opratorLong.Value = 1 + opratorLong;
            //
            // opratorDouble.Value++;
            // opratorDouble.Value = opratorDouble + opratorDouble;
            // opratorDouble.Value = opratorDouble + 1;
            // opratorDouble.Value = 1 + opratorDouble;
            //
            // opratorDecimal.Value++;
            // opratorDecimal.Value = opratorDecimal + opratorDecimal;
            // opratorDecimal.Value = opratorDecimal + 1;
            // opratorDecimal.Value = 1 + opratorDecimal;
            //
            //
            // Log.Info(opratorInt == opratorDecimal.ToInt());
            //
            opratorInt.Value--;
            opratorInt.Value = opratorInt - opratorInt;
            opratorInt.Value = opratorInt - 1;
            opratorInt.Value = 1 - opratorInt;
            
            opratorFloat.Value--;
            opratorFloat.Value = opratorFloat - opratorFloat;
            opratorFloat.Value = opratorFloat - 1;
            opratorFloat.Value = 1 - opratorFloat;
            
            opratorShort.Value--;
            opratorShort.Value = opratorShort - opratorShort;
            opratorShort.Value = opratorShort - 1;
            opratorShort.Value = 1 - opratorShort;
            
            opratorLong.Value--;
            opratorLong.Value = opratorLong - opratorLong;
            opratorLong.Value = opratorLong - 1;
            opratorLong.Value = 1 - opratorLong;
            
            opratorDouble.Value--;
            opratorDouble.Value = opratorDouble - opratorDouble;
            opratorDouble.Value = opratorDouble - 1;
            opratorDouble.Value = 1 - opratorDouble;
            
            opratorDecimal.Value--;
            opratorDecimal.Value = opratorDecimal - opratorDecimal;
            opratorDecimal.Value = opratorDecimal - 1;
            opratorDecimal.Value = 1 - opratorDecimal;
            
            
            opratorInt.Value--;
            opratorInt.Value = opratorInt * opratorInt;
            opratorInt.Value = opratorInt * 1;
            opratorInt.Value = 1 * opratorInt;
            
            opratorFloat.Value--;
            opratorFloat.Value = opratorFloat * opratorFloat;
            opratorFloat.Value = opratorFloat * 1;
            opratorFloat.Value = 1 * opratorFloat;
            
            opratorShort.Value--;
            opratorShort.Value = opratorShort * opratorShort;
            opratorShort.Value = opratorShort * 1;
            opratorShort.Value = 1 * opratorShort;
            
            opratorLong.Value--;
            opratorLong.Value = opratorLong * opratorLong;
            opratorLong.Value = opratorLong * 1;
            opratorLong.Value = 1 * opratorLong;
            
            opratorDouble.Value--;
            opratorDouble.Value = opratorDouble * opratorDouble;
            opratorDouble.Value = opratorDouble * 1;
            opratorDouble.Value = 1 * opratorDouble;
            
            opratorDecimal.Value--;
            opratorDecimal.Value = opratorDecimal * opratorDecimal;
            opratorDecimal.Value = opratorDecimal * 1;
            opratorDecimal.Value = 1 * opratorDecimal;
            
            
            opratorInt.Value--;
            opratorInt.Value = opratorInt / opratorInt;
            opratorInt.Value = opratorInt / 1;
            opratorInt.Value = 1 / opratorInt;
            
            opratorFloat.Value--;
            opratorFloat.Value = opratorFloat / opratorFloat;
            opratorFloat.Value = opratorFloat / 1;
            opratorFloat.Value = 1 / opratorFloat;
            
            opratorShort.Value--;
            opratorShort.Value = opratorShort / opratorShort;
            opratorShort.Value = opratorShort / 1;
            opratorShort.Value = 1 / opratorShort;
            
            opratorLong.Value--;
            opratorLong.Value = opratorLong / opratorLong;
            opratorLong.Value = opratorLong / 1;
            opratorLong.Value = 1 / opratorLong;
            
            opratorDouble.Value--;
            opratorDouble.Value = opratorDouble / opratorDouble;
            opratorDouble.Value = opratorDouble / 1;
            opratorDouble.Value = 1 / opratorDouble;
            
            opratorDecimal.Value--;
            opratorDecimal.Value = opratorDecimal / opratorDecimal;
            opratorDecimal.Value = opratorDecimal / 1;
            opratorDecimal.Value = 1 / opratorDecimal;


        
            
            
            // opratorInt++;

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

        

        


        // Bindable<UserInfo> userinfo = new Bindable<UserInfo>();

    }
    
    
}