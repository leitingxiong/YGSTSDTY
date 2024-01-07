using System;

public class ButtonHandler
{
	public string showDescription = "";
	public Action onClickCallBack;

	/// <summary>
	/// 两参构造函数
	/// </summary>
	/// <param name="Description">文本描述</param>
	/// <param name="callBack">回调方法</param>
	public ButtonHandler(string Description, Action callBack)
	{
		showDescription = Description;
		onClickCallBack = callBack;
	}
}