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
        private static Dictionary<string, List<AudioPlayer>> soundPlayerInPlaying = DictionaryPool<string, List<AudioPlayer>>.Allocate();

        protected AudioManager() { }
        
        public AudioPlayer MusicPlayer { get; private set; }

        public AudioPlayer VoicePlayer { get; private set; }
        
        public void CheckAudioListener() {
            // 确保有一个AudioListener
            if (FindObjectOfType<AudioListener>() == null) {
                gameObject.AddComponent<AudioListener>();
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            ObjectPool<AudioPlayer>.Instance.Init(10,1);
            MusicPlayer = AudioPlayer.Allocate();
            VoicePlayer = AudioPlayer.Allocate();
            
            CheckAudioListener();
            
            gameObject.transform.position = Vector3.zero;
            
            // 音乐音量调节
            Configure.AudioKit.MusicVolume.OnChange += (volume => { MusicPlayer.SetVolume(volume); });
            Configure.AudioKit.MusicVolume.DisposeWhenGameObjectDestroyed(this);
            
            // 音乐开关
            Configure.AudioKit.IsMusicOn.OnChange += (musicOn => {
                if (musicOn) {
                    if (CurrentMusicName.NotEmpty()) {
                        AudioKit.PlayMusic(CurrentMusicName);
                    }
                }
                else {
                    MusicPlayer.Stop();
                }
            });
            Configure.AudioKit.IsMusicOn.DisposeWhenGameObjectDestroyed(this);
            
            // // 人物声音开关
            
            // 音效音量调节
            // AudioSettings.settings.VoiceVolume.OnChange += (volume => { VoicePlayer.SetVolume(volume); });
            // AudioSettings.settings.VoiceVolume.DisposeWhenGameObjectDestroyed(this);

            
            // AudioSettings.settings.IsVoiceOn.OnChange += (musicOn => {
            //     if (musicOn) {
            //         if (CurrentVoiceName.NotEmpty()) {
            //             AudioKit.PlayVoice(CurrentVoiceName);
            //         }
            //     }
            //     else {
            //         VoicePlayer.Stop();
            //     }
            // });
            // AudioSettings.settings.IsVoiceOn.DisposeWhenGameObjectDestroyed(this);
            //
            // // 音效开关
            // AudioSettings.settings.IsSoundOn.OnChange += (soundOn => {
            //     if (soundOn) { }
            //     else {
            //         ForEachAllSound(player => player.Stop());
            //     }
            // });
            // AudioSettings.settings.IsSoundOn.DisposeWhenGameObjectDestroyed(this);
            //
            // // 音效开关
            // AudioSettings.settings.SoundVolume.OnChange += soundVolume =>  ForEachAllSound(player => player.SetVolume(soundVolume));
            // AudioSettings.settings.SoundVolume.DisposeWhenGameObjectDestroyed(this);
        }
        
        public string CurrentMusicName { get; set; }

        public string CurrentVoiceName { get; set; }
        
        public void ForEachAllSound(Action<AudioPlayer> operation) {
            foreach (AudioPlayer audioPlayer in soundPlayerInPlaying.SelectMany(keyValuePair => keyValuePair.Value)) {
                operation(audioPlayer);
            }
        }
        
        // /// <summary>
        // /// 播放音效
        // /// </summary>
        // void PlaySound(AudioSoundMsg soundMsg) {
        //     if (AudioKit.Settings.IsSoundOn.Value) {
        //         AudioPlayer unit = ObjectPool<AudioPlayer>.Instance.Allocate();
        //
        //         unit.SetOnStartListener(soundUnit => {
        //             soundMsg.onSoundBeganCallback.InvokeGracefully();
        //             unit.SetVolume(soundMsg.Volume);
        //             soundUnit.SetOnStartListener(null);
        //         });
        //         unit.SetAudio(gameObject, soundMsg.SoundName, false);
        //
        //         unit.SetOnFinishListener(soundUnit => {
        //             soundMsg.onSoundEndedCallback.InvokeGracefully();
        //             soundUnit.SetOnFinishListener(null);
        //         });
        //     }
        // }
    }
}
