using UnityEngine;
using System.Collections;

public class OnMouseOverEvent : EventClass
{
	// OnMouseOver is called every frame while
	// the mouse is over the GUIElement or
	// Collider.
	void OnMouseOver ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
