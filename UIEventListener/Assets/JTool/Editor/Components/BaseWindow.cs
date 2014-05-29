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
				//public EventEditorWindow mEditWndHdr;

				public delegate void OnRemoveWindowDelegate (BaseWindow rmWnd);

				public event OnRemoveWindowDelegate OnRemoveEvent;

				public BaseWindow (string aName, EventEditorWindow win)
				{
						//here to specify the size of rect
						Rect initWndSize = new Rect (30, 250, 100, 100);
						mWndRect = initWndSize;
						this.mWndTitleName = aName;
				}

				public virtual void OnGUI (int i)
				{

						mTextRect = new Rect (5, 20, 90, 20);
						mCloseRectnew = new Rect (5, 65, 90, 20);

						mWndTitleName = GUI.TextArea (mTextRect, mWndTitleName);

						if (GUI.Button (mCloseRectnew, mCloseBtn)) {
								//RemoveWindow ();
								DispatchRemoveEvent ();
						}
						GUI.DragWindow ();
				}

				//===================================================================================================

				//this maybe made more universial like:
				//Dispatch(EventType)
				void DispatchRemoveEvent ()
				{

						if (OnRemoveEvent != null)
								OnRemoveEvent (this);
				}

				//========================================================================
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
				//=============================================================================
	
		}
}
