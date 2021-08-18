using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Example
{
	public class AssetBundleResExample : MonoBehaviour
	{
		private ResourceLoader mResLoader = ResourceLoader.Allocate();

		public RawImage RawImage;

		// Use this for initialization
		void Start()
		{
			RawImage rawImage = transform.Find("RawImage").GetComponent<RawImage>();

			RawImage.texture = mResLoader.Load<Texture2D>("TestImage");
			
			// 通过下边方式也一样
			RawImage.texture = mResLoader.Load<Texture2D>("TestImage","testimage-png");
		}

		private void OnDestroy()
		{
			mResLoader.Recycle();
			mResLoader = null;
		}
	}
}