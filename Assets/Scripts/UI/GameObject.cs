using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class GameObjectData : UIPanelData
	{
	}
	public partial class GameObject : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GameObjectData ?? new GameObjectData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
