using UnityEngine;
using System.Collections;

public class OnGUIEvent : EventClass
{
	// OnGUI is called for rendering and handling
	// GUI events.
	void OnGUI ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
