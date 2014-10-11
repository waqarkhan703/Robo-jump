using UnityEngine;
using System.Collections;

public class OnTouchEvent : EventClass
{
	public Collider TargetCollider;

	//This event is called as soon as the user touches the collider of the target 
	void Update () 
	{

		foreach (Touch touch in Input.touches) 
		{
			if (touch.phase == TouchPhase.Began) 
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit ;
				if (Physics.Raycast (ray, out hit)) 
				{
					if(hit.collider.Equals(TargetCollider))
						Target.TriggerActionSequence();
				}
			}
		}

	}
}
