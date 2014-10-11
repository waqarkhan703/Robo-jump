using UnityEngine;
using System.Collections;

public class OnJointBreakEvent : EventClass
{
	// Called when a joint attached to the
	// same game object broke.
	void OnJointBreak (float breakForce)
	{
		Target.TriggerActionSequence();
		
	}
	
}
