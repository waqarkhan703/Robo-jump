using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

[CustomEditor(typeof(ActionSequence))]
public class ActionSequenceInspector : Editor 
{
	SerializedProperty _selectedTargetTypeEnum;
	
	SerializedProperty _selectedComponentIndex;
	SerializedProperty _selectedMethodIndex;

	private SerializedObject m_Object;
	private SerializedProperty _parameterClassArray;
	private SerializedProperty _parameterTypesArray;
	private SerializedProperty _methodName;
	private SerializedProperty _targetType;
	private SerializedProperty _foldOut;
	
	private SerializedProperty _actionArray;

	
	private ActionSequence VisActScript;
	
	private const string kEventsPath = "Visual Actions/Events";
	private const string kCustomEventsPath = "Visual Actions/Events/Custom";
	
	private static GUILayoutOption buttonWidth = GUILayout.MaxWidth(20f);
	private static GUIContent
		insertAction = new GUIContent("+", "Add new Action"),
		deleteAction = new GUIContent("-", "Delete this Action"),
		moveUpAction = new GUIContent("\u2191", "Shift Action Up"),
		moveDownAction = new GUIContent("\u2193", "Shift action down"),
		showCode = new GUIContent("\u2630", "Show code");

	
	// Rebind these global variables per action
	// This will likely change to use local variables in the future
	private void BindSerializableProperties(SerializedProperty currentAction)
	{
		_selectedTargetTypeEnum = currentAction.FindPropertyRelative("SelectedTargetTypeEnum");
		//Get the Method and Components
		_selectedComponentIndex = currentAction.FindPropertyRelative ("SelectedComponentIndex");
		_selectedMethodIndex = currentAction.FindPropertyRelative ("SelectedMethodIndex");
		
		
		_parameterClassArray = currentAction.FindPropertyRelative("ParameterClassArray");
		_parameterTypesArray = currentAction.FindPropertyRelative("ParameterTypesArray");
		_methodName = currentAction.FindPropertyRelative("MethodName");
		
		_targetType = currentAction.FindPropertyRelative("TargetType");
	}
		
		
	public virtual void OnEnable()
	{
		m_Object = new SerializedObject(target);
		
		_actionArray = m_Object.FindProperty("Actions");
 
	}
	
	public override void OnInspectorGUI()
	{
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        m_Object.Update ();
		
		ShowInspectorGUI();
		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI
        m_Object.ApplyModifiedProperties();
	}
	
