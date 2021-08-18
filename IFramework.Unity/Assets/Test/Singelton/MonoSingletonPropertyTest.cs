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
using IFramework.Core;

namespace IFramework.Test.Singelton
{
    
    public class MonoSingletonPropertyTest : MonoBehaviour
    {
        private void Start()
        {
            MonoSingletonPropertyTestDemo a = MonoSingletonPropertyTestDemo.Instance; 
            MonoSingletonPropertyTestDemo b = MonoSingletonPropertyTestDemo.Instance;
            
            Debug.Log(a == b);
            Debug.Log(a.GetHashCode());
            Debug.Log(b.GetHashCode());
            
            MonoSingletonPropertyTestDemo.Instance.Say();
            MonoSingletonPropertyTestDemo.Instance.Say2();
        }
    }
    
    public class MonoSingletonPropertyTestDemo :  Aaa, ISingleton
    {
        // 私有化构造函数，防止外部New创建
        private MonoSingletonPropertyTestDemo(){}

        public static MonoSingletonPropertyTestDemo Instance
        {
            get
            {
                return MonoSingletonProperty<MonoSingletonPropertyTestDemo>.Instance;
            }
        }
        
        public void Say()
        {
            Debug.Log("hello world");
        }
        
        public void OnInit()
        {
            Debug.Log("这是单例初始化");
        }
        
    }

    public class Aaa : MonoBehaviour
    {
        public void Say2()
        {
            Debug.Log("我是AAA");
        } 
    }
}