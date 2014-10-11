using UnityEngine;
using System.Collections;

public class OnLevelWasLoadedEvent : EventClass
{
	// This function is called after a new
	// level was loaded.
	void OnLevelWasLoaded (int level)
	{
		Target.TriggerActionSequence();
		
	}
	
}