using UnityEngine;
using System.Collections;

public class OnBecameInvisibleEvent : EventClass
{
	// OnBecameInvisible is called when the
	// renderer is no longer visible by any
	// camera.
	void OnBecameInvisible ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
