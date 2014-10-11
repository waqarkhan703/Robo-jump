using UnityEngine;
using System.Collections;

public class OnRenderImageEvent : EventClass
{
	// OnRenderImage is called after all rendering
	// is complete to render image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		Target.TriggerActionSequence();
		
	}
	
}
