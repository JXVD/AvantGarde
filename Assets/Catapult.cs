using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {
    public float thrust;
    public Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
}

	// Update is called once per frame
	void FixedUpdate () {
        
        if(Input.GetMouseButtonDown(1))
        {
            rb.AddForce(transform.up * thrust);
        }	
	}
}
