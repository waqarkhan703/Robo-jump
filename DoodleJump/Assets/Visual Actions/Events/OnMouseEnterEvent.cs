using UnityEngine;
using System.Collections;

public class OnMouseEnterEvent : EventClass
{
	// OnMouseEnter is called when the mouse
	// entered the GUIElement or Collider.
	void OnMouseEnter ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
