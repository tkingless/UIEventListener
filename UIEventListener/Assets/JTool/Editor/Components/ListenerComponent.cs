using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using JTool.Editor.Windows;
using JTool.JListen.ListenerLibrary;

namespace JTool.Editor.Component
{
public class ListenerComponent : BaseComponent {

	BaseListener listener;

	public ListenerComponent(string Name, EventEditorWindow win):base(Name, win)
	{
		Body = new Rect(30,200,100,120);
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

		listener = EditorGUI.ObjectField(new Rect(5,85,90,20), listener, typeof(MonoScript), false) as BaseListener;

		
		System.Type tt = System.Type.GetType("JTool.JListen.ListenerLibrary.PositionListener");
		Debug.Log(tt);
//		System.Type tt = .GetType("JTool.JListen.ListenerLibrary.PositionListener");
//		System.Object obj = Assembly.GetExecutingAssembly().GetType("PositionListener");

//		Debug.Log(tt);

		if(listener != null)
			listener.OnGUI();

		GUI.DragWindow();
	}
}
}
