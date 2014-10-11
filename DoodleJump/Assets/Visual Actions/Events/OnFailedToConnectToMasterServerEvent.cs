using UnityEngine;
using System.Collections;

public class OnFailedToConnectToMasterServerEvent : EventClass
{
	// Called on clients or servers when there
	// is a problem connecting to the MasterServer.
	void OnFailedToConnectToMasterServer (NetworkConnectionError info)
	{
		Target.TriggerActionSequence();
		
	}
	
}
