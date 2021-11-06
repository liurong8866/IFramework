using System;
using System.Runtime.Remoting.Messaging;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Engine
{
    public class AudioPlayer : IPoolable, IRecyclable
    {
        private string name;
        private bool isLoop;
        private bool isPause = false;
        private float volume = 1.0f;
        
        private ResourceLoader loader;
        private AudioSource audioSource;
        private AudioClip audioClip;
        
        public Action<AudioPlayer> OnStartListener;
        public Action<AudioPlayer> OnFinishListener;
        
        public static AudioPlayer Allocate()
        {
            return ObjectPool<AudioPlayer>.Instance.Allocate();
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
            // 没有指定名称则退出
            if (string.IsNullOrEmpty(name)) {
                return;
            }
            // 如果播放的是当前音乐则立即播放
            if (this.name == name) {
                PlayAudioClip();
                return;
            }
            // 指定AudioSource
            if (audioSource == null) {
                audioSource = root.AddComponent<AudioSource>();
            }
            // 异步加载资源
            this.loader = ResourceLoader.Allocate();
            this.isLoop = loop;
            this.name = name;
            this.volume = volume;
            this.loader.AddToLoad(name, OnResourceLoadFinish);
            this.loader.LoadAsync();
        }
        
        // 播放音乐
        private void PlayAudioClip()
        {
            if (audioSource == null || audioClip == null) {
                Release();
                return;
            }
            audioSource.clip = audioClip;
            audioSource.loop = isLoop;
            audioSource.volume = volume;
            // 播放前事件
            OnStartListener.InvokeSafe(this);
            audioSource.Play();
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            Release();
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void Pause()
        {
            if (isPause) {
                return;
            }
            isPause = true;
            audioSource.Pause();
        }

        /// <summary>
        /// 恢复播放
        /// </summary>
        public void Resume()
        {
            if (!isPause) {
                return;
            }
            isPause = false;
            audioSource.Play();
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
            PlayAudioClip();
        }

        private void OnSoundPlayFinish(int count)
        {
            OnFinishListener.InvokeSafe(this);
            if (!isLoop) {
                Release();
            }
        }

        private void Release()
        {
            Recycle();
        }

        private void CleanResources()
        {
            name = null;
            isPause = false;
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
    }
}
