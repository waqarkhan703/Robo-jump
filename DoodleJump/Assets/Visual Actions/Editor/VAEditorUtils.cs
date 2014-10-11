using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

public class VAEditorUtils
{
	/// <summary>
	/// Shows all available components in a Pop-up list. Returns the selected Component
	/// </summary>
	/// <returns>
	/// The components popup GU.
	/// </returns>
	/// <param name='targetComponents'>
	/// Target components.
	/// </param>
	/// <param name='selectedComponentIndex'>
	/// Selected component index.
	/// </param>
	public static Component ShowComponentsPopupGUI(Component[] targetComponents, SerializedProperty selectedComponentIndex)
	{
		//Store component names in a list
		String []componentsNamesArray = new String[targetComponents.Length];
		
		//Show each component
		if(targetComponents.Length > 0)
		for(int i=0; i< targetComponents.Length; i++)
		{
			componentsNamesArray[i] = targetComponents[i].GetType().ToString();
		}
		
		//Allow user to select particular component from list
		if(selectedComponentIndex.intValue >= componentsNamesArray.Length)
			selectedComponentIndex.intValue = 0;
		
		selectedComponentIndex.intValue = EditorGUILayout.Popup("Component", selectedComponentIndex.intValue,componentsNamesArray);
		
		//Get reference to selected component
		return targetComponents[selectedComponentIndex.intValue];
	}
	
	
	/// <summary>
	/// Presents the inspector GUI that shows all available methods, and returns the selected method.
	/// </summary>
	/// <returns>
	/// The methods GU.
	/// </returns>
	/// <param name='methods'>
	/// Methods.
	/// </param>
	/// <param name='selectedMethodIndex'>
	/// Selected method index.
	/// </param>
	public static MethodInfo ShowMethodsGUI(MethodInfo []methods, SerializedProperty selectedMethodIndex)
	{	
		//Array that will contain names of all methods in selected component
		String [] methodsNamesArray = new String[methods.Length];
		
		//-----------------
		//METHOD SIGNATURES
		//-----------------
		if(methods.Length > 0)
		for(int i=0; i< methods.Length; i++)
			{
			methodsNamesArray[i] = VAUtils.ComposeMethodSignature(methods[i]);
			}
			
		
		//--------------------
		//GUI: LIST OF METHODS
		//--------------------
		if(selectedMethodIndex.intValue >= methodsNamesArray.Length)
			selectedMethodIndex.intValue=0;
		
		//Show the actions popup
		selectedMethodIndex.intValue = EditorGUILayout.Popup("Action", selectedMethodIndex.intValue, methodsNamesArray);
		
		//Return new method signature
		return methods[selectedMethodIndex.intValue];
		
	}
	
	public static bool ShowParametersFromMethodGUI(SerializedProperty parameterClassArray, MethodInfo aMethod, bool shouldRepopulate = false)
	{
		//Get parameter list
		ParameterInfo []parameterTypesArray =  aMethod.GetParameters();
		
		//If the given method has parameters, set/show them
		if(parameterTypesArray.Length > 0)
		{
			//If no parameters are defined, or a different method is selected, repopulate array (with appropriate types)
			if(	parameterClassArray.arraySize <= 0 ||
				shouldRepopulate == true)
			{
				//Reset array. Clear old values
				parameterClassArray.ClearArray();
			
				//Populate array with appropriate types
				for(int j=1; j<= parameterTypesArray.Length; j++)
				{
					parameterClassArray.InsertArrayElementAtIndex(j-1);
				}
			}
		
			//Show each parameter
			for(int j=1; j<= parameterTypesArray.Length; j++)
			{
				//Get a single element from the Array of type parameterClass
				SerializedProperty currentElement = parameterClassArray.GetArrayElementAtIndex(j-1);
				
				//Show Inspector GUI for this instance of parameterClass
				string name = parameterTypesArray[j-1].Name;

				VAEditorUtils.ShowParameterGUI(currentElement,  parameterTypesArray[j-1].ParameterType, name);
			
			}
		}
		else
		{
			//VisActScript.Parameters = null;
			parameterClassArray.ClearArray();
			return false;
		}
		
		
		return true;
	}
	
	public static void ShowParameterGUI(SerializedProperty parameterClassInstance, Type parameterType, string name)
	{
		
		//Present input field based on parameter type
		switch(parameterType.ToString())
		{
			case "System.Int16":
			case "System.Int32":
			case "System.Int64":
				
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("intParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.INT;
				break;
			
			case "System.Single":
			case "System.Double":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("floatParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.FLOAT;
				
				break;
				
			case "System.String":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("stringParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.STRING;
				
				break;
				
			case "System.Boolean":
				//EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("boolParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.BOOL;
				
				//EditorGUILayout.Toggle("<=>",false);
				//EditorGUILayout.EndHorizontal();

				break;
		
			case "UnityEngine.Vector2":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("vector2Param"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.VEC2;
				
				break;

			case "UnityEngine.Vector3":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("vector3Param"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.VEC3;
				
				break;
			
			case "UnityEngine.Quaternion":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("vector3Param"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("quaternionParam").quaternionValue = Quaternion.Euler(parameterClassInstance.FindPropertyRelative("vector3Param").vector3Value);
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.QUATERNION;
				
				break;
			
			case "UnityEngine.Vector4":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("vector4Param"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.VEC4;
			
				break;
			
			case "UnityEngine.Color":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("colorParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.COLOR;
				
				break;
			
			case "UnityEngine.Rect":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("rectParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.RECT;
				
				break;
		
			case "UnityEngine.PrimitiveType":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("primitivesParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.PRIMITIVES;
				
				break;
		
			case "UnityEngine.AnimationCurve":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("curveParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.CURVE;
				
				break;

			case "UnityEngine.AnimationClip":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("animClipParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.ANIMCLIP;
			
				break;

			case "UnityEngine.AudioClip":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("audioClipParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.AUDIOCLIP;
				
				break;
			
			case "UnityEngine.Space":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("spaceParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.SPACE;
				
				break;

			case "UnityEngine.Object":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("objectParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.OBJECT;
				
				break;
		
			case "UnityEngine.GameObject":
				EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("gameobjectParam"),  new GUIContent(name));
				parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.GAMEOBJECT;

				break;

			//DEFAULT: If its derived from UnityEngine.Component, then show standard "slot", otherwise not supported
			default:
				if(parameterType.IsSubclassOf( typeof(UnityEngine.Component) ) )
				{
					SerializedProperty aComponent = parameterClassInstance.FindPropertyRelative("componentParam");
					aComponent.objectReferenceValue = EditorGUILayout.ObjectField(name, aComponent.objectReferenceValue , parameterType, true );
					parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.COMPONENT;
					//Debug.Log (parameterType);
				}
			else if(parameterType.IsSubclassOf( typeof(UnityEngine.Object) ) )
				{
					EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("objectParam"),  new GUIContent(name));
					parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.OBJECT;
					//Debug.Log (parameterType);
				}/*
			else if(parameterType.ToString().Equals("UnityEngine.Object"))
				{
					EditorGUILayout.PropertyField(parameterClassInstance.FindPropertyRelative("objectParam"),  new GUIContent(name));
					parameterClassInstance.FindPropertyRelative("selectedVariable").enumValueIndex = (int) VAUtils.VARIABLE_TYPES.OBJECT;
				}*/
			else
				{
					EditorGUILayout.LabelField("Unsupported parameter: ", parameterType.ToString() );
				}	
				break;	//Not needed, but just in-case we add something else below this case, and forget the break!

		}//End Switch

	}//End ShowParameterGUI
	
}
