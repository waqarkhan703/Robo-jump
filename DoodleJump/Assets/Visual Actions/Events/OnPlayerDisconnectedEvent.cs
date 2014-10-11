using UnityEngine;
using System.Collections;

public class OnPlayerDisconnectedEvent : EventClass
{
	// Called on the server whenever a player
	// disconnected from the server.
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Target.TriggerActionSequence();
		
	}
	
}
