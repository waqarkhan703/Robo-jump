using UnityEngine;
using System.Collections;

public class OnFailedToConnectEvent : EventClass
{
	// Called on the client when a connection
	// attempt fails for some reason.
	void OnFailedToConnect (NetworkConnectionError error)
	{
		Target.TriggerActionSequence();
		
	}
	
}
