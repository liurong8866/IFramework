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
using IFramework.Core;

namespace IFramework.Engine
{
    public static class ResourceFactory
    {
        private static List<IResourceCreator> creators = new List<IResourceCreator>()
        {
            new ResourceCreator()
        };

        /// <summary>
        /// Resource 生产方法
        /// </summary>
        public static IResource Create(ResourceSearchRule rule)
        {
            IResource resource = creators
                .Where(c => c.Match(rule))
                .Select(c => c.Create(rule))
                .FirstOrDefault();

            if (resource == null)
            {
                Log.Error("没有找到相关资源，创建资源失败!");
            }

            return resource;
        }
        
        public static void AddCreator(IResourceCreator creator)
        {
            creators.Add(creator);
        }
        
        public static void AddCreator<T>() where T : IResourceCreator, new()
        {
            creators.Add(new T());
        }
        
        public static void RemoveCreator<T>() where T : IResourceCreator, new()
        {
            creators.RemoveAll(t => t.GetType() == typeof(T));
        }
        
        
    }
}