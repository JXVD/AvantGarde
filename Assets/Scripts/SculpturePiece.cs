using UnityEngine;

public class SculpturePiece : MonoBehaviour {
    [SerializeField]
    bool debugEnabled = false;

    void OnCollisionEnter(Collision collision) {
        if (debugEnabled)
        {
            Debug.Log(name + ": " + collision.collider);
        }
    }
}
