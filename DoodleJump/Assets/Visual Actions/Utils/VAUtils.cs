using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;



[Serializable]
public class VAUtils
{
	public enum VARIABLE_TYPES { INT, FLOAT, STRING, BOOL, VEC2, VEC3, QUATERNION, VEC4, COLOR, RECT, PRIMITIVES, CURVE, ANIMCLIP, AUDIOCLIP, SPACE, OBJECT, COMPONENT, GAMEOBJECT};
	
	public enum TARGET_TYPES {Self, GameObject, Application, GameObjectClass, ObjectClass, UnityClass, CSharpClass, JavaScriptClass};
	
	
	[System.Serializable]
	/// <summary>
	/// A class containing all the possible parameters that Visual Actions supports
	/// </summary>
	public class parameterClass
	{	
		[SerializeField]
		public int intParam = 0;
		[SerializeField]
		public float floatParam = 0.0f;
		[SerializeField]
		public String stringParam = "Default";
		[SerializeField]
		public bool boolParam = true;
		[SerializeField]
		public Vector3 vector2Param;
		[SerializeField]
		public Vector3 vector3Param;
		[SerializeField]
		public Quaternion quaternionParam;
		[SerializeField]
		public Vector4 vector4Param;
		[SerializeField]
		public Color colorParam = Color.white;
		[SerializeField]
		public Rect rectParam;
		[SerializeField]
		public PrimitiveType primitivesParam;
		[SerializeField]
		public AnimationCurve curveParam;
		[SerializeField]
		public AnimationClip animClipParam;
		[SerializeField]
		public AudioClip audioClipParam;
		[SerializeField]
		public Space spaceParam;
		[SerializeField]
		public UnityEngine.Object objectParam;
		[SerializeField]
		public UnityEngine.Component componentParam;
		[SerializeField]
		public UnityEngine.GameObject gameobjectParam;
		
		[SerializeField]
		public VARIABLE_TYPES selectedVariable = VARIABLE_TYPES.INT;
		
	}
	
	/// <summary>
	/// Composes the method signature based on its parameters, and returns its as a string.
	/// Note: The params names are used in the signature rather than types. 
	/// Since param names can be the same for two overloaded functions, the signature can 
	/// still technically "conflict". This will likely be changed soon. For now, parameters
	/// shouldbe named differently for overloaded functions based on type
	/// </summary>
	/// <returns>
	/// The method signature.
	/// </returns>
	/// <param name='aMethod'>
	/// A method.
	/// </param>
	public static string ComposeMethodSignature(MethodInfo aMethod)
	{
		/*
		ParameterInfo []allParams = aMethod.GetParameters();
				
		//Show formatted: function_name(par1,param2) etc
		String paramString;
		if(allParams.Length > 0 )
		{
			paramString= "(";
			for(int i=0; i< allParams.Length; i++)
			{
				paramString += allParams[i].ParameterType +" ";
				paramString += allParams[i].Name;
				if(i < allParams.Length-1)
				{
					paramString += ", ";
				}
			}
			paramString += ")";
			return aMethod.Name.ToString() + " " + paramString ;
		}
		else
		{
			//If no params, just show: function_name
			return aMethod.Name.ToString() ;
		}*/

		return ComposeMethodSignature(aMethod, true);
	}

	/// <summary>
	/// Composes the method signature based on its parameters, and returns its as a string.
	/// Note: The params names are used in the signature rather than types. 
	/// Since param names can be the same for two overloaded functions, the signature can 
	/// still technically "conflict". This will likely be changed soon. For now, parameters
	/// shouldbe named differently for overloaded functions based on type
	/// </summary>
	/// <returns>
	/// The method signature.
	/// </returns>
	/// <param name='aMethod'>
	/// A method.
	/// </param>
	public static string ComposeMethodSignature(MethodInfo aMethod, bool showParamType)
	{
		ParameterInfo []allParams = aMethod.GetParameters();
		
		//Show formatted: function_name(par1,param2) etc
		String paramString;
		if(allParams.Length > 0 )
		{
			paramString= "(";
			for(int i=0; i< allParams.Length; i++)
			{
				//Show the parameter type, if option is selected
				if(showParamType == true)
				{
					string fullString = allParams[i].ParameterType.ToString();
					string paramWithoutClassName = StripClassName(fullString);
					paramString += paramWithoutClassName + " ";
				}

				paramString += allParams[i].Name;
				if(i < allParams.Length-1)
				{
					paramString += ", ";
				}
			}
			paramString += ")";
			return aMethod.Name.ToString() + " " + paramString ;
		}
		else
		{
			//If no params, just show: function_name
			return aMethod.Name.ToString()+ "( )" ;
		}
	}
	
	/// <summary>
	/// Finds the right function from overloaded ones, based on parameter types
	/// </summary>
	/// <returns>
	/// The overloaded method.
	/// </returns>
	/// <param name='methodName'>
	/// Method name.
	/// </param>
	/// <param name='parameters'>
	/// Parameters.
	/// </param>
	/// <param name='targetType'>
	/// Target type.
	/// </param>
	public static MethodInfo FindOverloadedMethod(string methodName, object[] parameters, Type targetType)
	{
		//Store all the parameter types
		Type []_typedParams = new Type[parameters.Length];
		
		for(int i=0;i<parameters.Length;i++)
		{
			_typedParams[i] = parameters[i].GetType();
		}
		
		//TYPE is required to get the appropriate method via GetMethod
		return targetType.GetMethod(methodName, _typedParams);
	}
	
