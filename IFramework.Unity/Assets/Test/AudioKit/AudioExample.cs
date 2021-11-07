using System;
using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Test
{
    public class AudioExample : MonoBehaviour
    {
        private void Awake()
        {
            var btnPlayHome = transform.Find("BtnPlayHome").GetComponent<Button>();
            var btnPlayGame = transform.Find("BtnPlayGame").GetComponent<Button>();
            var btnPlaySound = transform.Find("BtnPlaySoundClick").GetComponent<Button>();
            var btnPlayVoiceA = transform.Find("BtnPlayVoice").GetComponent<Button>();
            var btnSoundOn = transform.Find("BtnSoundOn").GetComponent<Button>();
            var btnSoundOff = transform.Find("BtnSoundOff").GetComponent<Button>();
            var btnMusicOn = transform.Find("BtnMusicOn").GetComponent<Button>();
            var btnMusicOff = transform.Find("BtnMusicOff").GetComponent<Button>();
            var btnVoiceOn = transform.Find("BtnVoiceOn").GetComponent<Button>();
            var btnVoiceOff = transform.Find("BtnVoiceOff").GetComponent<Button>();
            var musicVolumeSlider = transform.Find("MusicVolume").GetComponent<Slider>();
            var voiceVolumeSlider = transform.Find("VoiceVolume").GetComponent<Slider>();
            var soundVolumeSlider = transform.Find("SoundVolume").GetComponent<Slider>();
            btnPlayHome.onClick.AddListener(() => { AudioKit.PlayMusic(ResourcesUrlType.RESOURCES + "home_bg", false, onBeganCallback: () => { Log.LogInfo("开始播放"); }, onFinishCallback: () => { Log.LogInfo("播放完毕"); }); });
            btnPlayGame.onClick.AddListener(() => { AudioKit.PlayMusic(ResourcesUrlType.RESOURCES + "game_bg", true, onBeganCallback: () => { Log.LogInfo("开始播放"); }, onFinishCallback: () => { Log.LogInfo("播放完毕"); }); });
            btnPlayVoiceA.onClick.AddListener(() => { AudioKit.PlayVoice(ResourcesUrlType.RESOURCES + "hero_hurt", false, onBeganCallback: () => { Log.LogInfo("开始播放"); }, onFinishCallback: () => { Log.LogInfo("播放完毕"); }); });
            btnPlaySound.onClick.AddListener(() => {
                AudioKit.PlaySound(ResourcesUrlType.RESOURCES + "button_clicked", false, callBack: (audio) => { Log.LogInfo("回调"); }); 
                AudioKit.PlaySound(ResourcesUrlType.RESOURCES + "hero_hurt", false, callBack: (audio) => { Log.LogInfo("回调"); }); 
                
            });
            btnSoundOn.onClick.AddListener(() => { Configure.AudioKit.IsSoundOn.Value = true; });
            btnSoundOff.onClick.AddListener(() => { Configure.AudioKit.IsSoundOn.Value = false; });
            btnMusicOn.onClick.AddListener(() => { Configure.AudioKit.IsMusicOn.Value = true; });
            btnMusicOff.onClick.AddListener(() => { Configure.AudioKit.IsMusicOn.Value = false; });
            // btnMusicOn.onClick.AddListener(() => { Configure.AudioKit.IsMusicOn.Value = true; AudioKit.PauseMusic();});
            // btnMusicOff.onClick.AddListener(() => {  AudioKit.StopMusic();});
            btnVoiceOn.onClick.AddListener(() => { Configure.AudioKit.IsVoiceOn.Value = true; });
            btnVoiceOff.onClick.AddListener(() => { Configure.AudioKit.IsVoiceOn.Value = false; });
            musicVolumeSlider.value = Configure.AudioKit.MusicVolume.Value;
            voiceVolumeSlider.value = Configure.AudioKit.VoiceVolume.Value;
            soundVolumeSlider.value = Configure.AudioKit.SoundVolume.Value;
            musicVolumeSlider.onValueChanged.AddListener(v => { Configure.AudioKit.MusicVolume.Value = v; });
            voiceVolumeSlider.onValueChanged.AddListener(v => { Configure.AudioKit.VoiceVolume.Value = v; });
            soundVolumeSlider.onValueChanged.AddListener(v => { Configure.AudioKit.SoundVolume.Value = v; });
        }
    }
}
