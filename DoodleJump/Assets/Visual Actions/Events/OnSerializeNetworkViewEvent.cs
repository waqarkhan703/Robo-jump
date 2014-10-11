using UnityEngine;
using System.Collections;

public class OnSerializeNetworkViewEvent : EventClass
{
	// Used to customize synchronization of
	// variables in a script watched by a network
	// view.
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
	{
		Target.TriggerActionSequence();
		
	}
	
}
