using UnityEngine;
using System.Collections;

public class OnMouseUpAsButtonEvent : EventClass
{
	// OnMouseUpAsButton is only called when
	// the mouse is released over the same
	// GUIElement or Collider as it was pressed.
	void OnMouseUpAsButton ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
