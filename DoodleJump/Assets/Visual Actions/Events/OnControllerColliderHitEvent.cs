using UnityEngine;
using System.Collections;

public class OnControllerColliderHitEvent : EventClass
{
	// OnControllerColliderHit is called when
	// the controller hits a collider while
	// performing a Move.
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		Target.TriggerActionSequence();
		
	}
	
}
