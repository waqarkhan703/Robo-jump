using UnityEngine;
using System.Collections;

public class OnMouseDragEvent : EventClass
{
	// OnMouseDrag is called when the user
	// has clicked on a GUIElement or Collider
	// and is still holding down the mouse.
	void OnMouseDrag ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
