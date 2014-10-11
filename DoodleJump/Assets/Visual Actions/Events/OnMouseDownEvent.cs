using UnityEngine;
using System.Collections;

public class OnMouseDownEvent : EventClass
{
	// OnMouseDown is called when the user
	// has pressed the mouse button while over
	// the GUIElement or Collider.
	void OnMouseDown ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
