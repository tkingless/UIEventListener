using UnityEngine;
using System.Collections;
using JTool.Editor.Windows;

namespace JTool.Editor.Component
{
public class BaseComponent {
	//This class is to collect the common properties that will show on editor windows
	
	[System.Flags]
	public enum WindowType {tUndefine=0, tEvent=1, tListner=2, tNew=4}

	[System.Flags]
		public enum WindowProperty{none=0, connectable=1}

	WindowType mWndType = WindowType.tUndefine;
		WindowProperty mWndProp = WindowProperty.none;

	public string mWndTitleName;

	//Button names that already have function, designed to be prototype, therefore put on parent class level. Since I suppose for same functiong button
	//the button name should be same
	
	
	//consider need interface here : a97077
		private string mLinkBtn = "Link";
		private string mDetachkBtn = "Break";
		private string mCloseBtn = "Close";
	
		public Rect mWndRect;
		private Rect mTextRect;
		private Rect mLinkRect;
		private Rect mDetachRect;
		private Rect mCloseRectnew;

	
	public EventEditorWindow mEditWndHdr;

	public BaseComponent(string aName, EventEditorWindow win)
	{
		this.mWndTitleName = aName;
		this.mEditWndHdr = win;

			//for specific types of windows, if they inherit the connectable windows interface, then do the corresponding instantiation
			//TODO:
	}

	public virtual void OnGUI(int i) 
	{
			//so far I know for Event and Listener windows has gui layout like this, so I put this common property here:
			//especially I want here to see the difference of layouts for types, but nothing absolute, even when no target settled
			if ((mWndType & (WindowType.tEvent|WindowType.tListner))!=0){

				mTextRect = new Rect(5,20,90,20);
				mLinkRect = new Rect(5, 45,45,20);
				mDetachRect = new Rect(50, 45,45,20);
				mCloseRectnew = new Rect(5, 65,90,20);

				mWndTitleName = GUI.TextArea(mTextRect, mWndTitleName);

				if(GUI.Button(mLinkRect, mLinkBtn))
				{
					mEditWndHdr.AttachWindow(this);
				}
				else if(GUI.Button(mDetachRect, mDetachkBtn))
				{
					mEditWndHdr.DettachWindow(this);
				}
				else if(GUI.Button(mCloseRectnew, mCloseBtn))
				{
					mEditWndHdr.RemoveWindow(this);
				}

			}
			GUI.DragWindow();
	}

	public void Attaching()
	{
		Color origin = GUI.color;
		GUI.color = Color.red;
		GUI.Box(new Rect(mWndRect.x, mWndRect.y, 100,15), "");
		GUI.color = origin;
	}

	public void SetWndType (BaseComponent.WindowType wndType){
			this.mWndType = wndType;
		}
}
}
