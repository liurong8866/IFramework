using System;
using System.Linq;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Editor
{
    public class BindCollector
    {
        /// <summary>
        /// 递归查找当前ViewController下的所有Bind
        /// </summary>
        /// <param name="current">当前节点</param>
        /// <param name="fullName">全路径</param>
        /// <param name="panelCodeInfo">父节点</param>
        /// <param name="parentElementInfo">副节点Element信息</param>
        /// <param name="leafPanelType">叶节点类型</param>
        public static void SearchBind(Transform current, string fullName, PanelCodeInfo panelCodeInfo = null, ElementInfo parentElementInfo = null, Type leafPanelType = null)
        {
            foreach (Transform child in current) {
                // 获得当前节点Bind组件
                IBind bind = child.GetComponent<IBind>();

                if (bind != null) {
                    // 如果父节点Bind不是Element类型
                    if (parentElementInfo == null) {
                        // 如果不包含当前节点的对象名称
                        if (!panelCodeInfo.BindInfoList.Any(bindInfo => bindInfo.Name.Equals(bind.Transform.name))) {
                            panelCodeInfo.BindInfoList.Add(new BindInfo {
                                Name = bind.Transform.name,
                                BindScript = bind,
                                PathToElement = EditorUtils.PathToParent(child, panelCodeInfo.GameObjectName)
                            });
                            panelCodeInfo.NameToFullNameDic.Add(bind.Transform.name, fullName + child.name);
                        }
                        else { Log.Error("生成失败！ 绑定的对象名称重复: " + child.name); }
                    }
                    // 如果父节点Bind是Element类型
                    else {
                        // 如果不包含当前节点的对象名称
                        if (!parentElementInfo.BindInfoList.Any(bindInfo => bindInfo.Name.Equals(bind.Transform.name))) {
                            parentElementInfo.BindInfoList.Add(new BindInfo {
                                Name = bind.Transform.name,
                                BindScript = bind,
                                PathToElement = EditorUtils.PathToParent(child, parentElementInfo.BehaviourName)
                            });
                            parentElementInfo.NameToFullNameDic.Add(bind.Transform.name, fullName + child.name);
                        }
                        else { Log.Error("生成失败！ 绑定的对象名称重复: " + child.name); }
                    }

                    // 如果当前绑定类型不是DefaultElement，则
                    if (bind.BindType != BindType.DefaultElement) {
                        ElementInfo elementInfo = new ElementInfo {
                            BehaviourName = bind.ComponentName,
                            BindInfo = new BindInfo {
                                BindScript = bind
                            }
                        };

                        if (parentElementInfo == null) {
                            // 添加
                            panelCodeInfo.ElementInfoList.Add(elementInfo);
                        }
                        else {
                            // 添加
                            parentElementInfo.ElementInfoList.Add(elementInfo);
                        }

                        // 递归下一层
                        SearchBind(child, fullName + child.name + "/", panelCodeInfo, elementInfo);
                    }
                    else {
                        // 如果是标记的叶子节点则不再继续搜索
                        if (!(leafPanelType != null && bind.Transform.GetComponent(leafPanelType))) {
                            // 递归下一层
                            SearchBind(child, fullName + child.name + "/", panelCodeInfo, parentElementInfo);
                        }
                    }
                }
                else {
                    // 递归下一层
                    SearchBind(child, fullName + child.name + "/", panelCodeInfo, parentElementInfo);
                }
            }
        }
    }
}
