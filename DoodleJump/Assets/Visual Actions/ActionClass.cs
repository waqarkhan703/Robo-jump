using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

[Serializable]
public class ActionClass
{
	public VAUtils.TARGET_TYPES SelectedTargetTypeEnum;
	
	
	/// <summary>
	/// Target: The gameobject which will recieve a function/command when VA performs an action
	/// </summary>
	public GameObject TargetGameObject = null;
	
	
	/// <summary>
	/// The index of the selected component on the target.
	/// </summary>
	public int SelectedComponentIndex=0;
	
	/// <summary>
	/// The index of the selected method in the selected component in the target object.
	/// </summary>
	public int SelectedMethodIndex=0;
	
	/// <summary>
	/// The target component.
	/// </summary>
	public Component TargetComponent;
	
	public string TargetType;
	
	/// <summary>
	/// The name of the method to invoke upon button action.
	/// </summary>
	public String MethodName;

	#if UNITY_EDITOR
	/// <summary>
	/// The signature of the method, i.e, the way it appears in the inspector
	/// </summary>
	public String MethodSignature;
	public String []ParameterTypes;
	public String []ParameterNames;
	#endif
	
	/// <summary>
	/// The parameters to be passed to the selected method upon button action.
	/// </summary>
	/// 
	public VAUtils.parameterClass []ParameterClassArray;
	
	public string []ParameterTypesArray;
	
	public bool FoldOut = true;
	
	private object _targetObject;
	private MethodInfo _function;
	private object []_parameterObjects;
	
	public bool Init()
	{	

		//Copy parameter data from Inspector GUI to actualy model/class
		//Debug.Log("Updating Params");
		UpdateParameters();
		//A method name can refer to multiple (overloaded) methods. Get a specific one based
		//on parameter types.
		Type targetType = Type.GetType(TargetType);
		if(targetType != null)
			_function = VAUtils.FindOverloadedMethod(MethodName, ParameterTypesArray,  targetType/*Type.GetType(TargetType)*/ );
		//else //return false;
			//Debug.LogWarning("VA: Target Object not set in " + gameObject.name);
		
		//TargetObject is required to "Invoke" a non-static function
			//If target is another GO or self, then method will be inside Component
		if((SelectedTargetTypeEnum == VAUtils.TARGET_TYPES.GameObject 
			|| SelectedTargetTypeEnum == VAUtils.TARGET_TYPES.Self) 
			&& TargetGameObject != null)
		{
			_targetObject = TargetComponent;
		}
		
		return true;
	}

	public bool UpdateParameters()
	{
		_parameterObjects = VAUtils.ParamClassArrayToObjectArray(ParameterClassArray);
		return true;
	}
	
	/// <summary>
	/// Base function that ultimately performs the action. All other events call this function in the end
	/// to perform the action
	/// </summary>
	/// <returns>
	/// The action.
	/// </returns>
	public bool PerformAction()
	{
		if(_function != null)
			{
				if(TargetGameObject != null) //Proceed if Target is not null
				{
					//If no parameters are provided, send null parameters
					if (_parameterObjects.Length == 0)
					{
						_function.Invoke(_targetObject , null );
					}
					else
					{
						_function.Invoke(_targetObject, _parameterObjects);
					}
					
					return true;	//All went well
				}
				else //Target is null 
				{
					Debug.Log ("VA: Target is null");
					return false;	
				}
			}
		else
		{
			return false;	//Function was null, so couldn't perform action
		}
		
	}


}
