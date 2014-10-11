using UnityEngine;
using System.Collections;

public class LateUpdateEvent : EventClass
{
	// LateUpdate is called every frame, if
	// the Behaviour is enabled.
	void LateUpdate ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
