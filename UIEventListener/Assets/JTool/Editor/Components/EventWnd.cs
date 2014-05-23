using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JUITool;

namespace JUITool
{
public class EventWnd : BaseWindow {
	
	//here to specify the size of rect and btn name, this level is tended to design the window appearance and loaded function
	
	public EventWnd(string Name, EventEditorWindow win):base(Name, win)
	{
			SetWndType (WindowType.tEvent);
			//here to specify the size of rect and btn name
			Rect initWndSize = new Rect(30,250,100,100);
			mWndRect = initWndSize;
	}

	public override void OnGUI(int i)
	{
		base.OnGUI(i);
	}
}
}
