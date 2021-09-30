using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 调用其他脚本的协程方法
        StartCoroutine(GameObject.Find("GameController").GetComponent<GameController>().SayHi());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
