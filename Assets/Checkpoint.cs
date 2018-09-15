using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameObject player;
    public PlayerController controller;

    public float radius;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(controller.checkpoint != transform && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            controller.SetCheckpoint(transform);
        }
	}
}
