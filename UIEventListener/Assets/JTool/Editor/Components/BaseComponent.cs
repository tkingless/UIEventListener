using UnityEngine;
using System.Collections;
using JTool.Editor.Windows;

namespace JTool.Editor.Component
{
public class BaseComponent {

	public string Name;
	public Rect Body;
	public EventEditorWindow win;

	public BaseComponent(string Name, EventEditorWindow win)
	{
		this.Name = Name;
		this.win = win;
	}

	public virtual void OnGUI(int i) 
	{

	}

	public void Attaching()
	{
		Color origin = GUI.color;
		GUI.color = Color.red;
		GUI.Box(new Rect(Body.x, Body.y, 100,15), "");
		GUI.color = origin;
	}

}
}
