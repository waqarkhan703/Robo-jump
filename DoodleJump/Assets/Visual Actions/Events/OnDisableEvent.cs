using UnityEngine;
using System.Collections;

public class OnDisableEvent : EventClass
{
	// This function is called when the behaviour
	// becomes disabled () or inactive.
	void OnDisable ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
