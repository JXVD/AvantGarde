using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {
    public float thrust;
    public Rigidbody rb;
    bool launched = false;
    [SerializeField]
    bool debugEnabled = false;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void FixedUpdate () {
        
        if(Input.GetMouseButtonDown(1) && !launched)
        {
            rb.AddForce(transform.up * thrust, ForceMode.VelocityChange);
            rb.AddForce(transform.forward * thrust, ForceMode.VelocityChange);
            if (debugEnabled) {
                Debug.LogFormat("Launched {0} with {1} thrust", name, thrust);
            }
            launched = true;
        }
	}

    public void EnableDebug() {
        debugEnabled = true;
    }
}
