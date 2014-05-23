using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using JUITool;

namespace JUITool
{
		public class EventEditorWindow : EditorWindow
		{
				public float zoomScale = 1;
				public Vector2 scrollPos = Vector2.zero;
				private List<BaseWindow> mOnScreenWindows = new List<BaseWindow> ();
				List<BaseWindow> windowsToAttach = new List<BaseWindow> ();
				List<BaseWindow> windowsToDettach = new List<BaseWindow> ();
				List<BaseWindow> attachedWindows = new List<BaseWindow> ();
	
				[MenuItem("JUI/Event Listener Editor")]
				static void ShowEditor ()
				{
						EventEditorWindow editor = EditorWindow.GetWindow<EventEditorWindow> ();
				}
	
				void OnGUI ()
				{
						scrollPos = GUI.BeginScrollView (new Rect (0, 100, position.width, position.height - 100), scrollPos, new Rect (0, 100, 512 * zoomScale, 512 * zoomScale), false, false);

						#region Resize by using GUI matrix
						Matrix4x4 before = GUI.matrix;
						Matrix4x4 Translation = Matrix4x4.TRS (new Vector3 (0, 100, 0), Quaternion.identity, Vector3.one);
						Matrix4x4 Scale = Matrix4x4.Scale (new Vector3 (zoomScale, zoomScale, 1.0f));
						GUI.matrix = Translation * Scale * Translation.inverse;

						#region Link
#if false
		if (attachedWindows.Count >= 2)
		{
			for (int i = 0; i < attachedWindows.Count; i += 2)
				DrawNodeCurve(attachedWindows[i].mWndRect, attachedWindows[i + 1].mWndRect);
		}
		//foreach(BaseComponent wta in windowsToAttach)
			//wta.Attaching();
#endif
						#endregion Link


						#region window
						BeginWindows ();

						for (int i = 0; i < mOnScreenWindows.Count; i++) {
								mOnScreenWindows [i].mWndRect = GUI.Window (i, mOnScreenWindows [i].mWndRect, mOnScreenWindows [i].OnGUI, mOnScreenWindows [i].mWndTitleName);
						}
		
						EndWindows ();
						#endregion window

						GUI.matrix = before;

						#endregion Recuse the Matrix

						GUI.EndScrollView ();

						BasicUI ();
				}

				// Draw the Control GUI here
				private static readonly string newListenerBtn = "New Listener";
				private static readonly string newEventBtn = "New Event";
				//layout design
				Rect newEventRect = new Rect (210, 10, 180, 80) ;
				Rect newListenerRect = new Rect (10, 10, 180, 80);

				void BasicUI ()
				{
						// Container
						GUI.Box (new Rect (0, 0, position.width, 100), "");

						// Button to add Object
						if (GUI.Button (newListenerRect, newListenerBtn)) {
								//windows.Add(new ListenerWnd(newListenerBtn, this));
								mOnScreenWindows.Add (new ConnectableWnd ("LinkWnd", this));
						}
		// Button to add Event
			else if (GUI.Button (newEventRect, newEventBtn)) {
								//windows.Add(new EventWnd(newEventBtn, this));
						}
						//Zoom slider
						zoomScale = GUI.HorizontalSlider (new Rect (10, 110, position.width - 20, 20), zoomScale, 1.0f, 3.0f);
				}
	
				//this is well separated function, no need to do restructure
				void DrawNodeCurve (Rect start, Rect end)
				{
						Vector3 startPos = new Vector3 (start.x + start.width / 2, start.y + start.height / 2, 0);
						Vector3 endPos = new Vector3 (end.x + end.width / 2, end.y + end.height / 2, 0);
						Color shadowCol = new Color (0, 0, 0, 0.06f);
						for (int i = 0; i < 3; i++)
								Handles.DrawBezier (startPos, endPos, startPos, endPos, shadowCol, null, (i + 1) * 5);
						Handles.DrawBezier (startPos, endPos, startPos, endPos, Color.black, null, 1);
				}

				public void AttachWindow (BaseWindow win)
				{
						if (windowsToAttach.Contains (win)) {
								windowsToAttach.Remove (win);
						} else {
								windowsToAttach.Add (win);
						}
						if (windowsToAttach.Count == 2) {
								attachedWindows.Add (windowsToAttach [0]);
								attachedWindows.Add (windowsToAttach [1]);
								windowsToAttach.Clear ();
						}
				}

				/*public void OnRemoveWindow(BaseWindow win)
	{
		List<BaseWindow> attachRemoveList = new List<BaseWindow>();
		for (int i = 0; i < attachedWindows.Count; i += 2)
		{
			if(attachedWindows[i] == win || attachedWindows[i+1] == win)
			{
				attachRemoveList.Add(attachedWindows[i]);
				attachRemoveList.Add(attachedWindows[i+1]);
			}
		}

		foreach(BaseWindow at in attachRemoveList)	
			attachedWindows.Remove(at);
		mOnScreenWindows.Remove(win);
	}*/

				public void DettachWindow (BaseWindow win)
				{
						if (windowsToDettach.Contains (win)) {
								windowsToDettach.Remove (win);
						} else {
								windowsToDettach.Add (win);
						}

						if (windowsToDettach.Count == 2) {
								for (int i = 0; i < attachedWindows.Count; i += 2) {
										if (attachedWindows [i] == windowsToDettach [0] || attachedWindows [i] == windowsToDettach [1]) {
												if (attachedWindows [i + 1] == windowsToDettach [0] || attachedWindows [i + 1] == windowsToDettach [1]) {
														attachedWindows.RemoveRange (i, 2);
												}
										}
								}
								windowsToDettach.Clear ();
						}
				}

				public List<BaseWindow> GetOnWindows ()
				{

						return this.mOnScreenWindows;
				}
		}
}
