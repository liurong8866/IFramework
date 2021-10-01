// /*****************************************************************************
//  * MIT License
//  * 
//  * Copyright (c) 2021 liurong
//  * 
//  * Permission is hereby granted, free of charge, to any person obtaining a copy
//  * of this software and associated documentation files (the "Software"), to deal
//  * in the Software without restriction, including without limitation the rights
//  * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  * copies of the Software, and to permit persons to whom the Software is
//  * furnished to do so, subject to the following conditions:
//  * 
//  * The above copyright notice and this permission notice shall be included in all
//  * copies or substantial portions of the Software.
//  * 
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  * SOFTWARE.
//  *****************************************************************************/
//

namespace IFramework.Engine {
    /*
     Unity3D游戏引擎一共支持4个音乐格式的文件
    .AIFF  适用于较短的音乐文件可用作游戏打斗音效
    .WAV  适用于较短的音乐文件可用作游戏打斗音效
    .MP3  适用于较长的音乐文件可用作游戏背景音乐
    .OGG  适用于较长的音乐文件可用作游戏背景音乐
    在unity可以播放动画，播放音频，当然也可以播放视频啦~~~目前主要支持mov, .mpg, .mpeg, .mp4,.avi, .asf格式  
     */
    public static class ResourcesUrlType {
        public const string LOCAL_FILE = "file://";
        public const string RESOURCES = "resources::";
        public const string IMAGE = "image::";
        public const string VIDEO = "video::";
        public const string AUDIO_MP3 = "audio-mp3::";
        public const string AUDIO_WAV = "audio-wav::";
        public const string AUDIO_OGG = "audio-ogg::";
        
    }
}