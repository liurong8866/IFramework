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
    /// Transform 扩展方法
    /// </summary>
    public static class TransformExtension
    {
        /* Example
        public static void Example()
        {
            var selfScript = new GameObject().AddComponent<MonoBehaviour>();
            var transform = selfScript.transform;

            transform
                // .Parent(null)
                .LocalIdentity()
                .LocalPositionIdentity()
                .LocalRotationIdentity()
                .LocalScaleIdentity()
                .LocalPosition(Vector3.zero)
                .LocalPosition(0, 0, 0)
                .LocalPosition(0, 0)
                .LocalPositionX(0)
                .LocalPositionY(0)
                .LocalPositionZ(0)
                .LocalRotation(Quaternion.identity)
                .LocalScale(Vector3.one)
                .LocalScaleX(1.0f)
                .LocalScaleY(1.0f)
                .Identity()
                .PositionIdentity()
                .RotationIdentity()
                .Position(Vector3.zero)
                .PositionX(0)
                .PositionY(0)
                .PositionZ(0)
                .Rotation(Quaternion.identity)
                .DestroyChildren()
                .AsLastSibling()
                .AsFirstSibling()
                .SiblingIndex(0);

            selfScript
                // .Parent(null)
                .LocalIdentity()
                .LocalPositionIdentity()
                .LocalRotationIdentity()
                .LocalScaleIdentity()
                .LocalPosition(Vector3.zero)
                .LocalPosition(0, 0, 0)
                .LocalPosition(0, 0)
                .LocalPositionX(0)
                .LocalPositionY(0)
                .LocalPositionZ(0)
                .LocalRotation(Quaternion.identity)
                .LocalScale(Vector3.one)
                .LocalScaleX(1.0f)
                .LocalScaleY(1.0f)
                .Identity()
                .PositionIdentity()
                .RotationIdentity()
                .Position(Vector3.zero)
                .PositionX(0)
                .PositionY(0)
                .PositionZ(0)
                .Rotation(Quaternion.identity)
                .DestroyChildren()
                .AsLastSibling()
                .AsFirstSibling()
                .SiblingIndex(0);
        }

        */
        
        /// <summary>
        /// 设置父节点
        /// </summary>
        public static T Parent<T>(this T self, Component parent) where T : Component
        {
            self.transform.SetParent(parent == null ? null : parent.transform);
            return self;
        }

        /// <summary>
        /// 设置为根节点
        /// </summary> 
        public static T AsRoot<T>(this T self) where T : Component
        {
            self.transform.SetParent(null);
            return self;
        }
        
        /*----------------------------*/
        /* LocalIdentity              */
        /*----------------------------*/
        
        public static T LocalIdentity<T>(this T self) where T : Component
        {
            self.transform.localPosition = Vector3.zero;
            self.transform.localRotation = Quaternion.identity;
            self.transform.localScale = Vector3.one;
            return self;
        }
        
        /*----------------------------*/
        /* LocalPosition              */
        /*----------------------------*/
        
        public static T LocalPosition<T>(this T self, Vector3 position) where T : Component
        {
            self.transform.localPosition = position;
            return self;
        }

        public static T LocalPosition<T>(this T self, float x, float y, float z) where T : Component
        {
            self.transform.localPosition = new Vector3(x, y, z);
            return self;
        }

        public static T LocalPosition<T>(this T self, float x, float y) where T : Component
        {
            Vector3 position = self.transform.localPosition;
            position.x = x;
            position.y = y;
            self.transform.localPosition = position;
            return self;
        }

        public static T LocalPositionX<T>(this T self, float x) where T : Component
        {
            Vector3 position = self.transform.localPosition;
            position.x = x;
            self.transform.localPosition = position;
            return self;
        }

        public static T LocalPositionY<T>(this T self, float y) where T : Component
        {
            Vector3 position = self.transform.localPosition;
            position.y = y;
            self.transform.localPosition = position;
            return self;
        }

        public static T LocalPositionZ<T>(this T self, float z) where T : Component
        {
            Vector3 position = self.transform.localPosition;
            position.z = z;
            self.transform.localPosition = position;
            return self;
        }
        
        public static T LocalPositionIdentity<T>(this T self) where T : Component
        {
            self.transform.localPosition = Vector3.zero;
            return self;
        }

        /*----------------------------*/
        /* LocalRotation              */
        /*----------------------------*/

        public static T LocalRotation<T>(this T self, Quaternion localRotation) where T : Component
        {
            self.transform.localRotation = localRotation;
            return self;
        }

        public static T LocalRotationIdentity<T>(this T self) where T : Component
        {
            self.transform.localRotation = Quaternion.identity;
            return self;
        }

        /*----------------------------*/
        /* LocalScale                 */
        /*----------------------------*/

        public static T LocalScale<T>(this T self, Vector3 scale) where T : Component
        {
            self.transform.localScale = scale;
            return self;
        }

        public static T LocalScale<T>(this T self, float scale) where T : Component
        {
            self.transform.localScale = Vector3.one * scale;
            return self;
        }

        public static T LocalScale<T>(this T self, float x, float y, float z) where T : Component
        {
            self.transform.localScale = new Vector3(x, y, z);
            return self;
        }

        public static T LocalScale<T>(this T self, float x, float y) where T : Component
        {
            Vector3 scale = self.transform.localScale;
            scale.x = x;
            scale.y = y;
            self.transform.localScale = scale;
            return self;
        }

        public static T LocalScaleX<T>(this T self, float x) where T : Component
        {
            Vector3 scale = self.transform.localScale;
            scale.x = x;
            self.transform.localScale = scale;
            return self;
        }

        public static T LocalScaleY<T>(this T self, float y) where T : Component
        {
            Vector3 scale = self.transform.localScale;
            scale.y = y;
            self.transform.localScale = scale;
            return self;
        }

        public static T LocalScaleZ<T>(this T self, float z) where T : Component
        {
            Vector3 scale = self.transform.localScale;
            scale.z = z;
            self.transform.localScale = scale;
            return self;
        }

        public static T LocalScaleIdentity<T>(this T self) where T : Component
        {
            self.transform.localScale = Vector3.one;
            return self;
        }
        
        /*----------------------------*/
        /* Identity                   */
        /*----------------------------*/

        public static T Identity<T>(this T self) where T : Component
        {
            self.transform.position = Vector3.zero;
            self.transform.rotation = Quaternion.identity;
            self.transform.localScale = Vector3.one;
            return self;
        }
        
        public static T Identity<T>(this T self, Transform transform) where T : Component
        {
            self.transform.SetParent(transform.parent);
            self.transform.localPosition = transform.localPosition;
            self.transform.localRotation = transform.localRotation;
            self.transform.localScale = transform.localScale;

            return self;
        }

        /*----------------------------*/
        /* Position                   */
        /*----------------------------*/
        
        public static T Position<T>(this T self, Vector3 position) where T : Component
        {
            self.transform.position = position;
            return self;
        }

        public static T Position<T>(this T self, float x, float y, float z) where T : Component
        {
            self.transform.position = new Vector3(x, y, z);
            return self;
        }

        public static T Position<T>(this T self, float x, float y) where T : Component
        {
            Vector3 position = self.transform.position;
            position.x = x;
            position.y = y;
            self.transform.position = position;
            return self;
        }

        public static T PositionX<T>(this T self, float x) where T : Component
        {
            Vector3 position = self.transform.position;
            position.x = x;
            self.transform.position = position;
            return self;
        }

        public static T PositionX<T>(this T self, Func<float, float> xSetter) where T : Component
        {
            Vector3 position = self.transform.position;
            position.x = xSetter(position.x);
            self.transform.position = position;
            return self;
        }

        public static T PositionY<T>(this T self, float y) where T : Component
        {
            Vector3 position = self.transform.position;
            position.y = y;
            self.transform.position = position;
            return self;
        }

        public static T PositionY<T>(this T self, Func<float, float> ySetter) where T : Component
        {
            Vector3 position = self.transform.position;
            position.y = ySetter(position.y);
            self.transform.position = position;
            return self;
        }

        public static T PositionZ<T>(this T self, float z) where T : Component
        {
            Vector3 position = self.transform.position;
            position.z = z;
            self.transform.position = position;
            return self;
        }

        public static T PositionZ<T>(this T self, Func<float, float> zSetter) where T : Component
        {
            Vector3 position = self.transform.position;
            position.z = zSetter(position.z);
            self.transform.position = position;
            return self;
        }

        public static T PositionIdentity<T>(this T self) where T : Component
        {
            self.transform.position = Vector3.zero;
            return self;
        }

        /*----------------------------*/
        /* Rotation                   */
        /*----------------------------*/
        
        public static T Rotation<T>(this T self, Quaternion rotation) where T : Component
        {
            self.transform.rotation = rotation;
            return self;
        }
        
        public static T RotationIdentity<T>(this T self) where T : Component
        {
            self.transform.rotation = Quaternion.identity;
            return self;
        }
        
        /*----------------------------*/
        /* Sibling                    */
        /*----------------------------*/

        /// <summary>
        /// 设置为最底层
        /// </summary>
        public static T AsLastSibling<T>(this T self) where T : Component
        {
            self.transform.SetAsLastSibling();
            return self;
        }

        /// <summary>
        /// 设置为最顶层
        /// </summary>
        public static T AsFirstSibling<T>(this T self) where T : Component
        {
            self.transform.SetAsFirstSibling();
            return self;
        }

        /// <summary>
        /// 设置某一层级
        /// </summary>
        public static T SiblingIndex<T>(this T self, int index) where T : Component
        {
            self.transform.SetSiblingIndex(index);
            return self;
        }

        /*----------------------------*/
        /* Children                   */
        /*----------------------------*/

        /// <summary>
        /// 显示指定子物体
        /// </summary>
        public static T ShowChild<T>(this T self, string path) where T : Component
        {
            self.transform.Find(path).gameObject.Show();
            return self;
        }

        /// <summary>
        /// 隐藏指定子物体
        /// </summary>
        public static T HideChild<T>(this T self, string path) where T : Component
        {
            self.transform.Find(path).Hide();
            return self;
        }

        /*----------------------------*/
        /* Find Path                  */
        /*----------------------------*/
        
        /// <summary>
        /// 递归遍历查找指定的名字的子物体
        /// </summary>
        /// <param name="self">当前Transform</param>
        /// <param name="findName">目标名</param>
        /// <param name="stringComparison">字符串比较规则</param>
        /// <returns></returns>
        public static Transform FindRecursion(this Transform self, string findName, StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (self.name.Equals(findName, stringComparison))
            {
                return self;
            }

            foreach (Transform child in self)
            {
                Transform find = child.FindRecursion(findName, stringComparison);
                
                if (find) return find;
            }

            return null;
        }

        /// <summary>
        /// 递归遍历查找相应条件的子物体
        /// </summary>
        public static Transform FindRecursion(this Transform self, Func<Transform, bool> function)
        {
            if (function(self))
            {
                return self;
            }

            foreach (Transform child in self)
            {
                Transform find = child.FindRecursion(function);
                if (find)
                {
                    return find;
                }
            }

            return null;
        }

        /// <summary>
        /// 递归遍历子物体，并调用函数
        /// </summary>
        public static void ActionRecursion(this Transform transform, Action<Transform> action)
        {
            action(transform);
            foreach (Transform child in transform)
            {
                child.ActionRecursion(action);
            }
        }
        
        /// <summary>
        /// 显示当前Transform路径
        /// </summary>
        public static string Path(this Transform transform)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            Transform tran = transform;
            
            while (true)
            {
                sb.Insert(0, tran.name);
                tran = tran.parent;
                if (tran)
                {
                    sb.Insert(0, "/");
                }
                else
                {
                    return sb.ToString();
                }
            }
        }
        
        /*----------------------------*/
        /* Destroy                    */
        /*----------------------------*/
        
        public static void DestroySelf(this Transform self)
        {
            if (self)
            {
                UnityEngine.Object.Destroy(self.gameObject);
            }
        }
        
        /// <summary>
        /// 销毁所有子物体
        /// </summary>
        public static Transform DestroyChildren(this Transform self)
        {
            var childCount = self.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                self.transform.GetChild(i).DestroySelf();
            }
            
            return self;
        }
        
        /// <summary>
        /// 销毁所有子物体
        /// </summary>
        public static T DestroyChildren<T>(this T self) where T : Component
        {
            var childCount = self.transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                self.transform.GetChild(i).DestroySelf();
            }

            return self;
        }
        
    }
}