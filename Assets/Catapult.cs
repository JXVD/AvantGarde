using System;
using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour {
    Vector2 thrust = Vector2.one;
    bool launched = false;
    [SerializeField]
    bool debugEnabled = false;
    Action onLaunch;

    public void StartCountdown(float countdownTime) {
        StartCoroutine(countdownToLaunch(countdownTime));
    }

    IEnumerator countdownToLaunch(float countdownTime) {
        yield return new WaitForSeconds(countdownTime);
        launch();
    }

    public void SetThrust(Vector2 thrust) {
        this.thrust = thrust;
    }

    void launch() {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Vector2 thrustPerChild = thrust / rigidbodies.Length;
        foreach (Rigidbody rb in rigidbodies)
        {
            if (debugEnabled) Debug.LogFormat("Launched {0} with {1} thurst", rb.name, thrustPerChild);
            rb.AddForce(Vector3.up * thrustPerChild.y, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * thrustPerChild.x, ForceMode.Impulse);
        }
        if (onLaunch != null)
        {
            onLaunch();
        }
        launched = true;
    }

    public void EnableDebug() {
        debugEnabled = true;
    }

    public void OnLaunch(Action onLaunch) {
        this.onLaunch += onLaunch;
    }
}
