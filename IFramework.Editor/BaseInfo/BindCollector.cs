using System;
using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Editor
{
    public static class BindCollector
    {
        /// <summary>
        /// 递归查找当前ViewController下的所有Bind
        /// </summary>
        /// <param name="current">当前节点</param>
        /// <param name="fullName">全路径</param>
        /// <param name="rootNodeInfo">父ViewController节点信息</param>
        /// <param name="parentElementInfo">子ViewController节点，Bind的Element信息</param>
        /// <param name="leafPanelType">叶节点类型</param>
        public static void SearchBind(Transform current, string fullName, RootNodeInfo rootNodeInfo = null, ElementInfo parentElementInfo = null, Type leafPanelType = null)
        {
            foreach (Transform child in current) {
                // 获得当前节点Bind组件
                IBind bind = child.GetComponent<IBind>();
                if (bind != null) {
                    // 如果父节点Bind不是Element类型
                    if (parentElementInfo == null) {
                        // 如果不包含当前节点的对象名称
                        if (!rootNodeInfo.BindInfoList.Any(bindInfo => bindInfo.Name.Equals(bind.Transform.name))) {
                            rootNodeInfo.BindInfoList.Add(new BindInfo {
                                Name = bind.Transform.name.FormatName(),
                                BindScript = bind,
                                PathToElement = EditorUtils.PathToParent(child, rootNodeInfo.GameObjectName)
                            });
                            rootNodeInfo.NameToFullNameDic.Add(bind.Transform.name, fullName + child.name);
                        }
                        else {
                            Log.Error("生成失败！ 绑定的对象名称重复: " + child.name);
                        }
                    }
                    // 如果父节点Bind是Element类型
                    else {
                        // 如果不包含当前节点的对象名称
                        if (!parentElementInfo.BindInfoList.Any(bindInfo => bindInfo.Name.Equals(bind.Transform.name))) {
                            parentElementInfo.BindInfoList.Add(new BindInfo {
                                Name = bind.Transform.name.FormatName(),
                                BindScript = bind,
                                PathToElement = EditorUtils.PathToParent(child, parentElementInfo.GameObjectName)
                            });
                            parentElementInfo.NameToFullNameDic.Add(bind.Transform.name, fullName + child.name);
                        }
                        else {
                            Log.Error("生成失败！ 绑定的对象名称重复: " + child.name);
                        }
                    }

                    // 如果当前对象同时绑定了ViewContrller+Bind组件，并且Bind组件的类型为DefaultElement
                    if (bind.BindType != BindType.DefaultElement) {
                        ElementInfo elementInfo = new ElementInfo {
                            GameObjectName = bind.ComponentName,
                            BindInfo = new BindInfo {
                                BindScript = bind
                            }
                        };

                        // 如果没有Bind Element的对象，则
                        if (parentElementInfo == null) {
                            // 根节点ViewController 添加
                            rootNodeInfo.ElementInfoList.Add(elementInfo);
                        }
                        else {
                            // 子节点ViewController 添加
                            parentElementInfo.ElementInfoList.Add(elementInfo);
                        }

                        // 递归下一层
                        SearchBind(child, fullName + child.name + "/", rootNodeInfo, elementInfo);
                    }
                    else {
                        // 如果是标记的叶子节点则不再继续搜索, 这里是遇到Element就认为是叶节点
                        if (leafPanelType != null && bind.Transform.GetComponent(leafPanelType)) { }
                        else {
                            // 递归下一层
                            SearchBind(child, fullName + child.name + "/", rootNodeInfo, parentElementInfo);
                        }
                    }
                }
                else {
                    // 递归下一层
                    SearchBind(child, fullName + child.name + "/", rootNodeInfo, parentElementInfo);
                }
            }
        }
        
        /// <summary>
        /// 获取Element栈，从底层到上层
        /// </summary>
        public static void GetElementStack(Transform tran, Stack<Transform> elementStack)
        {
            elementStack ??= new Stack<Transform>();

            // 遇到DefaultElement 则返回
            IBind component = tran.GetComponent<IBind>();
            if (component != null) {
                // 如果是组件，则入栈
                if (component.BindType == BindType.Element || component.BindType == BindType.Component) {
                    if (!elementStack.Contains(tran)) {
                        elementStack.Push(tran);
                    }
                }
            }

            // 遍历所有元素，并向下递归
            foreach (Transform child in tran) {
                GetElementStack(child, elementStack);
            }
        }
    }
}
