using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {
    Vector2 thrust = Vector2.one;
    public Rigidbody rb;
    bool launched = false;
    [SerializeField]
    bool debugEnabled = false;

    Action onLaunch;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    public void SetThrust(Vector2 thrust) {
        this.thrust = thrust;
    }

	// Update is called once per frame
	void FixedUpdate () {
        
        if(Input.GetMouseButtonDown(1) && !launched)
        {
            rb.AddForce(transform.up * thrust.y, ForceMode.VelocityChange);
            rb.AddForce(transform.forward * thrust.x, ForceMode.VelocityChange);
            if (debugEnabled) {
                Debug.LogFormat("Launched {0} with {1} thrust", name, thrust);
            }
            launched = true;
            if(onLaunch != null) {
                onLaunch();
            }
        }
	}

    public void EnableDebug() {
        debugEnabled = true;
    }

    public void OnLaunch(Action onLaunch) {
        this.onLaunch += onLaunch;
    }
}
