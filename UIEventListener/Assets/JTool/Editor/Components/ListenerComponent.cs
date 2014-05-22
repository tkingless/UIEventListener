using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using JTool.Editor.Windows;
using JTool.JListen.ListenerLibrary;

namespace JTool.Editor.Component
{
		public class ListenerComponent : BaseComponent
		{

				BaseListener mListener;

				//here to specify the size of rect and btn name, this level is tended to design the window appearance and loaded function

				public ListenerComponent (string Name, EventEditorWindow win):base(Name, win)
				{
						SetWndType (WindowType.tListner);
						Rect initWndSize = new Rect (30, 200, 100, 120);
						mWndRect = initWndSize;
				}

				public override void OnGUI (int i)
				{
						base.OnGUI (i);

						Rect listenerRect = new Rect (5, 85, 90, 20);

						mListener = EditorGUI.ObjectField (listenerRect, mListener, typeof(MonoScript), false) as BaseListener;
		
						System.Type tt = System.Type.GetType ("JTool.JListen.ListenerLibrary.PositionListener");
		
						//Debug.Log(tt);
//		System.Type tt = .GetType("JTool.JListen.ListenerLibrary.PositionListener");
//		System.Object obj = Assembly.GetExecutingAssembly().GetType("PositionListener");

//		Debug.Log(tt);

						if (mListener != null)
								mListener.OnGUI ();
				}
		}
}
