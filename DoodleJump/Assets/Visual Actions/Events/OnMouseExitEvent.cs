using UnityEngine;
using System.Collections;

public class OnMouseExitEvent : EventClass
{
	// OnMouseExit is called when the mouse
	// is not any longer over the GUIElement
	// or Collider.
	void OnMouseExit ()
	{
		Target.TriggerActionSequence();
		
	}
	
}
