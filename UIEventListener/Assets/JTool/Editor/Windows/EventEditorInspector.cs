using UnityEngine;
using UnityEditor;
using System.Collections;

namespace JUITool
{
	[CustomEditor(typeof(EventHandler))]
	public class EventEditorInspector : Editor {
		EventHandler handler;

		public void OnEnable()
		{
			handler = (EventHandler)target;
		}
		public override void OnInspectorGUI()
		{
			if(GUILayout.Button("Edit",  GUILayout.Width(255)))
			{
				EventEditorWindow window = (EventEditorWindow) EditorWindow.GetWindow(typeof(EventEditorWindow));
				window.Init();
			}
		}
	}
}
