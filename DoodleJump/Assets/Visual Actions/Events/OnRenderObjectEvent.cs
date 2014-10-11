using UnityEngine;
using System.Collections;

public class OnRenderObjectEvent : EventClass
{
	// OnRenderObject is called after camera
	// has rendered the scene.
	void OnRenderObject ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
