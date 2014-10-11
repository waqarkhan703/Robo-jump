using UnityEngine;
using System.Collections;

public class OnPostRenderEvent : EventClass
{
	// OnPostRender is called after a camera
	// finished rendering the scene.
	void OnPostRender ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
