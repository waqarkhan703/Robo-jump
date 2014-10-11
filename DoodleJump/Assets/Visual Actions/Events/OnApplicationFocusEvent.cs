using UnityEngine;
using System.Collections;

public class OnApplicationFocusEvent : EventClass
{
	// Sent to all game objects when the player
	// gets or looses focus.
	void OnApplicationFocus (bool focus)
	{
		Target.TriggerActionSequence();
		
	}
	
}
