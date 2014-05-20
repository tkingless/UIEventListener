using UnityEngine;
using UnityEditor;
using System.Collections;

namespace JTool.JListen.ListenerLibrary
{
public class PositionListener : BaseListener {

	GameObject target;
	Transform pos;
	float range;


	// [0]GameObject Target, [1]Transform Location, [2]Float CircularRange
	public override void Init()
	{
		type = ListenType.Update;
	}

	public override bool Happen()
	{
		return Vector3.Distance(target.transform.position, pos.position) <= range;
	}

	public override void OnGUI()
	{
		target = EditorGUI.ObjectField(new Rect(5,5,90,20), target, typeof(GameObject), true) as GameObject;
		pos = EditorGUI.ObjectField(new Rect(5,25,90,20), pos, typeof(Transform), true) as Transform;
		range = EditorGUI.FloatField(new Rect(5,45,90,20), "半徑範圍", range);
	}
}
}
