using UnityEngine;
using System.Collections;

public class OnTriggerEnterEvent : EventClass
{
	// OnTriggerEnter is called when the Collider
	// other enters the trigger.
	void OnTriggerEnter (Collider other)
	{
		Target.TriggerActionSequence();
		
	}
	
}
