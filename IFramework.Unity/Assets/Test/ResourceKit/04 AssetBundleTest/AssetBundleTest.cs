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
using IFramework.Engine;
using UnityEngine;

namespace Test.ResourceKit._03_AssetBundleTest {
    public class AssetBundleTest : MonoBehaviour {

        private AssetResource assetResource;

        private void Start() {
            assetResource = AssetResource.Allocate("Malong");
            assetResource.Load();
            assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 Malong");
            assetResource = AssetResource.Allocate("AssetObj");
            assetResource.Load();
            assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 AssetObj");
            assetResource = AssetResource.Allocate("AssetObj", "pack1");
            assetResource.Load();
            assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 pack1");
            assetResource = AssetResource.Allocate("AssetObj", "pack2");
            assetResource.Load();
            assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 pack2");
        }

        private void OnDestroy() {
            // assetResource.Dispose();
        }

    }
}
