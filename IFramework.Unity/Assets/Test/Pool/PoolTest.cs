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

using UnityEngine;
using IFramework.Core;

namespace IFramework.Test.Pool {
    public class PoolTest : MonoBehaviour {

        private void Start() {
            // SimplePoolTest();
            ObjectPoolTest();
        }

        private void SimplePoolTest() {
            SimplePool<Bullet> bulletPool = new SimplePool<Bullet>(
                () => { return new Bullet(); },
                bullet => { bullet.state = "未发射"; }, 100);
            Debug.Log(bulletPool.Count);
            Bullet bullet = bulletPool.Allocate();
            Debug.Log(bulletPool.Count);
            bulletPool.Allocate();
            bulletPool.Allocate();
            bulletPool.Allocate();
            bulletPool.Allocate();
            Debug.Log(bulletPool.Count);
            bullet.state = "已发射";
            bulletPool.Recycle(bullet);
            Debug.Log(bulletPool.Count);
            bulletPool.Recycle(null);
            Debug.Log(bulletPool.Count);
        }

        private void ObjectPoolTest() {
            ObjectPool<Bullet2> pool = ObjectPool<Bullet2>.Instance;
            pool.Init(10, 5);
            Debug.Log(pool.Count);
            pool.Init(10, 10);
            Debug.Log(pool.Count);
            pool.Init(10, 15);
            Debug.Log(pool.Count);
            Bullet2[] bullet = new Bullet2[15];
            for (int i = 0; i < 15; i++) {
                bullet[i] = pool.Allocate();
                Debug.Log(pool.Count);
            }
            for (int i = 0; i < 15; i++) {
                pool.Recycle(bullet[i]);
                Debug.Log(pool.Count);
            }
            pool.Capacity = 8;
            Debug.Log(pool.Count);
        }

    }

    internal class Bullet {

        public string state = "未发射";

    }

    internal class Bullet2 : IPoolable {

        public string state = "未发射";

        /// <summary>
        /// 回收对象时触发的事件
        /// </summary>
        public void OnRecycled() {
            state = "未发射";
        }

        /// <summary>
        /// 回收状态
        /// </summary>
        public bool IsRecycled { get; set; }

    }
}
