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
        private bool usedCache = true;
        private bool isCache = false;
        
        private ResourceLoader loader;
        private AudioSource audioSource;
        private AudioClip audioClip;
        
        private Action<AudioPlayer> onStartListener;
        private Action<AudioPlayer> onFinishListener;
        
        public void SetOnStartListener(Action<AudioPlayer> action)
        {
            onStartListener = action;
        }

        public void SetOnFinishListener(Action<AudioPlayer> action)
        {
            onFinishListener = action;
        }

        public string Name => name;
        public AudioSource AudioSource => audioSource;

        public static AudioPlayer Allocate()
        {
            return ObjectPool<AudioPlayer>.Instance.Allocate();
        }

        public bool UsedCache { get => usedCache; set => usedCache = false; }

        public bool IsRecycled { get => isCache; set => isCache = false; }

        public void SetAudioExt(GameObject root, AudioClip clip, string name, bool loop)
        {
            if (clip == null || this.name == name) {
                return;
            }
            if (audioSource == null) {
                audioSource = root.AddComponent<AudioSource>();
            }
            CleanResources();
            isLoop = loop;
            this.name = name;
            audioClip = clip;
            PlayAudioClip();
        }

        public void SetAudio(GameObject root, string name, bool loop)
        {
            if (string.IsNullOrEmpty(name)) {
                return;
            }
            if (this.name == name) {
                return;
            }
            if (audioSource == null) {
                audioSource = root.AddComponent<AudioSource>();
            }

            //防止卸载后立马加载的情况
            ResourceLoader preLoader = this.loader;
            this.loader = null;
            CleanResources();
            this.loader = ResourceLoader.Allocate();
            this.isLoop = loop;
            this.name = name;
            this.loader.AddToLoad(name, OnResourceLoadFinish);
            if (preLoader != null) {
                preLoader.Recycle();
                preLoader = null;
            }
            if (this.loader != null) {
                this.loader.LoadAsync();
            }
        }

        public void Stop()
        {
            Release();
        }

        public void Pause()
        {
            if (isPause) {
                return;
            }
            isPause = true;
            audioSource.Pause();
        }

        public void Resume()
        {
            if (!isPause) {
                return;
            }
            isPause = false;
            audioSource.Play();
        }

        public void SetVolume(float volume)
        {
            if (audioSource != null) {
                audioSource.volume = volume;
            }
        }

        private void OnResourceLoadFinish(bool result, IResource resource)
        {
            if (!result) {
                Release();
                return;
            }
            audioClip = resource.Asset as AudioClip;
            if (audioClip == null) {
                Log.Error("音频资源错误:" + name);
                Release();
                return;
            }
            PlayAudioClip();
        }

        private void PlayAudioClip()
        {
            if (audioSource == null || audioClip == null) {
                Release();
                return;
            }
            audioSource.clip = audioClip;
            audioSource.loop = isLoop;
            audioSource.volume = 1.0f;
            if (onStartListener != null) {
                onStartListener(this);
            }
            audioSource.Play();
        }

        private void OnSoundPlayFinish(int count)
        {
            if (onFinishListener != null) {
                onFinishListener(this);
            }
            if (!isLoop) {
                Release();
            }
        }

        private void Release()
        {
            CleanResources();
            if (usedCache) {
                Recycle();
            }
        }

        private void CleanResources()
        {
            name = null;
            isPause = false;
            onFinishListener = null;
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
