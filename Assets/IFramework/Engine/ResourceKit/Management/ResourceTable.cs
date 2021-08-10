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

using System.Collections.Generic;
using System.Linq;
using IFramework.Engine.Core.Table;

namespace IFramework.Engine
{
    public class ResourceTable : Table<IResource>
    {
        public TableIndex<string, IResource> NameIndex = new TableIndex<string, IResource>( 
            res=> res.AssetName.ToLower()
            );
        
        public IResource GetResource(ResourceSearchRule rule)
        {
            string assetName = rule.AssetName;

            var resources = NameIndex.Get(assetName);

            if (rule.AssetType != null)
            {
                resources = resources.Where(res => res.AssetType == rule.AssetType);
            }
            
            if (rule.AssetBundleName != null)
            {
                resources = resources.Where(res => res.AssetBundleName == rule.AssetBundleName);
            }

            return resources.FirstOrDefault();
        }

        public override IEnumerator<IResource> GetEnumerator()
        {
            return NameIndex.Dictionary.SelectMany(d => d.Value).GetEnumerator();
        }

        protected override void OnAdd(IResource resource)
        {
            NameIndex.Add(resource);
        }

        protected override void OnRemove(IResource resource)
        {
            NameIndex.Remove(resource);
        }

        protected override void OnClear()
        {
            NameIndex.Clear();
        }

        protected override void OnDispose() { }
    }
}