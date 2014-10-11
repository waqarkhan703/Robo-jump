using UnityEngine;
using System.Collections;

public class OnCollisionExitEvent : EventClass
{
	// OnCollisionExit is called when this
	// collider/rigidbody has stopped touching
	// another rigidbody/collider.
	void OnCollisionExit (Collision collisionInfo)
	{
		Target.TriggerActionSequence();
		
	}
	
}
