using System;
using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour {
    Vector2 thrust = Vector2.one;
    float maxJitterX = 0;
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

    public void SetThrust(Vector2 thrust, float maxJitterX) {
        this.thrust = thrust;
        this.maxJitterX = maxJitterX;
    }

    void launch() {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Vector2 thrustPerChild = thrust / rigidbodies.Length;
        foreach (Rigidbody rb in rigidbodies)
        {
            float jitterX = UnityEngine.Random.Range(-maxJitterX, maxJitterX);
            if (debugEnabled) Debug.LogFormat("Launched {0} with {1} thurst and {2} x jitter", rb.name, thrustPerChild, jitterX);
            Vector3 thrustVector = new Vector3(jitterX, thrustPerChild.y, thrustPerChild.x);
            rb.AddForce(thrustVector, ForceMode.Impulse);
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
