using UnityEngine;
using System.Collections;

public class OnMasterServerEventEvent : EventClass
{
	// Called on clients or servers when reporting
	// events from the MasterServer.
	void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		Target.TriggerActionSequence();
		
	}
	
}
