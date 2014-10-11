using UnityEngine;
using System.Collections;

public class OnNetworkInstantiateEvent : EventClass
{
	// Called on objects which have been network
	// instantiated with Network.Instantiate
	void OnNetworkInstantiate (NetworkMessageInfo info)
	{
		Target.TriggerActionSequence();
		
	}
	
}
