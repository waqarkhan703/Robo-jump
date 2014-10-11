using UnityEngine;
using System.Collections;

public class MyCustomScript : MonoBehaviour 
{
	public void SetText(string aString)
	{
		guiText.text = aString;
	}
	
	public void ChooseColor(Color aColor)
	{
		Debug.Log("Chosen Color");
	}
}
