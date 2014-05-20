using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using JTool.Editor.Component;

namespace JTool.Editor.Windows
{
public class EventEditorWindow : EditorWindow {
	public float zoomScale = 1;
	public Vector2 scrollPos = Vector2.zero;
	public List<BaseComponent> windows = new List<BaseComponent>();
	List<BaseComponent> windowsToAttach = new List<BaseComponent>();
	List<BaseComponent> windowsToDettach = new List<BaseComponent>();
	List<BaseComponent> attachedWindows = new List<BaseComponent>();

	[MenuItem("Window/事件編輯器")]
	static void ShowEditor() 
	{
		EventEditorWindow editor = EditorWindow.GetWindow<EventEditorWindow>();
	}
	
	void OnGUI() {
		scrollPos = GUI.BeginScrollView(new Rect (0, 100, position.width, position.height-100), scrollPos, new Rect (0, 100, 2000 * zoomScale, 1500 * zoomScale),true, true);

		#region Resize by using GUI matrix
		Matrix4x4 before = GUI.matrix;
		Matrix4x4 Translation = Matrix4x4.TRS(new Vector3(0,100,0),Quaternion.identity,Vector3.one);
		Matrix4x4 Scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
		GUI.matrix = Translation*Scale*Translation.inverse;

		#region Link
		if (attachedWindows.Count >= 2)
		{
			for (int i = 0; i < attachedWindows.Count; i += 2)
				DrawNodeCurve(attachedWindows[i].Body, attachedWindows[i + 1].Body);
		}
		foreach(BaseComponent wta in windowsToAttach)
			wta.Attaching();
		#endregion Link


		#region window
		BeginWindows();

		for( int i = 0; i < windows.Count; i++)
		{
			windows[i].Body = GUI.Window(i, windows[i].Body, windows[i].OnGUI, windows[i].Name);
		}
		
		EndWindows();
		#endregion window

		GUI.matrix = before;

		#endregion Recuse the Matrix

		GUI.EndScrollView ();

		BasicUI();
	}

	// Draw the Control GUI here
	void BasicUI()
	{
		// Container
		GUI.Box(new Rect(0,0, position.width, 100), "");

		// Button to add Object
		if(GUI.Button(new Rect(10,10,180,80), "新增監聽器"))
		{
			windows.Add(new ListenerComponent("新增監聽器", this));
		}
		// Button to add Event
		else if(GUI.Button(new Rect(210,10,180,80), "新增事件"))
		{
			windows.Add(new EventComponent("新增事件", this));
		}
		//Zoom slider
		zoomScale = GUI.HorizontalSlider(new Rect(10,110,position.width-20, 20), zoomScale, 1.0f, 3.0f);
	}

	void DrawNodeCurve(Rect start, Rect end) 
	{
		Vector3 startPos = new Vector3(start.x + start.width/2, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3(end.x + end.width/2, end.y + end.height / 2, 0);
		Color shadowCol = new Color(0, 0, 0, 0.06f);
		for (int i = 0; i < 3; i++)
			Handles.DrawBezier(startPos, endPos, startPos, endPos, shadowCol, null, (i + 1) * 5);
		Handles.DrawBezier(startPos, endPos, startPos, endPos, Color.black, null, 1);
	}

	public void AttachWindow(BaseComponent win)
	{
		if(windowsToAttach.Contains(win))
		{
			windowsToAttach.Remove(win);
		}
		else
		{
			windowsToAttach.Add(win);
		}
		if (windowsToAttach.Count == 2) 
		{
			attachedWindows.Add(windowsToAttach[0]);
			attachedWindows.Add(windowsToAttach[1]);
			windowsToAttach.Clear();
		}
	}

	public void RemoveWindow(BaseComponent win)
	{
		List<BaseComponent> attachRemoveList = new List<BaseComponent>();
		for (int i = 0; i < attachedWindows.Count; i += 2)
		{
			if(attachedWindows[i] == win || attachedWindows[i+1] == win)
			{
				attachRemoveList.Add(attachedWindows[i]);
				attachRemoveList.Add(attachedWindows[i+1]);
			}
		}

		foreach(BaseComponent at in attachRemoveList)	
			attachedWindows.Remove(at);
		windows.Remove(win);
	}

	public void DettachWindow(BaseComponent win)
	{
		if(windowsToDettach.Contains(win))
		{
			windowsToDettach.Remove(win);
		}
		else
		{
			windowsToDettach.Add(win);
		}

		if(windowsToDettach.Count == 2)
		{
			for (int i = 0; i < attachedWindows.Count; i += 2)
			{
				if(attachedWindows[i] == windowsToDettach[0] || attachedWindows[i] == windowsToDettach[1])
				{
					if(attachedWindows[i+1] == windowsToDettach[0] || attachedWindows[i+1] == windowsToDettach[1])
					{
						attachedWindows.RemoveRange(i,2);
					}
				}
			}
			windowsToDettach.Clear();
		}
	}
}
}
