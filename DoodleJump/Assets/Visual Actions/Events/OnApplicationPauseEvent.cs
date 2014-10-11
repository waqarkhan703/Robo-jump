using UnityEngine;
using System.Collections;

public class OnApplicationPauseEvent : EventClass
{
	// Sent to all game objects when the player
	// pauses.
	void OnApplicationPause (bool pause)
	{
		Target.TriggerActionSequence();
		
	}
	
}
