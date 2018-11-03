using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJoiner : MonoBehaviour {
    public GameObject Join(string objectName, GameObject[] components, Vector3 center) {
        GameObject parent = new GameObject(objectName);
        Transform parentT = parent.transform;
        parent.AddComponent<Rigidbody>();
        parentT.position = center;
        foreach (GameObject comp in components) {
            FixedJoint parentJoint = parent.AddComponent<FixedJoint>();
            parentJoint.connectedBody = comp.GetComponent<Rigidbody>();
            comp.transform.SetParent(parentT);
        }
        return parent;
    }
}
