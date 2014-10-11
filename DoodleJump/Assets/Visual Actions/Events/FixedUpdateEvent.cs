using UnityEngine;
using System.Collections;

public class FixedUpdateEvent : EventClass
{
	// This function is called every fixed
	// framerate frame, if the MonoBehaviour
	// is enabled.
	void FixedUpdate ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
