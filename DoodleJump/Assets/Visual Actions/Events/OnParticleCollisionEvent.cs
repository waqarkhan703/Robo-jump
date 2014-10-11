using UnityEngine;
using System.Collections;

public class OnParticleCollisionEvent : EventClass
{
	// OnParticleCollision is called when a
	// particle hits a collider.
	void OnParticleCollision (GameObject other)
	{
		Target.TriggerActionSequence();
		
	}
	
}
