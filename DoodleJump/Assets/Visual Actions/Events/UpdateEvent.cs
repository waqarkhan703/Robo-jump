using UnityEngine;
using System.Collections;

public class UpdateEvent : EventClass
{
	// Update is called every frame, if the
	// MonoBehaviour is enabled.
	void Update ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
