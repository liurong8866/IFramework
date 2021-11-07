using System;
using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Engine
{
    [MonoSingleton("[Audio]/AudioManager")]
    public class AudioManager : ManagerBehaviour<AudioManager>
    {
        // 音效
        private static Dictionary<string, List<AudioPlayer>> soundPlayer= DictionaryPool<string, List<AudioPlayer>>.Allocate();

        protected AudioManager() { }

        public AudioPlayer MusicPlayer { get; private set; }

        public AudioPlayer VoicePlayer { get; private set; }

        public string CurrentMusicName { get; set; }

        public string CurrentVoiceName { get; set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            ObjectPool<AudioPlayer>.Instance.Init(10, 1);
            MusicPlayer = AudioPlayer.Allocate();
            VoicePlayer = AudioPlayer.Allocate();
            CheckAudioListener();
            gameObject.transform.position = Vector3.zero;

            // 音乐开关
            Configure.AudioKit.IsMusicOn.OnChange += (musicOn => { musicOn.iif(() => CheckAudioListener(), () => MusicPlayer.Release()); });
            Configure.AudioKit.IsMusicOn.DisposeWhenGameObjectDestroyed(this);
            // 音乐音量调节
            Configure.AudioKit.MusicVolume.OnChange += (volume => { MusicPlayer.SetVolume(volume); });
            Configure.AudioKit.MusicVolume.DisposeWhenGameObjectDestroyed(this);
            
            // 人物声音开关
            Configure.AudioKit.IsVoiceOn.OnChange += (voiceOn => { voiceOn.iif(() => CheckAudioListener(), () => VoicePlayer.Release()); });
            Configure.AudioKit.IsVoiceOn.DisposeWhenGameObjectDestroyed(this);
            // 人物音量调节
            Configure.AudioKit.VoiceVolume.OnChange += (volume => { VoicePlayer.SetVolume(volume); });
            Configure.AudioKit.VoiceVolume.DisposeWhenGameObjectDestroyed(this);

            // 音效开关
            Configure.AudioKit.IsSoundOn.OnChange += (soundOn => { soundOn.iif(null, () => ForEachAllSound(player => player.Release())); });
            Configure.AudioKit.IsSoundOn.DisposeWhenGameObjectDestroyed(this);
            // 音效开关
            Configure.AudioKit.SoundVolume.OnChange += volume => ForEachAllSound(player => player.SetVolume(volume));
            Configure.AudioKit.SoundVolume.DisposeWhenGameObjectDestroyed(this);
        }

        /// <summary>
        /// 检查AudioListener，如果没有则添加
        /// </summary>
        public void CheckAudioListener()
        {
            // 确保有一个AudioListener
            if (FindObjectOfType<AudioListener>() == null) {
                gameObject.AddComponent<AudioListener>();
            }
        }
        
        public void ForEachAllSound(Action<AudioPlayer> operation)
        {
            foreach (AudioPlayer audioPlayer in soundPlayer.SelectMany(keyValuePair => keyValuePair.Value)) {
                operation(audioPlayer);
            }
        }
    }
}
