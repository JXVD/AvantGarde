using System.Collections.Generic;
using UnityEngine;

public class ObjectJoiner : MonoBehaviour
{
    public GameObject Join(string objectName, GameObject[] components, Vector3 center)
    {
        GameObject parent = new GameObject(objectName);
        Transform parentT = parent.transform;
        parentT.position = center;
        List<Collider> colliders = new List<Collider>();
        for (int i = 0; i < components.Length; i++)
        {
            Collider newCollider = components[i].GetComponent<Collider>();
            colliders.ForEach((Collider collider) => {
                Physics.IgnoreCollision(collider, newCollider);
            });
            colliders.Add(newCollider);
            int index = i - 1;
            if (index == -1) {
                index = components.Length - 1;
            }
            FixedJoint parentJoint = components[index].AddComponent<FixedJoint>();
            Rigidbody compRigibody = components[i].GetComponent<Rigidbody>();
            compRigibody.isKinematic = false;
            compRigibody.useGravity = true;
            parentJoint.connectedBody = compRigibody;
            components[i].transform.SetParent(parentT);
        }
        parent.AddComponent<Catapult>();
        return parent;
    }
}