	public void ShowInspectorGUI()
	{

		VisActScript = (ActionSequence) target;
		SerializedProperty currentAction, foldOut;

		//-----------
		//	ACTIONS
		//-----------
		if(_actionArray.arraySize < 1)
		{
			//Have at-least one action by default
			_actionArray.InsertArrayElementAtIndex(0);

		}
		
		for(int i=0; i < _actionArray.arraySize; i++)
		{
						
			currentAction = _actionArray.GetArrayElementAtIndex(i);
			string methodName = currentAction.FindPropertyRelative("MethodName").stringValue;
			if(methodName.Equals("") )
			{
				methodName = "Choose Action";
			}
			
			EditorGUILayout.BeginHorizontal();
			foldOut = currentAction.FindPropertyRelative("FoldOut");
			foldOut.boolValue = EditorGUILayout.Foldout(foldOut.boolValue, new GUIContent(methodName) );
		//--------------
		//Move Action Up
		//--------------
			if(GUILayout.Button(moveUpAction, EditorStyles.miniButtonLeft, buttonWidth))
			{
				_actionArray.MoveArrayElement(i, i-1);
				
			}
		//----------------
		//Move Action Down
		//----------------
			if(GUILayout.Button(moveDownAction, EditorStyles.miniButtonMid, buttonWidth))
			{
				_actionArray.MoveArrayElement(i, i+1);
				
			}
		//-----------------
		//Insert New Action
		//-----------------
			if(GUILayout.Button(insertAction, EditorStyles.miniButtonMid, buttonWidth))
			{
				_actionArray.InsertArrayElementAtIndex(i);	
				break;
			}

			if(GUILayout.Button(deleteAction, EditorStyles.miniButtonMid, buttonWidth))
			{
				_actionArray.DeleteArrayElementAtIndex(i);
				break;
			}
		//-----------
		//	CODE-GEN
		//-----------
			if(GUILayout.Button(showCode, EditorStyles.miniButtonRight, buttonWidth))
			{

				EventClass[] eventList = VisActScript.gameObject.GetComponents<EventClass>();
				string allEventCode;
				if(eventList.Length <= 0)
				{

					EditorUtility.DisplayDialog("No events found!", "Please add some events under 'When to perform action(s)'" , "OK");

				}
				else if(_selectedTargetTypeEnum == null)
				{
					EditorUtility.DisplayDialog("No action chosen!", "Please choose an action by clicking the 'Choose action' drop-down" , "OK");
				}

				else
				{
					allEventCode = MonoScript.FromMonoBehaviour(eventList[0]).text;
					string methodSignature="\t";
					string paramDeclarations = "";
					Component aComponent;

				//METHOD CALL
				//-----------
				//What type of target have we chosen?
					switch(_selectedTargetTypeEnum.enumNames[_selectedTargetTypeEnum.enumValueIndex])
					{
					case "GameObject":
						methodSignature += "TargetGameObject.";
						aComponent = currentAction.FindPropertyRelative("TargetComponent").objectReferenceValue as Component;
						methodSignature += VAUtils.StripClassName(aComponent.GetType().ToString())+".";
						
						paramDeclarations += "\tGameObject TargetGameObject;\n"; 
						break;
						
					case "Self":
						aComponent = currentAction.FindPropertyRelative("TargetComponent").objectReferenceValue as Component;
						methodSignature += VAUtils.StripClassName(aComponent.GetType().ToString())+".";
						
						break;
					case "GameObjectClass":
						methodSignature += "GameObject.";
						break;
						
					case "CSharpClass":
					case "JavaScriptClass":
						methodSignature += Type.GetType(_targetType.stringValue).ToString() + ".";
						break;
						
					default:
						methodSignature += _selectedTargetTypeEnum.enumNames[_selectedTargetTypeEnum.enumValueIndex] + ".";
						
						break;
					}
					
					methodSignature += currentAction.FindPropertyRelative("MethodSignature").stringValue +";";

					allEventCode = allEventCode.Replace("Target.TriggerActionSequence();", methodSignature);
					allEventCode += "\n";

					//VARIABLE DECLARATIONS
					//---------------------
					SerializedProperty paramTypes = currentAction.FindPropertyRelative("ParameterTypes");
					SerializedProperty paramNames = currentAction.FindPropertyRelative("ParameterNames");

					for(int j=0; j< currentAction.FindPropertyRelative("ParameterTypes").arraySize; j++)
					{
						paramDeclarations += "\t";
						paramDeclarations += VAUtils.StripClassName(paramTypes.GetArrayElementAtIndex(j).stringValue) + " ";
						paramDeclarations += VAUtils.StripClassName(paramNames.GetArrayElementAtIndex(j).stringValue) + ";\n";
					}

					//Find where to insert param declarations
					int locParamDeclaration = allEventCode.IndexOf("{");
					allEventCode = allEventCode.Insert(locParamDeclaration+1, "\n" + paramDeclarations);

					//CLASS NAME
					//----------
					//Get rid of the "EventClass" inheritance
					allEventCode = Regex.Replace(allEventCode, ":.*\\bEventClass\\b", ": Monobehaviour");

					//Change Class Name
					//string nameWithoutSpaces = VisActScript.gameObject.name.Replace(" ", string.Empty) + "VAScript";
					string nameWithoutSpaces = VisActScript.gameObject.name.Replace(" ", string.Empty) + "_" + currentAction.FindPropertyRelative("MethodName").stringValue;
					allEventCode = Regex.Replace(allEventCode, "\\bpublic\\b\\s*\\bclass\\b\\s*\\w*", "public class " + nameWithoutSpaces);

					//Add warning
					allEventCode = "//NOTE: Code-generation only works for the first event at the moment.\n\n" + allEventCode; 

					//Show Code
					EditorUtility.DisplayDialog("Code", allEventCode, "OK");
				}

				break;
			}
			
		//--==================--
		// ACTIONS TO PERFORM
		//--==================---
			
			EditorGUILayout.EndHorizontal();
			
			if(foldOut.boolValue == true)
			{	
				BindSerializableProperties(currentAction);
				
				Type type = null;
				string targetClassName="";
				bool doesNeedReferenceToComponent = true;		//If target is a GameObject, we need its component instead of original GO
				//GUILayout.Label("Who performs action:", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField(_selectedTargetTypeEnum, new GUIContent("Who performs action")) ;
				SerializedProperty targetGameObject = currentAction.FindPropertyRelative("TargetGameObject");
				//SerializedProperty gameObject = currentAction.FindPropertyRelative("gameObject");
				SerializedProperty targetComponent = currentAction.FindPropertyRelative("TargetComponent");
				
		//------
		//TARGET
		//------
				//What type of target have we chosen?
				switch(_selectedTargetTypeEnum.enumNames[_selectedTargetTypeEnum.enumValueIndex])
				{
				case "GameObject":	
					//Make sure the "Target Object" slot is up to date
					targetGameObject = currentAction.FindPropertyRelative("TargetGameObject");
					//var aGameObject = (GameObject)EditorGUILayout.ObjectField("Target Object", VisActScript.TargetGameObject, typeof(GameObject), true); 
					EditorGUILayout.PropertyField(targetGameObject);
					doesNeedReferenceToComponent = true;
					//if(GUI.changed)
						//VisActScript.TargetGameObject = aGameObject;
					
					break;
				//If target = Self
				case "Self":
					targetGameObject.objectReferenceValue = VisActScript.gameObject;
					doesNeedReferenceToComponent = true;
					break;
					
				case "GameObjectClass":
					targetGameObject = null;
					doesNeedReferenceToComponent = false;
					
					targetClassName += "UnityEngine.GameObject, UnityEngine";
					break;
				
				case "Application":
					targetGameObject.objectReferenceValue = null;
					doesNeedReferenceToComponent = false;
					
					targetClassName +=  "UnityEngine.Application, UnityEngine";
					break;
					
				case "ObjectClass":
					targetGameObject = null;
					doesNeedReferenceToComponent = false;
					
					targetClassName += "UnityEngine.Object, UnityEngine";
					break;
					
				case "CSharpClass":
					targetGameObject = null;
					doesNeedReferenceToComponent = false;
					
					EditorGUILayout.PropertyField(_targetType, new GUIContent("Class Name"));
					targetClassName += _targetType.stringValue + ", Assembly-CSharp";
				break;
				
				case "JavaScriptClass":
					targetGameObject = null;
					doesNeedReferenceToComponent = false;
					
					EditorGUILayout.PropertyField(_targetType, new GUIContent("Class Name"));
					targetClassName += _targetType.stringValue + ", Assembly-UnityScript";
					break;
				
					
				case "UnityClass":
					targetGameObject = null;
					doesNeedReferenceToComponent = false;
					
					EditorGUILayout.PropertyField(_targetType, new GUIContent("Class Name"));
					//targetClassName += "UnityEngine." + _targetType.stringValue + ", UnityEngine";
					targetClassName += "UnityEngine." + _targetType.stringValue + ", UnityEngine";
				break;
					
				default:
					Debug.Log("Chose an unknown Target type. Please file a bug report.");	
					break;
				}
				
				//If "Target Object" has been selected, show all of its components, and functions
				if(doesNeedReferenceToComponent == true && targetGameObject.objectReferenceValue != null)
				{
						
		//----------
		//COMPONENTS
		//----------
					
					//Get all components attached to the target object
					Component[] targetComponents = (targetGameObject.objectReferenceValue as GameObject).GetComponents<Component>();
					Component selectedComponent = VAEditorUtils.ShowComponentsPopupGUI(targetComponents, _selectedComponentIndex);
					
					if(GUI.changed)
						_selectedMethodIndex.intValue = 0;
					
					//Choose "Component" as type
					type = selectedComponent.GetType();
					targetComponent.objectReferenceValue = selectedComponent;	
				}
				//Not a GameObject, so just find type based on targetClassName (as string)
				else
				{
					if(targetClassName.Length > 0)	//If a targetClassName has been provided
						type=Type.GetType(targetClassName);
				}
				
				
				//ERROR CHECK
				//Only proceed if we managed to get the type properly
				if(type != null)
				{
					//If its a GameObject or "Self" then it should have a TargetType.
					//Save TargetType so we can get all its methods in the VisActScript at runtime
					if(_selectedTargetTypeEnum.enumNames[_selectedTargetTypeEnum.enumValueIndex].Equals("Object") ||
					   _selectedTargetTypeEnum.enumNames[_selectedTargetTypeEnum.enumValueIndex].Equals("Self") )
					{
						_targetType.stringValue = type.AssemblyQualifiedName;
					}

					//GameObject requires "target" to be valid (not null). so,
					//if is GameObject AND has valid target (target!=null), 
					//OR
					// if "not" GameObject at all, but still has parameters 
					// then show the parameters,
					//if( targetGameObject != null || _selectedTargetTypeEnum.enumValueIndex > 0)
					if(targetGameObject != null || _selectedTargetTypeEnum.enumNames[_selectedTargetTypeEnum.enumValueIndex] != "GameObject")
					{
		//----------
		//PARAMETERS
		//----------	
						ShowMethodsAndParamsGUI(type, VisActScript.Actions[i]);
						
					}
				}
			}
		
		}
		
		//if (GUI.changed)
        //   EditorUtility.SetDirty (target);
		
	}
	
	
	ParameterInfo[] ShowMethodsAndParamsGUI(Type type, ActionClass anAction)
	{	
	
		//------------
		//METHODS GUI
		//-----------
		//Get all methods
		MethodInfo []methods =  type.GetMethods();
		
		VAUtils.parameterClass []oldInternalParameters = anAction.ParameterClassArray;
		//-------------
		// SYNC: In case methods are added/removed so index is wrong
		//-------------
		MethodInfo internalMethod = null;
		
		//Does an internal method exist?
		if(oldInternalParameters != null)
		{	
			List<string> paramTypeArray = new List<string>() ;
			for(int i=0;i<_parameterTypesArray.arraySize;i++)
			{
				paramTypeArray.Add(_parameterTypesArray.GetArrayElementAtIndex(i).stringValue);
			}
			
			internalMethod = VAUtils.FindOverloadedMethod(_methodName.stringValue, paramTypeArray.ToArray(), type);	

		}

		
		//Have we found an internalMethod? Maybe there is no previous method. 
		//Maybe Its the first time we created the method...
		if(internalMethod != null)
		{
			if(_selectedMethodIndex.intValue >= methods.Length ||	//Quick check: If the selectedmethod is outside the array, methods are not equal obviously
					VAUtils.AreMethodsEqual(internalMethod, methods[_selectedMethodIndex.intValue] ) == false)	//More explicit check
			{
				//Make sure the method is in sync with its index
				_selectedMethodIndex.intValue = VAUtils.GetSelectedMethodFromArray(methods, internalMethod);
				
				if(_selectedMethodIndex.intValue < 0)
				{
					Debug.LogWarning("VA: Action not found. Reseting to first Action available in object");
					_selectedMethodIndex.intValue = 0;
				}
			}
		}
		else
		{
			_selectedMethodIndex.intValue = 0;
		}
		//------------

		
		//Preserve old method
		MethodInfo oldMethod = methods[_selectedMethodIndex.intValue];
		
		//Show GUI for methods
		MethodInfo newMethod = VAEditorUtils.ShowMethodsGUI(methods, _selectedMethodIndex);
		
		//Save method name back to target
		_methodName.stringValue = newMethod.Name;

		//Save method signature in target
		anAction.MethodSignature = VAUtils.ComposeMethodSignature(newMethod, false);

		//Save method params
		ParameterInfo []newParams = newMethod.GetParameters();
		anAction.ParameterTypes = new string[newParams.Length];
		anAction.ParameterNames = new string[newParams.Length];
		for(int i=0;i<newParams.Length; i++)
		{
			anAction.ParameterNames[i] = newParams[i].Name;
			anAction.ParameterTypes[i] = newParams[i].ParameterType.ToString();
		}

		//Save parameterTypes in array as well
		ParameterInfo []parameterArray = newMethod.GetParameters();
		_parameterTypesArray.arraySize = parameterArray.Length;

		for(int i=0;i< parameterArray.Length;i++)
		{
			_parameterTypesArray.GetArrayElementAtIndex(i).stringValue = parameterArray[i].ParameterType.AssemblyQualifiedName;
		}

		
		//---------------
		//GUI: PARAMETERS
		//---------------
		//If method has changed, then repopulate the array
		bool shouldRepopulate = ! (VAUtils.AreMethodsEqual(oldMethod, newMethod) ); 
		
		//Show GUI for parameters
		bool hasParameters = VAEditorUtils.ShowParametersFromMethodGUI(_parameterClassArray, methods[_selectedMethodIndex.intValue], shouldRepopulate);

		//Copy parameter data from Inspector GUI to actualy model/class
		if(Application.isPlaying)
			anAction.UpdateParameters();

		//----------------
		//GUI: RETURN TYPE
		//----------------
		EditorGUILayout.LabelField("Return: ", methods[_selectedMethodIndex.intValue].ReturnType.ToString() );

		//return parameters;
		if(hasParameters)
			return methods[_selectedMethodIndex.intValue].GetParameters();
		else
			return null;
	}	

}