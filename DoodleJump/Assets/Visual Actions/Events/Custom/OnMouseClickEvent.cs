using UnityEngine;
using System.Collections;

public class OnMouseClickEvent : EventClass 
{
	// 0 = Left button, 1 = Right button, 2 = Middle button
	public int PressedButton = 0;
	
	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp(PressedButton))
		{
			Target.TriggerActionSequence();	
		}
		
	}
}
