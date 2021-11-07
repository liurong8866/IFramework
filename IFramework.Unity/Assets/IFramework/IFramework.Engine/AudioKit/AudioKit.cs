using System;
using IFramework.Core;

namespace IFramework.Engine
{
    public class AudioKit
    {
        public static AudioPlayer MusicPlayer { get { return AudioManager.Instance.MusicPlayer; } }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="musicName"></param>
        /// <param name="loop"></param>
        /// <param name="volume"></param>
        /// <param name="allowMusicOff"></param>
        /// <param name="onBeganCallback"></param>
        /// <param name="onFinishCallback"></param>
        public static void PlayMusic(string musicName, bool loop = true, float volume = -1f, bool allowMusicOff = true, Action onBeganCallback = null, Action onFinishCallback = null)
        {
            AudioManager.Instance.CurrentMusicName = musicName;
            if (!Configure.AudioKit.IsMusicOn.Value && allowMusicOff) {
                onBeganCallback.InvokeSafe();
                onFinishCallback.InvokeSafe();
                return;
            }
            MusicPlayer.OnStartListener = player => { onBeganCallback.InvokeSafe(); };
            MusicPlayer.Play(AudioManager.Instance.gameObject, musicName, loop, volume < 0 ? Configure.AudioKit.MusicVolume.Value : volume);
            MusicPlayer.OnFinishListener = player => { onFinishCallback.InvokeSafe(); };
        }

        /// <summary>
        /// 结束播放
        /// </summary>
        public static void StopMusic()
        {
            MusicPlayer.Stop();
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public static void PauseMusic()
        {
            MusicPlayer.Pause();
        }

        /// <summary>
        /// 恢复播放
        /// </summary>
        public static void ResumeMusic()
        {
            MusicPlayer.Resume();
        }

        public static AudioPlayer VoicePlayer { get { return AudioManager.Instance.VoicePlayer; } }

        /// <summary>
        /// 播放人物声音
        /// </summary>
        /// <param name="voiceName"></param>
        /// <param name="loop"></param>
        /// <param name="onBeganCallback"></param>
        /// <param name="onFinishCallback"></param>
        public static void PlayVoice(string voiceName, bool loop = false, Action onBeganCallback = null, Action onFinishCallback = null)
        {
            AudioManager.Instance.CurrentVoiceName = voiceName;
            if (!Configure.AudioKit.IsVoiceOn.Value) {
                onBeganCallback.InvokeSafe();
                onFinishCallback.InvokeSafe();
                return;
            }
            VoicePlayer.OnStartListener = player => { onBeganCallback.InvokeSafe(); };
            VoicePlayer.Play(AudioManager.Instance.gameObject, voiceName, loop, Configure.AudioKit.VoiceVolume.Value);
            VoicePlayer.OnFinishListener = player => { onFinishCallback.InvokeSafe(); };
        }

        public static void PauseVoice()
        {
            VoicePlayer.Pause();
        }

        public static void ResumeVoice()
        {
            VoicePlayer.Resume();
        }

        public static void StopVoice()
        {
            VoicePlayer.Stop();
        }

        public static AudioPlayer PlaySound(string soundName, bool loop = false, Action<AudioPlayer> callBack = null)
        {
            if (!Configure.AudioKit.IsSoundOn.Value) return null;
            AudioPlayer soundPlayer = AudioPlayer.Allocate(false);
            soundPlayer.OnFinishListener = (player) => {
                player.Release();
            };
            soundPlayer.Play(AudioManager.Instance.gameObject, soundName, loop, Configure.AudioKit.SoundVolume.Value);
            return soundPlayer;
        }

        public static void StopAllSound()
        {
            AudioManager.Instance.ForEachAllSound(player => player.Stop());
            // AudioManager.Instance.ClearAllPlayingSound();
        }
    }
}
