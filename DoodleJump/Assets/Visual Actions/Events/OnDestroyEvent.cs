using UnityEngine;
using System.Collections;

public class OnDestroyEvent : EventClass
{
	// This function is called when the MonoBehaviour
	// will be destroyed.
	void OnDestroy ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
