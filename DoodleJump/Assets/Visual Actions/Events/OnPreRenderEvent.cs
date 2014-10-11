using UnityEngine;
using System.Collections;

public class OnPreRenderEvent : EventClass
{
	// OnPreRender is called before a camera
	// starts rendering the scene.
	void OnPreRender ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
