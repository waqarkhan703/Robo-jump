using UnityEngine;
using System.Collections;

public class OnTriggerExitEvent : EventClass
{
	// OnTriggerExit is called when the Collider
	// other has stopped touching the trigger.
	void OnTriggerExit (Collider other)
	{
		Target.TriggerActionSequence();
		
	}
	
}
