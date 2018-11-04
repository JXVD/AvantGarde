using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TriggerOnStop : MonoBehaviour {
    Rigidbody physics;
    float velocityTolerance = 0.01f;
    Action onStop;
    bool hasStopped = false;
    [SerializeField]
    bool debugEnabled = false;
    bool isListening = true;

    void Awake() {
        physics = GetComponent<Rigidbody>();
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

    void Update() {
        if (!isListening) return;

        if (debugEnabled) Debug.Log(physics.velocity.magnitude);
        if (!hasStopped && 
            physics.velocity.magnitude <= velocityTolerance && 
            onStop != null) {
            onStop();
            hasStopped = true;
        }
    }
}
