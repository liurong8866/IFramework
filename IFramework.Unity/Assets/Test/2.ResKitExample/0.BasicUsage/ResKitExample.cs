﻿/****************************************************************************
 * Copyright (c) 2018.5 liangxie
 * 
 * http://liangxiegame.com
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

using System.IO;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace QFramework.Example
{
	public class ResKitExample : MonoBehaviour
	{
		private ResourceLoader mResLoader = ResourceLoader.Allocate();

		private void Start()
		{
			// mResLoader.Load<GameObject>("resources://GameObject")
			// 	.Instantiate()
			// 	.Name("这是使用 ResKit 加载的对象");
			//
			// mResLoader.Load<GameObject>("AssetObj")
			// 	.Instantiate()
			// 	.Name("这是使用通过 AssetName 加载的对象");
			
			mResLoader.Load<GameObject>("AssetObj", "assetobj-prefab")
				.Instantiate()
				.Name("这是使用通过 AssetName  和 AssetBundle 加载的对象");
		}
		
		

		private void OnDestroy()
		{
			mResLoader.Recycle();
			mResLoader = null;
		}
	}
}