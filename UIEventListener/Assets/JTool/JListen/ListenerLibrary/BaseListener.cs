using UnityEngine;
using UnityEditor;
using System.Collections;

namespace JUITool
{
public class BaseListener{
	public float Height = 100.0f;

		public BaseListener()
		{

		}

	public enum ListenType
	{
		Update, FixUpdate, Timer, Dispatch, SceneEnter, SceneLeave
	}

	public ListenType type;

	public virtual void Init()
	{
	}

	public virtual bool Happen()
	{
		return false;
	}

	public virtual void OnGUI()
	{
		
	}
}
}
