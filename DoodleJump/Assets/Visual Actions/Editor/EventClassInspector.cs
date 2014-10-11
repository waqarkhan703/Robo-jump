using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EventClass))]
public class EventClassInspector : Editor 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	public override void OnInspectorGUI()
	{
		//base.DrawDefaultInspector();
		EditorGUILayout.FloatField(10);
	}
}
