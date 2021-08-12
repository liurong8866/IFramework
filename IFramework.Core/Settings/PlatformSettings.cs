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
using UnityEditor;

namespace IFramework.Core
{
    public class PlatformSettings
    {
        public static int GetCurrentPlatform()
        {
            int platformIndex = 0;
            
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneWindows:
                    platformIndex = 0;
                    break;
                case BuildTarget.StandaloneOSX:
                    platformIndex = 1;
                    break;
                case BuildTarget.iOS:
                    platformIndex = 2;
                    break;
                case BuildTarget.Android:
                    platformIndex = 3;
                    break;
                case BuildTarget.WebGL:
                    platformIndex = 4;
                    break;
                case BuildTarget.PS4:
                    platformIndex = 5;
                    break;
                case BuildTarget.PS5:
                    platformIndex = 6;
                    break;
                case BuildTarget.XboxOne:
                    platformIndex = 7;
                    break;
                default:
                    platformIndex = 0;
                    break;
            }

            return platformIndex;
        }

        public static void SetCurrentPlatform(int platformIndex)
        {
            try
            {
                switch (platformIndex)
                {
                    case 0:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
                        break;
                    case 1:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
                        break;
                    case 2:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
                        break;
                    case 3:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                        break;
                    case 4:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
                        break;
                    case 5:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.PS4, BuildTarget.PS4);
                        break;
                    case 6:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.PS5, BuildTarget.PS5);
                        break;
                    case 7:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.XboxOne, BuildTarget.XboxOne);
                        break;
                    default:
                        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
                        break;
                }
            }
            catch (Exception e)
            {
                // e.LogException();
                Log.LogError("未安装当前平台包:" + GetPlatformName(platformIndex));
            }
            
        }
        
        public static string GetPlatformName(int platformIndex)
        {
            switch (platformIndex)
            {
                case 0: return BuildTarget.StandaloneWindows.ToString();
                case 1: return BuildTarget.StandaloneOSX.ToString();
                case 2: return BuildTarget.iOS.ToString();
                case 3: return BuildTarget.Android.ToString();
                case 4: return BuildTarget.WebGL.ToString();
                case 5: return BuildTarget.PS4.ToString();
                case 6: return BuildTarget.PS5.ToString();
                case 7: return BuildTarget.XboxOne.ToString();
                default: return BuildTarget.StandaloneWindows.ToString();
            }
        }

        public static string CurrentPlatformName
        {
            get
            {
                string name = EditorUserBuildSettings.activeBuildTarget.ToString();
                return name;
            }
        }
    }
}