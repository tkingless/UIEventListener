using UnityEngine;
using System.Collections;
using JUITool;

namespace JUITool
{
		public class BaseWindow
		{
				//This class is to collect the common properties that will show on Event editor windows(actually should not just Event editor), though from the naming, it should have broader abstraction
				[System.Flags]
				public enum WindowType
				{
						tUndefine=0,
						tEvent=1,
						tListner=2,
						tNew=4
				}

				[System.Flags]
				public enum WindowProperty
				{
						none=0,
						connectable=1
				}

				WindowType mWndType = WindowType.tUndefine;
				WindowProperty mWndProp = WindowProperty.none;

				/// <summary>
				/// The name of the window top bar
				/// </summary>
				public string mWndTitleName;
				/// <summary>
				/// The label for close button, suppose all windows has close button
				/// </summary>
				private string mCloseBtn = "Close";
				public Rect mWndRect;
				private Rect mTextRect;
				private Rect mCloseRectnew;
				public EventEditorWindow mEditWndHdr;

				public BaseWindow (string aName, EventEditorWindow win)
				{
						this.mWndTitleName = aName;
						this.mEditWndHdr = win;
				}

				public virtual void OnGUI (int i)
				{
						mTextRect = new Rect (5, 20, 90, 20);
						mCloseRectnew = new Rect (5, 65, 90, 20);

						mWndTitleName = GUI.TextArea (mTextRect, mWndTitleName);

						if (GUI.Button (mCloseRectnew, mCloseBtn)) {
								OnRemoveWindow ();
						}
						GUI.DragWindow ();
				}

				//acutally need to be rewritten
				public void OnRemoveWindow ()
				{
						if (this.GetType () == typeof(JUITool.ConnectableWnd)) {
				//this loop is no good to be here
								ConnectableWnd temp = (ConnectableWnd)this;
								foreach (ConnectableWnd frd in temp.mConnectedWnd)
										frd.mConnectedWnd.Remove ((ConnectableWnd)this);

						}
						if (mEditWndHdr != null) {
								if (mEditWndHdr.GetOnWindows ().Contains (this))
										mEditWndHdr.GetOnWindows ().Remove (this);

						} else
								Debug.Log ("Error, mEditWndHdr is null!!");

						if (ConnectableWnd.mPairWnd1 == this) {
								//this is not a good practice putting ConnectableWnd things here.
								ConnectableWnd.resetMeta ();
								Debug.Log ("resetMeta() called due to closing meta window");
						}
						//this is not good practice EventEditorWindow thing hooked here
						mEditWndHdr.ClearNodeCurvePairs ();

				}

				public void SetWndType (BaseWindow.WindowType wndType)
				{
						this.mWndType = wndType;
				}

				public void SetWndProp (BaseWindow.WindowProperty wndProp)
				{
						this.mWndProp = wndProp;
				}
				
				public BaseWindow.WindowProperty GetWndProp ()
				{
						return this.mWndProp;
				}
	
		}
}
