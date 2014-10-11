using UnityEngine;
using System.Collections;

public class OnServerInitializedEvent : EventClass
{
	// Called on the server whenever a Network.InitializeServer
	// was invoked and has completed.
	void OnServerInitialized ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
