using System;
using System.Collections;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace IFramework.Test.AssetResourceKit
{
    /*
        https://www.litefeel.com/unity-2017-new-sprite-atlas/#comment-28099
        总是启用 Always Enabled，默认是 Disabled，菜单路径：Edit > Project Settings > Editor > Sprite Packer > Mode
        纹理集 Inspector 面板上的 Include in Build 选项，仅作用于编辑器中。如果勾选就在点击运行时自动加载纹理集，否则不自动加载，并触发SpriteAtlasManager.atlasRequested事件。
        纹理集中的图片的纹理类型需要设置为 Sprite (2D and UI)
        编辑器中的原始纹理集一定不能删除。
        不同电脑上的纹理集的.meta文件中的hash会不一样，建议只在一台电脑上打包assetbundle，其他电脑忽略.meta文件的变化。不要提交。
    
        方案1： Include in Build 勾选
        方案2： 不勾选，使用SpriteAtlasManager.atlasRequested事件回调
     */
    public class LoadSpriteAtlasExample : MonoBehaviour
    {
        [SerializeField]
        private Image mImage;

        private void Awake()
        {
            // 注册事件, 只有当啊 Atlas设置 build in 勾选后触发
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
            mImage = transform.Find("Image").GetComponent<Image>();
        }

        private void Start()
        {
            ResourceLoader loader = ResourceLoader.Allocate();
            SpriteAtlas spriteAtlas = loader.Load<SpriteAtlas>("spriteatlasV");

            // Include in Build 勾选时到逻辑
            //        Sprite square = spriteAtlas.GetSprite("CharCommunity_010");
            //        Log.Info(spriteAtlas.spriteCount);
            //        Sprite[] array = new Sprite[spriteAtlas.spriteCount];
            //        spriteAtlas.GetSprites(array);
            //        foreach (Sprite sprite in array) {
            //            Log.Info(sprite.name);
            //        }
            //        mImage.sprite = square;
        }

        private void OnAtlasRequested(string tag, Action<SpriteAtlas> action)
        {
            // 加载纹理集
            // unity2017.1版本：当收到回调后必须立刻用纹理集填充
            // SpriteAtlas atlas = LoadAtlas(tag);
            // action(atlas);
            // 2018.2.1f1版本：可以异步加载纹理集，只需向action回调填充纹理集就可以了
            // 当纹理集没有被填充前，Image等组件将显示为默认的白色纹理
            // 一旦纹理填充后，Image等组件将自动显示为正确的纹理

            // 同步
            SpriteAtlas sa = Resources.Load<SpriteAtlas>(tag);
            mImage.sprite = sa.GetSprite("CharCommunity_010");
            action(sa);

            // 异步
            // StartCoroutine(DoLoadAsset(action, tag));
        }

        private IEnumerator DoLoadAsset(Action<SpriteAtlas> action, string tag)
        {
            // var ab = AssetBundle.LoadFromFileAsync(tag);
            // yield return ab;
            // var sa = ab.assetBundle.LoadAsset(tag);
            // Object loadAsset = ab.assetBundle.LoadAsset(tag);
            // mImage.sprite = loadAsset.GetSprite("CharCommunity_001");
            // action((SpriteAtlas)sa);
            yield return null;

            // yield return new WaitForSeconds(3);
            // var ab = AssetBundle.LoadFromFileAsync(getpath(tag));
            // yield return ab;
            //
            // print("DoloadAsset frame:" + Time.frameCount);
            // var sa = ab.assetBundle.LoadAsset(tag);
            // print("sa: " + sa);
            // action(sa);
        }
    }
}
