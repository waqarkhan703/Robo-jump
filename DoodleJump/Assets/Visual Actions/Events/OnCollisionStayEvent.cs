using UnityEngine;
using System.Collections;

public class OnCollisionStayEvent : EventClass
{
	// OnCollisionStay is called once per frame
	// for every collider/rigidbody that is
	// touching rigidbody/collider.
	void OnCollisionStay (Collision collisionInfo)
	{
		Target.TriggerActionSequence();
		
	}
	
}
