#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class VisualActions : ActionSequence 
{
	/*
	//----------------------------------------------------------------------------------
	// Begin : Reads data from accelerometer and can return normalized offset for X,Y axis from zero-orientation
	//----------------------------------------------------------------------------------
	public Vector3 dir_out;
	public Vector3 startDir;
	// Tweak those parameter to adjust input filtering
	public float AccelerometerUpdateInterval = 1.0f / 60.0f;
	public float LowPassFilterFactor = 1; 
	public float LowPassKernelWidthInSeconds = 1f;
	public Vector3 lowPassValue = Vector3.zero;
	//----------------------------------------------------------------------------------
	// End : Reads data from accelerometer and can return normalized offset for X,Y axis from zero-orientation
	//----------------------------------------------------------------------------------
	
	public bool OnMouseClick=true;		//Equivalent to a left-mouse click
	public bool OnMouseHover = false;
	public bool OnMouseWheelMove = false;
	public bool OnAccelerometerChange = false;
	public bool AtStart = false;
	public bool OnTap = true;
	public bool OnCollision = false;
	public bool OnCollisionWithCharacter = false;
	public bool OnTrigger = false;
	public bool OnKeyPress = false;
	public bool OnUpdate = false;
	public bool OnFixedUpdate = false;
	public string PressedKey;
	public int PressedButton;
	public bool IsFrameRateIndependent = false;*/
	#if UNITY_EDITOR
	public bool IsEventFoldoutOpen = true;
	#endif
	/*
	//----------------------------------------------------------------------------------
	//	Begin for accelerometer
	//----------------------------------------------------------------------------------
	// Reinit zero-orientation
 	public void  Recalibrate ()
	{
   		startDir.y = Input.acceleration.y;
    	startDir.x = Input.acceleration.x;
 	}
	
	//----------------------------------------------------------------------------------
	// Filter accelerometer input to make it less  jerky and noisy
	public Vector3 LowPassFilterAccelerometer ()
	{
		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}



	protected override void Start()
	{
		base.Start();


		//START
		//-----
		if(AtStart == true)
		{
			PlayAllActions();
		}
		
		//Accelerometer
		//-------------
		if(OnAccelerometerChange == true)
		{
			LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; 
			lowPassValue = Input.acceleration;
			Recalibrate();
		}
	}

	
		//Per-Frame
	//---------
	void Update()
	{
		//UPDATE
		//------
		if(OnUpdate == true)
		{
			PlayAllActions();
		}
		
		
		//TOUCH
		//-----
		if(OnTap == true)
		{
			foreach (Touch touch in Input.touches) 
			{
				if (touch.phase == TouchPhase.Began) 
				{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit ;
				if (Physics.Raycast (ray, out hit)) 
					{
						PlayAllActions();
					}
				}
			}
		}
		
		//ACCELEROMETER
		//-------------
		if(OnAccelerometerChange == true)
		{
			dir_out = Vector3.zero;
 			Vector3 filteredAcceleration = LowPassFilterAccelerometer();
    		dir_out.y =  filteredAcceleration.y - startDir.y;
			dir_out.x =  filteredAcceleration.x - startDir.x;
    		if (dir_out.sqrMagnitude > 1) dir_out.Normalize();
			if (dir_out.x != 0 || dir_out.y !=0)
			{
				PlayAllActions();
			}
		}
		
		//KEYBOARD
		//--------
		if(OnKeyPress == true)
		{
			 if (Input.GetKey(PressedKey))
			{
				PlayAllActions();
			}
		}
		
	}
	
	
	//Fixed Update for Physics-related code
	//---
	void FixedUpdate()
	{
		if(OnFixedUpdate == true)
		{
			PlayAllActions();
		}
	}
	
	
	//Hover
	//-----
	virtual protected void OnMouseOver()
	{
		if(OnMouseHover == true)
		{
			PlayAllActions();
		}
		else
		//Click
		//-----
		if (OnMouseClick == true)
		{
			if (Input.GetMouseButtonUp(PressedButton))
			{
				PlayAllActions();	
			}
		}
		// Mousewheel
		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if(OnMouseWheelMove == true)
			{
			PlayAllActions();
			}
		}
	}
	
	//Collision
	//---------
	void OnCollisionEnter() 
	{
		if(OnCollision == true)
			PlayAllActions();	
	}
	
	//Collision with the player
	//--------------------------
	void  OnControllerColliderHit() 
	{
		if(OnCollisionWithCharacter == true)
			PlayAllActions();	
	}
	
	//Collision with a trigger
	//------------------------
	void OnTriggerEnter(Collider other) 
	{
		if(OnTrigger == true)
			PlayAllActions();
	}
*/


	void OnDestroy()
	{
		EventClass[] eventComponents = gameObject.GetComponents<EventClass>();
		
		if(eventComponents != null)
		{
			//Show all the events that are linked to this particual VisualActions script
			for(int i=0; i<eventComponents.Length; i++)
			{
				if(eventComponents[i].Target == this)
				{
					var currentEvent = eventComponents[i]; // save in a different buffer to save state for anon func
					#if UNITY_EDITOR
					EditorApplication.delayCall += ()=>
					{
						if(currentEvent) GameObject.DestroyImmediate(currentEvent);
					};
					#else
						if(currentEvent) GameObject.Destroy(currentEvent);
					#endif
				}
			}
		}
	}

}
