using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.Text.RegularExpressions;
using System.Collections.Generic;

[CustomEditor(typeof(VisualActions))]
public class VisualActionsInspector : ActionSequenceInspector
{
	/*
	SerializedProperty _onMouseClick;
	SerializedProperty _onMouseHover;
	SerializedProperty _onMouseWheelMove;
	SerializedProperty _onAccelerometerChange;
	SerializedProperty _atStart;
	SerializedProperty _onTap;
	SerializedProperty _onCollision;
	SerializedProperty _onCollisionWithCharacter;
	SerializedProperty _onTrigger;
	SerializedProperty _onKeyPress;
	SerializedProperty _pressedKey;
	SerializedProperty _pressedButton;
	SerializedProperty _isFrameRateIndependent;
	
	SerializedProperty _onUpdate;
	SerializedProperty _onFixedUpdate;
	*/

	SerializedProperty _isEventFoldoutOpen;
	SerializedProperty _isSettingsShownProperty;
	
	private SerializedObject m_Object;
	//Handle to the VisualActions script that this inspector refers to
	private VisualActions _targetComponent;
	//Handle to GameObject that holds the VisualActions script that this inspector refers to
	private GameObject _targetGameObject;
	
	private static GUILayoutOption buttonWidth = GUILayout.MaxWidth(20f);

	private static GUIContent
		insertEvent = new GUIContent("+", "Insert new Events"),
		deleteEvent = new GUIContent("-", "Delete this Event"),
		showSettings = new GUIContent ("\u2637", "Show parameters");

	//private static GUIContent showSettings;

	public override void OnEnable()
	{
		base.OnEnable();
		m_Object = new SerializedObject(target);

		//Handle to the VisualActions script that this inspector refers to
		_targetComponent = m_Object.targetObject as VisualActions;
		//Handle to GameObject that holds the VisualActions script that this inspector refers to
		_targetGameObject = _targetComponent.gameObject;

		_isEventFoldoutOpen = m_Object.FindProperty("IsEventFoldoutOpen");

		//showSettings = new GUIContent (string.Empty, EditorGUIUtility.LoadRequired("Black_Settings.png") as Texture2D, "Show parameters");

	}


