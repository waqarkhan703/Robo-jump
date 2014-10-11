using UnityEngine;
using System.Collections;

public class AwakeEvent : EventClass
{
	// Awake is called when the script instance
	// is being loaded.
	void Awake ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
