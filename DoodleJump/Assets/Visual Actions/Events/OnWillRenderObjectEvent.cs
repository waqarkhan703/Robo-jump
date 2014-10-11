using UnityEngine;
using System.Collections;

public class OnWillRenderObjectEvent : EventClass
{
	// OnWillRenderObject is called once for
	// each camera if the object is visible.
	void OnWillRenderObject ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
