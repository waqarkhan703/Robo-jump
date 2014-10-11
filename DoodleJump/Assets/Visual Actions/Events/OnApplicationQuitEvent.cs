using UnityEngine;
using System.Collections;

public class OnApplicationQuitEvent : EventClass
{
	// Sent to all game objects before the
	// application is quit.
	void OnApplicationQuit ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