	public override void OnInspectorGUI()
	{
		/*
		if(GUILayout.Button("\u2642") )
		{
			//Camera[] camList = InternalEditorUtility.GetSceneViewCameras();

			//AssetDatabase.CreateAsset(aCloneList, "Assets/VAComponent.asset");
			//AssetDatabase.SaveAssets();

			//Create container object which will be saved in prefab
			GameObject containerObject = new GameObject();
			containerObject.name = "VA_" + (target as VisualActions).Actions[0].MethodName;

			//Add the main VA component
			Component destComponent = containerObject.AddComponent<VisualActions>();
			EditorUtility.CopySerialized(target, destComponent);

			//Add the Event components
			//All components attached to the GameObject that is the target of this inspector
			EventClass[] eventComponents = (target as VisualActions).gameObject.GetComponents<EventClass>();
			
			if(eventComponents != null)
			{
				//Show all the events that are linked to this particular VisualActions script
				for(int i=0; i<eventComponents.Length; i++)
				{
					if(eventComponents[i].Target == target as VisualActions)
					{
						destComponent = containerObject.AddComponent(eventComponents[i].GetType().ToString());
						EditorUtility.CopySerialized(eventComponents[i], destComponent);
						(destComponent as EventClass).Target = containerObject.GetComponent<VisualActions>();
					}
					
				}
			}

			PrefabUtility.CreatePrefab("Assets/" + containerObject.name+".prefab", containerObject);
			DestroyImmediate(containerObject);

			//AssetDatabase.CreateAsset(containerObject, "Assets/VAComponent.asset");
			//AssetDatabase.SaveAssets();

		}*/
		GUILayout.Label("Action(s) to perform:", /*EditorStyles.boldLabel*/new GUIStyle ("OL Title") );
		base.OnInspectorGUI();
		
		m_Object.Update();
		ShowEventsGUI();

		m_Object.ApplyModifiedProperties();
	}
	
	
	void ShowEventsGUI()
	{
		EditorGUILayout.Separator();
		
		/*
		GUILayout.Box("A Box and some new test", new GUIStyle ("OL Box") );
		GUILayout.Box("A Box and some new test", new GUIStyle ("OL Title") );
		GUILayout.Box("A Box and some new test", new GUIStyle ("TextField") );
		GUILayout.Box("A Box and some new test", new GUIStyle ("helpbox") );
		*/
		
		GUILayout.Label("When to perform Action(s):", /*EditorStyles.boldLabel*/ new GUIStyle ("OL Title") );

		//EVENTS FOLD-OUT
		EditorGUILayout.BeginHorizontal();
		_isEventFoldoutOpen.boolValue = EditorGUILayout.Foldout(_isEventFoldoutOpen.boolValue, new GUIContent("Events") );
		if(GUILayout.Button(insertEvent, EditorStyles.miniButton, buttonWidth))
		{
			EventsEditorWindow.Init(_targetComponent);
		}
		EditorGUILayout.EndHorizontal();

		if(_isEventFoldoutOpen.boolValue == true)
		{

			//All components attached to the GameObject that is the target of this inspector
			EventClass[] eventComponents = _targetGameObject.GetComponents<EventClass>();

			if(eventComponents != null)
			{
				//Show all the events that are linked to this particual VisualActions script
				for(int i=0; i<eventComponents.Length; i++)
				{
					if(eventComponents[i].Target == _targetComponent)
					{

						string fullName = eventComponents[i].GetType().Name;

						if(fullName.Substring(fullName.Length - 5).Equals("Event") )
							fullName = fullName.Remove(fullName.Length - 5, 5);	//Remove "Event"

						//Put spaces, i.e,
						//Split by capital letters
						var newString = new Regex(@"
                							(?<=[A-Z])(?=[A-Z][a-z]) |
                 							(?<=[^A-Z])(?=[A-Z]) |
                 							(?<=[A-Za-z])(?=[^A-Za-z])", 
						                  	RegexOptions.IgnorePatternWhitespace
						                  );

						string newName = newString.Replace(fullName, " ");

						EditorGUILayout.BeginHorizontal();
							//DELETE button
							if(GUILayout.Button(deleteEvent, EditorStyles.miniButtonLeft, GUILayout.MaxWidth(17)))
							{
								DestroyImmediate(eventComponents[i]);
								return;
							}
							//NAME
							EditorGUILayout.LabelField(newName);
							/*
							bool isShown = true;
							//SHOW/HIDE
							if(eventComponents[i].hideFlags == HideFlags.HideInInspector ||
						   		eventComponents[i].hideFlags == HideFlags.HideAndDontSave)
								isShown = false;
							*/
							//Make the icon appear on the left
							GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
							//toggleStyle.contentOffset = new Vector2(-30,0);
							
							//Get access to the settings object/inspector
							SerializedObject componentObject = new SerializedObject(eventComponents[i]);
							_isSettingsShownProperty = componentObject.FindProperty("IsSettingsShown");
							
							//See if there are any worthwhile settings (i.e, anything after the "target" parameter)
							SerializedProperty targetProperty = componentObject.FindProperty("Target");
							if(targetProperty.NextVisible(true))
							{
								//Show the toggle
								_isSettingsShownProperty.boolValue = GUILayout.Toggle(_isSettingsShownProperty.boolValue, showSettings, toggleStyle, GUILayout.Width(30));
							}

							EditorGUILayout.EndHorizontal();

							//SHOW SETTINGS
							//--------------
							if(_isSettingsShownProperty.boolValue == false)
							{
								/*
								eventComponents[i].hideFlags = HideFlags.HideInInspector;
								//foreach(System.Reflection.FieldInfo field in eventComponents[i].GetType().GetFields())
								//	field.SetValue(toComponent, field.GetValue(fromComponent));
								*/
							}
							else
							{
								//eventComponents[i].hideFlags = HideFlags.None;

								SerializedProperty eventComponentProperty = componentObject.FindProperty("Target");
								while(eventComponentProperty.NextVisible(true) )
									EditorGUILayout.PropertyField(eventComponentProperty);
							}

						componentObject.ApplyModifiedProperties();
					}
				}
			}
			
		}
	}


}
