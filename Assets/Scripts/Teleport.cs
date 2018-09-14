using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform player;
    public float radius;

    public Transform start;
    public Transform end;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Vector3.Distance(start.position, player.position) < radius)
        {
            player.position = end.position + player.position - start.position;
        }
	}
}
