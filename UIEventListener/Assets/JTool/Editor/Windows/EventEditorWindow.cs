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
				private WndContainer mWnds = new WndContainer ();
	
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
						foreach (WndContainer.ConnectPair aPair in mWnds.GetConnectPair())
								DrawNodeCurve (aPair.wndA.mWndRect, aPair.wndB.mWndRect);
						#endregion Link


						#region window
						BeginWindows ();
						List<BaseWindow> temp = mWnds.GetWnds ();
						for (int i = 0; i < temp.Count; i++) {
								temp [i].mWndRect = GUI.Window (i, temp [i].mWndRect, temp [i].OnGUI, temp [i].mWndTitleName);
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
								mWnds.Add (new ListenerWnd ("LinkWndL", this));
						}
						// Button to add Event
						else if (GUI.Button (newEventRect, newEventBtn)) {
								//windows.Add(new EventWnd(newEventBtn, this));
								mWnds.Add (new ConnectableWnd ("LinkWndE", this));
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
		}
}
