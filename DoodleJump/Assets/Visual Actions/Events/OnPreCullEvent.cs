using UnityEngine;
using System.Collections;

public class OnPreCullEvent : EventClass
{
	// OnPreCull is called before a camera
	// culls the scene.
	void OnPreCull ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
