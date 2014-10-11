using UnityEngine;
using System.Collections;

public class OnPlayerConnectedEvent : EventClass
{
	// Called on the server whenever a new
	// player has successfully connected.
	void OnPlayerConnected (NetworkPlayer player)
	{
		Target.TriggerActionSequence();
		
	}
	
}
