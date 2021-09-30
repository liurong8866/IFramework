
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LoadSpriteAtlasExample : MonoBehaviour
{
    [SerializeField] private Image mImage;
    void Start()
    {
        ResourceLoader loader = ResourceLoader.Allocate();
        
        SpriteAtlas spriteAtlas = loader.Load<SpriteAtlas>("spriteatlasV");
        Sprite square = spriteAtlas.GetSprite("CharCommunity_001");
        Log.Info(spriteAtlas.spriteCount);

        Sprite[] array = new Sprite[spriteAtlas.spriteCount] ;
        spriteAtlas.GetSprites(array);
        
        foreach (Sprite sprite in array) {
            Log.Info(sprite.name); 
        }

        mImage.sprite = square;
    }

}
