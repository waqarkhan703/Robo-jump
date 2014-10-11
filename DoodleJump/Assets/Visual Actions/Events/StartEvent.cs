using UnityEngine;
using System.Collections;

public class StartEvent : EventClass
{
	// Start is called just before any of the
	// Update methods is called the first time.
	void Start ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
