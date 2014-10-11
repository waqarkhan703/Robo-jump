using UnityEngine;
using System.Collections;

public class OnTriggerStayEvent : EventClass
{
	// OnTriggerStay is called once per frame
	// for every Collider other that is touching
	// the trigger.
	void OnTriggerStay (Collider other)
	{
		Target.TriggerActionSequence();
		
	}
	
}
