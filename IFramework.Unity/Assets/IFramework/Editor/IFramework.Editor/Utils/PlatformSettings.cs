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

namespace IFramework.Editor
{
    public static class PlatformSettings
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
                e.LogException();
                Log.LogError("未安装当前平台包:" + GetBuildTargetByIndex(platformIndex).ToString());
            }
            
        }
        
        public static BuildTarget GetBuildTargetByIndex(int platformIndex)
        {
            return platformIndex switch
            {
                0 => BuildTarget.StandaloneWindows,
                1 => BuildTarget.StandaloneOSX,
                2 => BuildTarget.iOS,
                3 => BuildTarget.Android,
                4 => BuildTarget.WebGL,
                5 => BuildTarget.PS4,
                6 => BuildTarget.PS5,
                7 => BuildTarget.XboxOne,
                _ => BuildTarget.StandaloneWindows
            };
        }

        public static BuildTarget CurrentBundlePlatform => GetBuildTargetByIndex(Configure.CurrentPlatform.Value);
    }
}