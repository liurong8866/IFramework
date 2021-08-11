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

using UnityEditor;

namespace IFramework.Editor
{
    public class Menu
    {
        [MenuItem("GameObject/I Kit - Bind", false, 30)]
        private static void UiKitBind()
        {
            UIKit.BindScript();
        }
        [MenuItem("GameObject/I Kit - Add View", false, 31)]
        private static void UiKitAddView()
        {
            
        }
        [MenuItem("GameObject/I Kit - Generate Code", false, 32)]
        private static void UiKitCreateCode()
        {
            
        }
        
        [MenuItem("Assets/I Kit - AssetBundle Mark", false, 120)]
        private static void AssetBundleMark()
        {
        }
        
        [MenuItem("Assets/I Kit - Generate Code", true, 121)]
        private static void AssetCreateCode()
        {
            
        }
        // 控制是否可用
        [MenuItem("Assets/I Kit - Generate Code", false, 121)]
        private static bool AssetCreateCodeValidate()
        {
            return false;
        }
    }
}