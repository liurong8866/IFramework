using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using IFramework.Core;
using IFramework.Engine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Video;

public class VidioController : MonoBehaviour
{
    // 播放视频的UI组件
    private RawImage videoImage;
    // 视频地址
    private string videoUrl = "https://vd3.bdstatic.com/mda-ka5ayxd86t7z2h1r/mda-ka5ayxd86t7z2h1r.mp4?pd=22";
    // 视频存储路径
    private string videoFilePath;
    // 文件名称
    private string videoName = "myvideo.mp4";
    // 视频播放控件
    VideoPlayer videoPlayer;
    VideoClip videoClip;
    //声音控件
    AudioSource audioSource;

    private void Awake()
    {
        videoFilePath = Application.dataPath + "/Test/Basic/Coroutine/Resources/" + videoName;

        videoImage = transform.Find("RawImage").GetComponent<RawImage>();

        videoPlayer = videoImage.GetComponent<VideoPlayer>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (File.Exists(videoFilePath))
        {
            StartCoroutine(PlayVideo());
        }
        else
        {
            StartCoroutine(DownLoadMovie());
        }
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 下载视频
    /// </summary>
    IEnumerator DownLoadMovie()
    {
        WWW www =new WWW(videoUrl);

        while (www.isDone == false)
        {
            // 下载进度
            Log.Info(www.progress);
            yield return null;
        }
        
        //视频下载完毕，保存到本地
        try
        {
            File.WriteAllBytes(videoFilePath, www.bytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        // 如果文件不存在，就等待持续写入
        while (!File.Exists(videoFilePath))
        {
            yield return null;
        }
        
        AssetDatabase.Refresh();
        StartCoroutine(PlayVideo());
    }
    
    /// <summary>
    /// 播放视频
    /// </summary>
    IEnumerator PlayVideo()
    {
        while (videoClip == null)
        {
            videoClip = Resources.Load("myvideo") as VideoClip;
            yield return null;
        }

        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }
}
