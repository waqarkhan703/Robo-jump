using UnityEngine;
using System.Collections;

public class OnBecameVisibleEvent : EventClass
{
	// OnBecameVisible is called when the renderer
	// became visible by any camera.
	void OnBecameVisible ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