	/// <summary>
	/// Finds the right function from overloaded ones, based on parameter types
	/// </summary>
	/// <returns>
	/// The overloaded method.
	/// </returns>
	/// <param name='methodName'>
	/// Method name.
	/// </param>
	/// <param name='parameters'>
	/// Parameters.
	/// </param>
	/// <param name='targetType'>
	/// Target type.
	/// </param>
	public static MethodInfo FindOverloadedMethod(string methodName, string[] paramTypesAsStrings, Type targetType)
	{
		
		//Store all the parameter types
		Type []_typedParams = new Type[paramTypesAsStrings.Length];
		
		for(int i=0;i<paramTypesAsStrings.Length;i++)
		{
			_typedParams[i] = Type.GetType(paramTypesAsStrings[i]);
			//If couldn't get the type, assume its a UnityEngine object
			//if(_typedParams[i] == null)
				//_typedParams[i] = typeof(UnityEngine.Object);
			
		}
		
		MethodInfo returnMethod = targetType.GetMethod(methodName, _typedParams);
		//TYPE is required to get the appropriate method via GetMethod
		return returnMethod;
	}
	
	/// <summary>
	/// Comapres two methods, and returns true if they are equal.
	/// Source: http://ayende.com/blog/2658/method-equality 
	/// </summary>
	/// <returns>
	/// The method equals.
	/// </returns>
	/// <param name='left'>
	/// If set to <c>true</c> left.
	/// </param>
	/// <param name='right'>
	/// If set to <c>true</c> right.
	/// </param>
	public static bool AreMethodsEqual(MethodInfo left, MethodInfo right)
	{
		if (left.Equals(right))
			return true;
		// GetHashCode calls to RuntimeMethodHandle.StripMethodInstantiation()
		// which is needed to fix issues with method equality from generic types.
		if (left.GetHashCode() != right.GetHashCode())
			return false;
		if (left.DeclaringType != right.DeclaringType)
			return false;
		ParameterInfo[] leftParams = left.GetParameters();
		ParameterInfo[] rightParams = right.GetParameters();
		if (leftParams.Length != rightParams.Length)
			return false;
		for (int i = 0; i < leftParams.Length; i++)
		{
			if (leftParams[i].ParameterType != rightParams[i].ParameterType)
				return false;
		}
		if (left.ReturnType != right.ReturnType)
			return false;
		return true;
	}
	
	/// <summary>
	/// Gets the selected method from array. Returns its index from the array provided.
	/// </summary>
	/// <returns>
	/// The selected method from array.
	/// </returns>
	/// <param name='methods'>
	/// Methods.
	/// </param>
	/// <param name='incomingMethod'>
	/// Incoming method.
	/// </param>
	public static int GetSelectedMethodFromArray(MethodInfo []methods, MethodInfo incomingMethod)
	{
		//Get Index of incomingMethod from method list
		for (int i=0; i <  methods.Length; i++)
		{
			if(VAUtils.AreMethodsEqual(methods[i], incomingMethod) )
				return i;
		}
		
		//If does not exist, then return -1
		return -1;
	}
	
	
	
	
	public static object[] ParamClassArrayToObjectArray(parameterClass []parameterClassArray)
	{
		int paramLength = parameterClassArray.Length;
		
		//Initialize functionality of button upon start
		object []parameters = new object[paramLength];

		//Assign relevent parameters to button
		for( int i=0; i< paramLength; i++)
		{
			parameters[i] = new object();
			
			switch(parameterClassArray[i].selectedVariable)
			{
				case VAUtils.VARIABLE_TYPES.INT:
				parameters[i] = parameterClassArray[i].intParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.FLOAT:
				parameters[i] = parameterClassArray[i].floatParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.STRING:
				parameters[i] = parameterClassArray[i].stringParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.BOOL:
				parameters[i] = parameterClassArray[i].boolParam;
				break;
								
				case VAUtils.VARIABLE_TYPES.VEC2:
				parameters[i] = parameterClassArray[i].vector2Param;
				break;
				
				case VAUtils.VARIABLE_TYPES.VEC3:
				parameters[i] = parameterClassArray[i].vector3Param;
				break;
				
				case VAUtils.VARIABLE_TYPES.QUATERNION:
				parameters[i] = parameterClassArray[i].quaternionParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.VEC4:
				parameters[i] = parameterClassArray[i].vector4Param;
				break;
				
				case VAUtils.VARIABLE_TYPES.COLOR:
				parameters[i] = parameterClassArray[i].colorParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.RECT:
				parameters[i] = parameterClassArray[i].rectParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.PRIMITIVES:
				parameters[i] = parameterClassArray[i].primitivesParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.CURVE:
				parameters[i] = parameterClassArray[i].curveParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.SPACE:
				parameters[i] = parameterClassArray[i].spaceParam;
				break;
				
				case VAUtils.VARIABLE_TYPES.OBJECT:
				parameters[i] = parameterClassArray[i].objectParam;
				break;

				case VAUtils.VARIABLE_TYPES.COMPONENT:
				parameters[i] = parameterClassArray[i].componentParam;
				break;

				case VAUtils.VARIABLE_TYPES.GAMEOBJECT:
				parameters[i] = parameterClassArray[i].gameobjectParam;
				break;
			} //End switch
			
		}
		
		return parameters;
	}

	public static string StripClassName(string fullString)
	{
		int index = fullString.IndexOf('.');
		string paramWithoutClassName = fullString.Substring(index+1,fullString.Length-(index+1));
		return paramWithoutClassName;
	}
}
