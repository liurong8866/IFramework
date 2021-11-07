namespace IFramework.Engine
{
    /*
     Unity3D游戏引擎一共支持4个音乐格式的文件
    .AIFF  适用于较短的音乐文件可用作游戏打斗音效
    .WAV  适用于较短的音乐文件可用作游戏打斗音效
    .MP3  适用于较长的音乐文件可用作游戏背景音乐
    .OGG  适用于较长的音乐文件可用作游戏背景音乐
    在unity可以播放动画，播放音频，当然也可以播放视频啦~~~目前主要支持mov, .mpg, .mpeg, .mp4,.avi, .asf格式  
     */
    public static class ResourcesUrlType
    {
        public const string LOCAL_FILE = "file://";
        public const string RESOURCES = "resources::";
        public const string IMAGE = "image::";
        public const string VIDEO = "video::";
        public const string AUDIO_MP3 = "audio-mp3::";
        public const string AUDIO_WAV = "audio-wav::";
        public const string AUDIO_OGG = "audio-ogg::";
    }
}
