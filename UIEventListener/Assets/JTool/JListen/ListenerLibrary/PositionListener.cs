using UnityEngine;
using UnityEditor;
using System.Collections;

namespace JUITool
{
	public class PositionListener : BaseListener 
	{

		GameObject target;
		Transform pos;
		float range = 10;

		public PositionListener():base()
		{
			Height = 130.0f;
		}

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
			GUI.Label(new Rect(5,5,90,20), "Target");
			target = EditorGUI.ObjectField(new Rect(5,25,90,20), target, typeof(GameObject), true) as GameObject;
			GUI.Label(new Rect(5,45,90,20), "Position");
			pos = EditorGUI.ObjectField(new Rect(5,65,90,20), pos, typeof(Transform), true) as Transform;
			GUI.Label(new Rect(5,85,90,20), "Radius");
			range = EditorGUI.FloatField(new Rect(5,105,90,20), range);
		}
	}
}
