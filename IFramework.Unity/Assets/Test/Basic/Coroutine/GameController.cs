using System.Collections;
using System.Collections.Generic;
using System.Threading;
using IFramework.Core;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Transform cube;
    public Text text;

    private Coroutine coroutineSayHi;
    /*
     * 什么是协程
     * 1、协程是一段在祝线程中执行的代码逻辑（本质就是一个方法）
     * 2、协程不是多线程、从上到下依次执行
     * 3、Unity每帧结束后检测yield后的条件是否满足
     * 
     * 协程的格式
     * 1、IEnumerator 协程名字(参数){
     *         ...
     *         yield return ...;
     *         ...
     *     }
     * 如何控制协程
     * 1、开始协程：StartCoroutine();
     * 2、结束协程：StopCoroutine();
     * 
     * 协程的返回值
     * 1、跟普通方法一样可以调用多次，
     * 2、如果遇到 yield return null, 0, 1, 2...  都表示yield这行代码下面的逻辑将在下一帧继续执行
     * 3、如果遇到 yield return new WaitForSeconds(n) 表示等待n秒后继续执行
     * 4、如果遇到 yield return StartCoroutine(A) 表示在A协程执行完毕后继续执行
     * 5、如果遇到 yield return WaitForFixedUpdate() 表示在FixedUpdate执行后继续执行
     * 6、如果遇到 yield return WaitForEndOfFrame() 表示在OnGUI执行后继续执行
     * 7、如果遇到 yield return WWW 表示在www下载完毕后继续执行
     * 8、如果遇到 yield return obj 表示在obj不为空时继续执行
     * 9、将系统提的都回调方法可以改成协程方法 IEnumerator Start(){ yield return ...}
     * 10、协程方法可以做普通方法自由调用
     */
    void Start()
    {
        // 方法一
        // coroutineSayHi = StartCoroutine(SayHi());
        // 方法二
        // StartCoroutine("SayHi");

        // 休息2s
        // StartCoroutine(Waiting());
        // 倒计时
        // StartCoroutine(Timer());
        
        // 父子调用
        // StartCoroutine(Father());

        // 普通多线程
        // Thread thread = new Thread(() =>
        // {
        //     for (int i = 0; i < 10000; i++)
        //     {
        //         Debug.Log("hello world");
        //         Thread.Sleep(1000);
        //     }
        // });
        //
        // thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        cube.Rotate(Vector3.up);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // StopCoroutine("SayHi");
            // StopAllCoroutines();
            StopCoroutine(coroutineSayHi); 
            // StopCoroutine(SayHi()); 此方法错误
        }
    }
    
    public IEnumerator SayHi()
    {
        for (int i = 0; i < 1000; i++)
        {
            Log.Info("你好");
            yield return null;
        }
        cube.transform.GetComponent<MeshRenderer>().material.color= Color.green;
    }
    
    // 创建协程，实现休息2s
    IEnumerator Waiting()
    {
        Log.Info("我要开始休息");
        
        yield return new WaitForSeconds(2f);
        
        Log.Info("我休息完毕了");
        
        yield return new WaitForSeconds(3f);
        
        Log.Info("我又累了，想睡觉了");
        
        yield return new WaitForSeconds(2f);
        
        Log.Info("我休息完毕了");
    }

    IEnumerator Timer()
    {
        for (int i = 10; i >= 0; i--)
        {
            text.text = i+"";
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Son()
    {
        Log.Info("我是儿子...");
        yield return StartCoroutine(Waiting());
        Log.Info("我饿了，我要吃东西");
    }
    
    IEnumerator Father()
    {
        Log.Info("儿子，我是父亲...");
        yield return StartCoroutine(Son());
        Log.Info("我在做饭，等一下就好");
    }
}
