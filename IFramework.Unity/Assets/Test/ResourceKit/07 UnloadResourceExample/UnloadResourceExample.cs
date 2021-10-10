using System.Collections;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Test.AssetResourceKit
{
    public class UnloadResourceExample : MonoBehaviour
    {
        private IEnumerator Start()
        {
            Image image = transform.Find("Image").GetComponent<Image>();
            ResourceLoader resLoader = ResourceLoader.Allocate();
            Texture2D texture2D = resLoader.Load<Texture2D>("Code");

            // create Sprite 扩展
            Sprite sprite = texture2D.CreateSprite();
            image.sprite = sprite;

            // 添加关联的 Sprite
            resLoader.DestroyOnRecycle(sprite);
            yield return new WaitForSeconds(5);

            resLoader.Recycle();
            resLoader = null;
        }
    }
}