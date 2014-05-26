using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using JUITool;

namespace JUITool
{
	public class ListenerWnd : ConnectableWnd
	{

		BaseListener mListener;
		MonoScript mListenerScript;

				//here to specify the size of rect and btn name, this level is tended to design the window appearance and loaded function

		public ListenerWnd (string Name, EventEditorWindow win):base(Name, win)
		{
			SetWndType (WindowType.tListner);
			Rect initWndSize = new Rect (30, 200, 100, 120);
			mWndRect = initWndSize;
		}

		public override void OnGUI (int i)
		{
			base.OnGUI (i);
			Rect listenerRect = new Rect (5, 85, 90, 20);

			mListenerScript = (MonoScript)EditorGUI.ObjectField (listenerRect, mListenerScript, typeof(MonoScript), false);
		
			if(mListenerScript != null)
			{
				System.Type tt = mListenerScript.GetClass();
				if(tt == typeof(BaseListener) || tt.BaseType == typeof(BaseListener))
					if((mListener != null && mListener.GetType().Name != tt.Name) || mListener == null)
					{
						mListener = Activator.CreateInstance(tt) as BaseListener;
						Debug.Log(mListener.GetType().Name);
					}
			}

			if (mListener != null)
			{
				GUI.BeginGroup(new Rect(0,110,100,mListener.Height));
				mWndRect.height = 120 + mListener.Height;
				mListener.OnGUI ();
				GUI.EndGroup();
			}
			else
				mWndRect.height= 120;
		}
	}
}
