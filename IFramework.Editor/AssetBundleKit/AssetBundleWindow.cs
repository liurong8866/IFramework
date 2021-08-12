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

using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public class AssetBundleWindow : EditorWindow
    {
        private int platformIndex = 0;
        private bool autoGenerateName = true;
        private bool isSimulation = true;
        
        public static void Open ()
        {       
            //创建窗口
            AssetBundleWindow window = (AssetBundleWindow)EditorWindow.GetWindow<AssetBundleWindow>(true, "资源管理器") ;
            // window.maximized = true;
            window.Show();
        }
        
        public void Awake ()
        {
            if(PlayerPrefs.HasKey("autoGenerateName"))
            {
                autoGenerateName = PlayerPrefs.GetInt("autoGenerateName") ==1;
            }

            if (PlayerPrefs.HasKey("isSimulation"))
            {
                isSimulation = PlayerPrefs.GetInt("isSimulation") == 1;
            }
            
            // 当前平台
            platformIndex = PlatformSettings.GetCurrentPlatform();
        }
 
        //绘制窗口时调用
        void OnGUI () 
        {
            
            GUILayout.Space(20);

            // PersistentPath
            EditorGUILayout.BeginHorizontal();
           
            EditorGUILayout.TextField("PersistentPath:",Application.persistentDataPath);

            if(GUILayout.Button("打开目录",GUILayout.Width(100)))
            {
                EditorUtility.RevealInFinder(Application.persistentDataPath);
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // 选择平台
            platformIndex = GUILayout.Toolbar(platformIndex, new[] {"Window", "MacOS", "iOS", "Android", "WebGL", "PS4", "PS5", "XboxOne"});
            
            //切换当前平台
            PlatformSettings.SetCurrentPlatform(platformIndex);
            
            GUILayout.Space(10);
            
            // 是否自动生成常量
            bool generateName = GUILayout.Toggle(autoGenerateName, "打 AB 包时，自动生成资源名常量代码");
            if (autoGenerateName != generateName)
            {
                autoGenerateName = generateName;
                PlayerPrefs.SetInt("autoGenerateName", autoGenerateName?1:0);
            }
            
            GUILayout.Space(10);
            
            // 模拟模式
            bool simulation = GUILayout.Toggle(isSimulation, "模拟模式（勾选后每当资源修改时无需再打 AB 包，开发阶段建议勾选，打真机包时取消勾选并打一次 AB 包）");
            if (simulation != isSimulation)
            {
                isSimulation = simulation;
                PlayerPrefs.SetInt("isSimulation", isSimulation?1:0);
            }
            
            GUILayout.Space(10);
            
            if(GUILayout.Button("生成 AB 包"))
            {
                AssetBundleBuilder.BuildAssetBundles();
            }
            
            EditorGUILayout.BeginHorizontal();
            
            if(GUILayout.Button("生成 AB 常量"))
            {
                
            }
            if(GUILayout.Button("清空已生成的 AB 包"))
            {
                
            }
            
            EditorGUILayout.EndHorizontal();
            

        }
    }
}