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
        /// <param name="onBeganCallback"></param>
        /// <param name="onEndCallback"></param>
        /// <param name="loop"></param>
        /// <param name="allowMusicOff"></param>
        /// <param name="volume"></param>
        public static void PlayMusic(string musicName, bool loop = true, float volume = -1f, bool allowMusicOff = true, Action onBeganCallback = null, Action onEndCallback = null)
        {
            // AudioManager.Instance.CurrentMusicName = musicName;
            // if (!Configure.AudioConfig.IsMusicOn.Value && allowMusicOff) {
            //     onBeganCallback.InvokeSafe();
            //     onEndCallback.InvokeSafe();
            //     return;
            // }
            //
            // // TODO: 需要按照这个顺序去 之后查一下原因
            // // 需要先注册事件，然后再play
            // MusicPlayer.SetOnStartListener(musicUnit => {
            //     onBeganCallback.InvokeSafe();
            //     if (volume < 0) {
            //         MusicPlayer.SetVolume(Configure.AudioConfig.MusicVolume.Value);
            //     }
            //     else {
            //         MusicPlayer.SetVolume(volume);
            //     }
            //     // 调用完就置为null，否则应用层每注册一个而没有注销，都会调用
            //     MusicPlayer.SetOnStartListener(null);
            // });
            
            MusicPlayer.Play(AudioManager.Instance.gameObject, musicName, loop, Configure.AudioKit.MusicVolume.Value);
            
            // MusicPlayer.SetOnFinishListener(player => {
            //     onEndCallback.InvokeSafe();
            //     // 调用完就置为null，否则应用层每注册一个而没有注销，都会调用
            //     player.SetOnFinishListener(null);
            // });
        }

        public static void StopMusic()
        {
            AudioManager.Instance.MusicPlayer.Stop();
        }

        public static void PauseMusic()
        {
            AudioManager.Instance.MusicPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            AudioManager.Instance.MusicPlayer.Resume();
        }
    }
}
