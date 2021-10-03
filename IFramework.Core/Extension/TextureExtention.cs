using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// Texture图片扩展方法
    /// </summary>
    public static class TextureExtention
    {
        public static Sprite CreateSprite(this Texture2D self) { return Sprite.Create(self, new Rect(0, 0, self.width, self.height), Vector2.one * 0.5f); }

        // 屏幕截图
        // var screenshotTexture2D = Camera.main.CaptureCamera(new Rect(0, 0, Screen.width, Screen.height));
        // Log.I(screenshotTexture2D.width);
        public static Texture2D CaptureCamera(this Camera camera, Rect rect)
        {
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.Destroy(renderTexture);
            return screenShot;
        }

        public static Color HtmlStringToColor(this string htmlString)
        {
            Color retColor;
            bool parseSucceed = ColorUtility.TryParseHtmlString(htmlString, out retColor);
            return parseSucceed ? retColor : Color.black;
        }

        // public static T ColorAlpha<T>(this T self, float alpha) where T : Graphic
        // {
        //     var color = self.color;
        //     color.a = alpha;
        //     self.color = color;
        //     return self;
        // }
    }
}
