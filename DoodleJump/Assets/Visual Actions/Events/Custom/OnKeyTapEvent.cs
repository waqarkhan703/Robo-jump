using UnityEngine;
using System.Collections;

public class OnKeyTapEvent : EventClass 
{
	//Key codes can be found on the "KeyCode" page in the scripting reference
	// Examples: "up", "left", "return", "backspace", "w", "a", "s", "d", etc.
	public string PressedKey = "space";
	
	// Update is called once per frame
	void Update () 
	{
		//If we have a key, then check when it is pressed
		if (PressedKey != "")
		{
			if (Input.GetKeyUp(PressedKey))
			{
				Target.TriggerActionSequence();
			}
		}
		//If a key is not defined, then trigger upon any key
		else if (Input.anyKey == true)
		{
			Target.TriggerActionSequence();
		}
	}
}
