using UnityEngine;
using System.Collections;

public class OnDisconnectedFromServerEvent : EventClass
{
	// Called on the client when the connection
	// was lost or you disconnected from the
	// server.
	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		Target.TriggerActionSequence();
		
	}
	
}
