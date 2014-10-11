using UnityEngine;
using System.Collections;

public class OnMouseUpEvent : EventClass
{
	// OnMouseUp is called when the user has
	// released the mouse button.
	void OnMouseUp ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
