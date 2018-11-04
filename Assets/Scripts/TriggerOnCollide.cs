using System;
using UnityEngine;

public class TriggerOnCollide : MonoBehaviour {
    [SerializeField]
    bool onlyTriggerOnce;

    bool triggered = false;

    Action<Collision> onCollide;

    public void SubscribeOnCollisionEnter(Action<Collision> onCollide) {
        onCollide += onCollide;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!triggered || !onlyTriggerOnce)
        {
            if (onCollide != null)
            {
                onCollide(collision);
            }
            triggered = true;
        }
    }
}
