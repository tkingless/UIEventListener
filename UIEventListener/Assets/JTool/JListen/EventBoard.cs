using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Diagnostics;
using JUITool;

namespace JUITool
{
public class EventBoard {
	

	private static Dictionary<string,List<Action<BaseEvent>>> EventList = new Dictionary<string,List<Action<BaseEvent>>>();

	public static void add(string ListenKey, Action<BaseEvent> Method)
	{
		if(!EventList.ContainsKey(ListenKey))
			EventList.Add(ListenKey, new List<Action<BaseEvent>>());

		if(!EventList[ListenKey].Contains(Method))
			EventList[ListenKey].Add(Method);
	}

	public static void remove(string ListenKey, Action<BaseEvent> Method)
	{
		if(!EventList.ContainsKey(ListenKey))
			return;
		Action<BaseEvent> removal = EventList[ListenKey].Find(x => x == Method);
		EventList[ListenKey].Remove(removal);
		if(EventList[ListenKey].Count == 0)
			EventList.Remove(ListenKey);
	}

	public static bool has(string ListenKey, Action<BaseEvent> Method)
	{
		return (EventList.ContainsKey(ListenKey) && EventList[ListenKey].Contains(Method));
	}

	public static bool has(string ListenKey)
	{
		return EventList.ContainsKey(ListenKey);
	}

	public static void dis(BaseEvent e)
	{
		string type = e.type;
		if(!EventList.ContainsKey(type))
			return;

		List<Action<BaseEvent>> HandlerList = EventList[type];

		for(int i = 0; i < HandlerList.Count; i++)
		{
			HandlerList[i](e);
		}
	}
}
}
