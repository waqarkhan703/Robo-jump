using UnityEngine;
using System.Collections;

public class OnEnableEvent : EventClass
{
	// This function is called when the object
	// becomes enabled and active.
	void OnEnable ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
