using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class JumpThrough : MonoBehaviour
{
    
    public GameObject JumpPad;
    public GameObject Player;
    public GameObject Borders;

    public float _xWidth;
    public float _yheight;

    private float _highestPoint;
    private Vector3 _player_start_pos;
    public List<GameObject> PlatformArray;
    private float _seedValue = 264.4f;
    

	// Use this for initialization
	void Start ()
	{
	    Player = GameObject.FindGameObjectWithTag("Player");
	    PlatformArray = new List<GameObject>();
	    _player_start_pos = Player.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Player.transform.position.y > _highestPoint)
	    {
	        _highestPoint = Player.transform.position.y;
	        var new_jumpad = Instantiate(JumpPad, new Vector3(_xWidth*Random.Range(-1.0f,1.0f), _yheight+_highestPoint), Quaternion.identity);
	    }
	}
}
