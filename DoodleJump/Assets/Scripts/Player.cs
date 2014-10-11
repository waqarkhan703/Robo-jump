using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool IsGrounded= false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
