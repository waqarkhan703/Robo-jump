using UnityEngine;
using System.Collections;

public class OnConnectedToServerEvent : EventClass
{
	// Called on the client when you have successfully
	// connected to a server.
	void OnConnectedToServer ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
