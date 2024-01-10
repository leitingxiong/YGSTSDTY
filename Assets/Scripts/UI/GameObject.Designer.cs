using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:8a5857ce-5212-41ae-b360-cb4591f5a992
	public partial class GameObject
	{
		public const string Name = "GameObject";
		
		
		private GameObjectData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public GameObjectData Data
		{
			get
			{
				return mData;
			}
		}
		
		GameObjectData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GameObjectData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
