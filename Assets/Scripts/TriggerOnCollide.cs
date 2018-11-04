using System;
using UnityEngine;

public class TriggerOnCollide : MonoBehaviour {
    [SerializeField]
    bool onlyTriggerOnce;
    [SerializeField]
    bool debugEnabled;

    bool triggered = false;

    Action<Collision> onCollide;

    public void SubscribeOnCollisionEnter(Action<Collision> onCollide) {
        this.onCollide += onCollide;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!triggered || !onlyTriggerOnce)
        {
            if (debugEnabled) Debug.LogFormat("Collided with {0}", collision.collider);
            if (onCollide != null)
            {
                onCollide(collision);
            }
            triggered = true;
        }
    }
}
