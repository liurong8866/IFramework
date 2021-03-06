using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Engine
{
    public enum AudioPlayerState
    {
        None,
        Playing,
        Pause,
        Stopped
    }

    public class AudioPlayer : IPoolable, IRecyclable, IDisposable
    {
        private string name;
        private bool isLoop;
        private float volume = 1.0f;
        private bool useCache;
        private AudioPlayerState state = AudioPlayerState.None;

        private ResourceLoader loader;
        private AudioSource audioSource;
        private AudioClip audioClip;

        public Action<AudioPlayer> OnStartListener;
        public Action<AudioPlayer> OnFinishListener;

        public static readonly Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();
        
        public static AudioPlayer Allocate(bool useCache = true)
        {
            AudioPlayer audioPlayer = ObjectPool<AudioPlayer>.Instance.Allocate();
            audioPlayer.useCache = useCache;
            return audioPlayer;
        }

        public string Name => name;

        public AudioSource AudioSource => audioSource;

        public bool IsRecycled { get; set; }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="root">AudioManager</param>
        /// <param name="name">名称</param>
        /// <param name="loop">是否循环</param>
        /// <param name="volume">音量</param>
        public void Play(GameObject root, string name, bool loop, float volume)
        {
            state = AudioPlayerState.Playing;

            // 没有指定名称则退出
            if (string.IsNullOrEmpty(name)) {
                return;
            }
            
            this.isLoop = loop;
            this.name = name;
            this.volume = volume;
            
            // 指定AudioSource
            if (audioSource == null) {
                audioSource = root.AddComponent<AudioSource>();
            }
            // 如果播放的已缓存
            if (audioClipDic.ContainsKey(name)) {
                audioClip = audioClipDic[name];
                AudioManager.Instance.StartCoroutine(PlayAudioClip());
                return;
            }
            // 异步加载资源
            this.loader = ResourceLoader.Allocate();
            this.loader.AddToLoad(name, OnResourceLoadFinish);
            this.loader.LoadAsync();
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        public IEnumerator PlayAudioClip()
        {
            if (audioSource == null || audioClip == null) {
                Release();
                yield break;
            }
            audioSource.clip = audioClip;
            audioSource.loop = isLoop;
            audioSource.volume = volume;

            // 播放前事件
            OnStartListener.InvokeSafe(this);
            OnStartListener = null;

            // 播放
            audioSource.Play();

            // 播放完毕后回调
            if (!isLoop) {
                yield return new WaitForSeconds(audioClip.length);
                OnFinishListener.InvokeSafe(this);
            }
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            if (state == AudioPlayerState.Stopped) {
                return;
            }
            state = AudioPlayerState.Stopped;
            if (audioSource != null && audioSource.isPlaying) {
                audioSource.Stop();
            }
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void Pause()
        {
            if (state == AudioPlayerState.Pause) {
                return;
            }
            state = AudioPlayerState.Pause;
            if (audioSource != null && audioSource.isPlaying) {
                audioSource.Pause();
            }
        }

        /// <summary>
        /// 恢复播放
        /// </summary>
        public void Resume()
        {
            if (state != AudioPlayerState.Pause) {
                return;
            }
            state = AudioPlayerState.Playing;
            if (audioSource != null) {
                audioSource.Play();
            }
        }

        /// <summary>
        /// 设置播放声音
        /// </summary>
        public void SetVolume(float volume)
        {
            if (audioSource != null) {
                audioSource.volume = volume;
            }
        }

        /// <summary>
        /// 加载资源完毕后回调函数
        /// </summary>
        private void OnResourceLoadFinish(bool result, IResource resource)
        {
            if (!result) {
                Release();
                return;
            }
            audioClip = resource.Asset as AudioClip;
            if (audioClip == null) {
                Log.Error("音频资源加载失败:" + name);
                Release();
                return;
            }
            // if (useCache) {
                // 记录缓存
                audioClipDic[name] = audioClip;
            // }
            AudioManager.Instance.StartCoroutine(PlayAudioClip());
        }

        public void Release()
        {
            CleanResources();
            
            if (useCache) {
                Recycle();
            }
            else {
                if (audioSource != null) {
                    GameObject.Destroy(audioSource);
                    audioSource = null;
                }
            }
        }

        private void CleanResources()
        {
            name = null;
            state = AudioPlayerState.None;
            OnFinishListener = null;
            if (audioSource != null) {
                if (audioSource.clip == audioClip) {
                    audioSource.Stop();
                    audioSource.clip = null;
                }
            }
            audioClip = null;
            if (loader != null) {
                loader.Recycle();
                loader = null;
            }
        }

        public void OnRecycled()
        {
            CleanResources();
        }

        public void Recycle()
        {
            if (!ObjectPool<AudioPlayer>.Instance.Recycle(this)) {
                if (audioSource != null) {
                    GameObject.Destroy(audioSource);
                    audioSource = null;
                }
            }
        }
        
        public void Dispose()
        {
            Release();
            audioClipDic.Clear();
        }
    }
}
