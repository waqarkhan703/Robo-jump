using UnityEngine;
using System.Collections;

public class OnCollisionEnterEvent : EventClass
{
	// OnCollisionEnter is called when this
	// collider/rigidbody has begun touching
	// another rigidbody/collider.
	void OnCollisionEnter (Collision collision)
	{
		Target.TriggerActionSequence();
		
	}
	
}
