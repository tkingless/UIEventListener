using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JTool.Editor.Windows;

namespace JTool.Editor.Component
{
public class EventComponent : BaseComponent {

	public EventComponent(string Name, EventEditorWindow win):base(Name, win)
	{
		Body = new Rect(30,250,100,100);
	}

	public override void OnGUI(int i)
	{
		base.OnGUI(i);
		
		Name = GUI.TextArea(new Rect(5,20,90,20), Name);
		
		if(GUI.Button(new Rect(5, 45,45,20), "連"))
		{
			win.AttachWindow(this);
		}
		else if(GUI.Button(new Rect(50, 45,45,20), "斷"))
		{
			win.DettachWindow(this);
		}
		else if(GUI.Button(new Rect(5, 65,90,20), "刪"))
		{
			win.RemoveWindow(this);
		}


		GUI.DragWindow();
	}
}
}
