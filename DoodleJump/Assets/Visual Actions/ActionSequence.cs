using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;


[AddComponentMenu("Visual Actions/StandAlone Action")]
public class ActionSequence : MonoBehaviour 
{
	public List<ActionClass> Actions;

	virtual protected void Awake()
	{	
		if(Actions != null)
		foreach(ActionClass action in Actions)
		{
			if(action != null)
				action.Init();
		}
	}
	

	/// <summary>
	/// Public function that plays the first Action in the Action Sequence 
	/// </summary>
	public void TriggerAction()
	{
		
		TriggerFirstActionInSequence();
	}
	
	/// <summary>
	/// Public function that plays the Action specified by the index, in the Action Sequence 
	/// </summary>
	public void TriggerAction(int index)
	{
		
		if(Actions[index].PerformAction())
			{
				//Successful
			}
			else
			{
				Debug.Log("VA: Attempted to trigger action (" + Actions[index].MethodName+ ") with index: " + index.ToString()+ " in Sequence, from external event, but failed");
			}
	}
	
	/// <summary>
	/// Public function that allows the entire sequence to triggered externally.
	/// </summary>
	public bool TriggerActionSequence()
	{
		if(PlayAllActions())
			return true;
		else
		{
			Debug.Log("VA: Attempted to trigger ActionSequence from external event, but failed");
			return false;
		}
	}
	
	/// <summary>
	/// Public function that plays the first Action in the Action Sequence 
	/// </summary>
	public void TriggerFirstActionInSequence()
	{
		
		if(Actions[0].PerformAction() )
			{
				//Successful
			}
		else
			{
				Debug.Log("VA: Attempted to trigger first Action in Sequence from external event, but failed");
			}
	}
	
	/// <summary>
	/// Public function that plays the last Action in the Action Sequence 
	/// </summary>
	public void TriggerLastActionInSequence()
	{
		
		if(Actions[Actions.Count-1].PerformAction() )
			{
				//Successful
			}
		else
			{
				Debug.Log("VA: Attempted to trigger last Action in Sequence from external event, but failed");
			}
	}
	
	
	protected bool PlayAllActions()
	{
		bool didPerformAllActions = true;
		foreach(ActionClass action in Actions)
		{
			if(action.PerformAction() )
			{
				//Successful
			}
			else
			{
				Debug.Log("VA: Attempted to play Action (" + action.MethodName + ") but failed");
				didPerformAllActions = false;
			}
		}
		
		return didPerformAllActions;
	}
	
}
