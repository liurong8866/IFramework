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
using UnityEditor;
using UnityEngine;

namespace IFramework.Test.Viarable
{
    public class ViarableGui : EditorWindow
    {
        private ConfigInt platformIndex;
        private ConfigBool autoGenerateName;
        private ConfigBool isSimulation;
        private bool aotuConst;

        private void Awake()
        {
            
            platformIndex  = new ConfigInt("platformIndex");
            autoGenerateName   = new ConfigBool("autoGenerateName", true);
            isSimulation= new ConfigBool("isSimulation", true);
            aotuConst = autoGenerateName.Value;
        }

        [MenuItem("IFramework/Test/Window")]
        public static void Open()
        {
            ViarableGui window = EditorWindow.GetWindow<ViarableGui>();
            
            window.Show();
        }
        
        private void OnGUI()
        {
            // 选择平台
            platformIndex.Value = GUILayout.Toolbar(platformIndex.Value, new[] {"Window", "MacOS", "iOS", "Android", "WebGL", "PS4", "PS5", "XboxOne"});
            
            GUILayout.Space(10);
            
            // 是否自动生成常量
            autoGenerateName.Value = GUILayout.Toggle(autoGenerateName.Value, "打 AB 包时，自动生成资源名常量代码");
            GUILayout.Space(10);
            
            // 模拟模式
            isSimulation.Value = GUILayout.Toggle(isSimulation.Value, "模拟模式（勾选后每当资源修改时无需再打 AB 包，开发阶段建议勾选，打真机包时取消勾选并打一次 AB 包）");
            
            GUILayout.Space(10);

        }
    }
}