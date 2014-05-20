using UnityEngine;
using System.Collections;

namespace JTool.JListen.EventLibrary
{
public class BaseEvent {

	public string type;
	public BaseEvent(string type)
	{
		this.type = type;
	}
}
}
