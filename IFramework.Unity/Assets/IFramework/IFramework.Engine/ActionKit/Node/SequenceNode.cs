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
using IFramework.Core;

namespace IFramework.Engine
{
    public class SequenceNode : AbstractAction, INode
    {
        // 记录原始节点状态，恢复默认值用
        protected List<IAction> originalNodes;
        // 真正执行的节点
        protected List<IAction> executeNodes;

        public SequenceNode()
        {
            // 从List对象池中分配
            originalNodes = ListPool<IAction>.Allocate();
            executeNodes = ListPool<IAction>.Allocate();
        }
        
        /// <summary>
        /// 总节点数量
        /// </summary>
        public int Count => executeNodes.Count;

        public IAction CurrentNode {
            get {
                IAction currentNode = executeNodes[0];
                // 如果当前节点是INode类型节点，也即是装饰节点，则取对应的属性CurrentNode， 否则直接返回
                return !(currentNode is INode node) ? currentNode : node.CurrentNode;
            }
        }
        
        protected override void OnReset() {
            executeNodes.Clear();

            // 通过原始节点恢复初始状态
            foreach (IAction node in originalNodes) {
                node.Reset();
                executeNodes.Add(node);
            }
        }
    }
}
