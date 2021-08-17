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
using System.Collections.Generic;
using IFramework.Core;

namespace IFramework.Engine
{
    public interface IResource : ICounter, IRecyclable, IResourceLoadTask
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        string AssetName { get; }
        
        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        string AssetBundleName { get; }
        
        /// <summary>
        /// 资源类型
        /// </summary>
        Type AssetType { get; set; }

        /// <summary>
        /// 资源对象
        /// </summary>
        UnityEngine.Object Asset { get; }
        
        /// <summary>
        /// 资源加载状态
        /// </summary>
        ResourceState State { get; }
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        bool Load();
        
        /// <summary>
        /// 异步加载资源
        /// </summary>
        void LoadASync();

        /// <summary>
        /// 卸载图片资源
        /// </summary>
        bool UnloadImage(bool flag);
        
        /// <summary>
        /// 释放资源
        /// </summary>
        bool Release();
        
        /// <summary>
        /// 获取依赖的资源
        /// </summary>
        List<string> GetDependResourceList();

        /// <summary>
        /// 是否依赖资源加载完毕
        /// </summary>
        bool IsDependResourceLoaded();
        
        /// <summary>
        /// 注册资源加载完毕事件
        /// </summary>
        void RegisterOnLoadedEvent(Action<bool, IResource> listener);
        
        /// <summary>
        /// 注销资源加载完毕事件
        /// </summary>
        void UnRegisterOnLoadedEvent(Action<bool, IResource> listener);

    }
}