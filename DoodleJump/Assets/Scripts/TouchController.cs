using System;
using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{

    public GameObject Player;
    public float InitialVelocity = 13.4f;

    private Player _player;
    private Rigidbody _playerRigidbody;
    

    void Awake()
    {
        _player = Player.GetComponent<Player>();
        _playerRigidbody = Player.GetComponent<Rigidbody>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.touchCount > 0)
	    {
            if (_player.IsGrounded)
            {
                _playerRigidbody.velocity = new Vector3(0.0f, InitialVelocity, 0f);
            }
	    }
	}
}
