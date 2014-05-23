using UnityEngine;
using UnityEditor;
using System.Collections;

namespace JUITool
{
public interface JInteractive3D
{
	void OnPress();
	void OnDown();
	void OnUp();
}


[RequireComponent (typeof (Camera))]
public class JCamera3D : MonoBehaviour {


	public enum Control{
		Click, Touch
	}

	public Control Method;

	void Update () 
	{
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Transform currentObject = null;
		JInteractive3D curI = null;

		if (Physics.Raycast (ray, out hit)) 
		{
			currentObject = hit.transform;
			if(currentObject)
			{
				try
				{
					curI = (JInteractive3D)currentObject.GetComponent(typeof(JInteractive3D));
				}
				catch{}
			}

			if(curI != null)
			{
				if(isDown)
					curI.OnDown();
				else if(isUp)
					curI.OnUp();
				else if(isPress)
					curI.OnPress();
			}
		}
	}

	bool isDown
	{
		get
		{
			switch(Method)
			{
			case Control.Click:
				return Input.GetMouseButtonDown(0);
			case Control.Touch:
				return false;
			default:
				return false;
			}
		}
	}

	bool isUp
	{
		get
		{
			switch(Method)
			{
			case Control.Click:
				return Input.GetMouseButtonUp(0);
			case Control.Touch:
				return false;
			default:
				return false;
			}
		}
	}

	bool isPress
	{
		get
		{
			switch(Method)
			{
			case Control.Click:
				return Input.GetMouseButton(0);
			case Control.Touch:
				return false;
			default:
				return false;
			}
		}
	}
}
}
