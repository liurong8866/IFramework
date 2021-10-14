using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源类型映射表
    /// </summary>
    public static class AssetTypeCode
    {
        public const short GAME_OBJECT = 1;
        public const short AUDIO_CLIP = 2;
        public const short SPRITE = 3;
        public const short SCENE = 4;
        public const short SPRITE_ATLAS = 5;
        public const short MESH = 6;
        public const short TEXTURE_2D = 7;
        public const short TEXT_ASSET = 8;
        public const short ASSET_BUNDLE = 9;

        public static readonly Type GameObjectType = typeof(GameObject);
        public static readonly Type AudioClipType = typeof(AudioClip);
        public static readonly Type SpriteType = typeof(Sprite);
        public static readonly Type SceneType = typeof(Scene);
        public static readonly Type SpriteAtlasType = typeof(SpriteAtlas);
        public static readonly Type MeshType = typeof(Mesh);
        public static readonly Type Texture2DType = typeof(Texture2D);
        public static readonly Type TextAssetType = typeof(TextAsset);
        public static readonly Type AssetBundleType = typeof(AssetBundle);

        public static short ToCode(this Type type)
        {
            if (type == GameObjectType) {
                return GAME_OBJECT;
            }
            if (type == AudioClipType) {
                return AUDIO_CLIP;
            }
            if (type == SpriteType) {
                return SPRITE;
            }
            if (type == SceneType) {
                return SCENE;
            }
            if (type == SpriteAtlasType) {
                return SPRITE_ATLAS;
            }
            if (type == MeshType) {
                return MESH;
            }
            if (type == Texture2DType) {
                return TEXTURE_2D;
            }
            if (type == TextAssetType) {
                return TEXT_ASSET;
            }
            if (type == AssetBundleType) {
                return ASSET_BUNDLE;
            }
            return 0;
        }

        public static Type ToType(this short code)
        {
            if (code == GAME_OBJECT) {
                return GameObjectType;
            }
            if (code == AUDIO_CLIP) {
                return AudioClipType;
            }
            if (code == SPRITE) {
                return SpriteType;
            }
            if (code == SCENE) {
                return SceneType;
            }
            if (code == SPRITE_ATLAS) {
                return SpriteAtlasType;
            }
            if (code == MESH) {
                return MeshType;
            }
            if (code == TEXTURE_2D) {
                return Texture2DType;
            }
            if (code == TEXT_ASSET) {
                return TextAssetType;
            }
            if (code == ASSET_BUNDLE) {
                return AssetBundleType;
            }
            return null;
        }
    }
}
