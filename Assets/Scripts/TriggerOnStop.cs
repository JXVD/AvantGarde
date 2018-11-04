using System;
using UnityEngine;

public class TriggerOnStop : MonoBehaviour {
    Rigidbody[] rigidbodies;
    float velocityTolerance = 0.01f;
    Action onStop;
    bool hasStopped = false;
    [SerializeField]
    bool debugEnabled = false;
    bool isListening = true;

    void Awake() {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void ToggleListening(bool isListening) {
        this.isListening = isListening;
    }

    public void SetVelocityTolerance(float tolerance) {
        velocityTolerance = tolerance;
    }

    public void OnStop(Action action) {
        onStop += action;
    }

    void Update()
    {
        if (!isListening) return;
        float magnitude = pollMagnitude();
        if (debugEnabled) Debug.LogFormat("Total Magnitude is {0}", magnitude);
        if (!hasStopped &&
            magnitude <= velocityTolerance &&
            onStop != null)
        {
            triggerStop();
        }
#if UNITY_EDITOR
        if (debugEnabled && Input.GetKeyDown(KeyCode.Space)) {
            triggerStop();
        }
#endif
    }

    void triggerStop() {
        onStop();
        hasStopped = true;
    }

    float pollMagnitude() {
        float totalMagnitude = 0;
        foreach (Rigidbody rb in rigidbodies) {
            totalMagnitude += rb.velocity.magnitude;
        }
        return totalMagnitude;
    }
}
