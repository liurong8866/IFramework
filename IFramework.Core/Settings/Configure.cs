namespace IFramework.Core
{
    public static class Configure
    {
        /// <summary>
        /// 当前平台
        /// </summary>
        public static readonly ConfigInt CurrentPlatform = new ConfigInt("CurrentPlatform");

        /*----------------------------- Resource Kit -----------------------------*/
        public static class ResourceKit
        {
            /// <summary>
            /// 自动生成名称
            /// </summary>
            public static readonly ConfigBool AutoGenerateName = new ConfigBool("AutoGenerateName", true);

            /// <summary>
            /// 是否模拟模式
            /// </summary>
            public static readonly ConfigBool IsSimulation = new ConfigBool("IsSimulation", true);

            /// <summary>
            /// 是否从StreamingAssets加载资源
            /// </summary>
            public static readonly ConfigBool LoadAssetFromStream = new ConfigBool("LoadAssetFromStream", true);
        }
        
        /*----------------------------- UI Kit -----------------------------*/

        public static class UIKit
        {
            /// <summary>
            /// 生成脚本默认命名空间
            /// </summary>
            public static readonly ConfigString DefaultNameSpace = new ConfigString("DefaultNameSpace", Constant.UIKIT_DEFAULT_NAMESPACE);

            /// <summary>
            /// UI脚本生成路径
            /// </summary>
            public static readonly ConfigString UIScriptPath = new ConfigString("UIScriptPath", Constant.UIKIT_UI_SCRIPT_PATH);

            /// <summary>
            /// UI Prefab 生成路径
            /// </summary>
            public static readonly ConfigString UIPrefabPath = new ConfigString("UIPrefabPath", Constant.UIKIT_UI_PREFAB_PATH);

            /// <summary>
            /// ViewController 脚本生成路径
            /// </summary>
            public static readonly ConfigString ViewControllerScriptPath = new ConfigString("ViewControllerScriptPath", Constant.UIKIT_UI_VC_SCRIPT_PATH);

            /// <summary>
            /// ViewController Prefab 生成路径
            /// </summary>
            public static readonly ConfigString ViewControllerPrefabPath = new ConfigString("ViewControllerPrefabPath", Constant.UIKIT_UI_VC_PREFAB_PATH);
        }
        
        /*----------------------------- Audio Kit -----------------------------*/
        public static class AudioKit
        {
            public static readonly ConfigBool IsOn = new ConfigBool("KEY_AUDIO__ON", true);
            public static readonly ConfigBool IsMusicOn = new ConfigBool("KEY_AUDIO_MUSIC_ON", true);
            public static readonly ConfigBool IsVoiceOn = new ConfigBool("KEY_AUDIO_VOICE_ON", true);
            public static readonly ConfigBool IsSoundOn = new ConfigBool("KEY_AUDIO_SOUND_ON", true);

            public static readonly ConfigFloat MusicVolume = new ConfigFloat("KEY_AUDIO_VOICE_VOLUME", 1.0f);
            public static readonly ConfigFloat VoiceVolume = new ConfigFloat("KEY_AUDIO_MUSIC_VOLUME", 1.0f);
            public static readonly ConfigFloat SoundVolume = new ConfigFloat("KEY_AUDIO_SOUND_VOLUME", 1.0f);
        }
    }
}
