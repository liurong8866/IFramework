using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace IFramework.Example
{
	// Generate Id:3bb92a33-58de-4463-8ac6-7dda3584cb84
	public partial class UIHomePanel
	{
		public const string Name = "UIHomePanel";
		
		
		private UIHomePanelData mPrivateData = null;
		 
		
		public UIHomePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIHomePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIHomePanelData());
			}
			set
			{
				data = value;
				mPrivateData = value;
			}
		}
	}
}
