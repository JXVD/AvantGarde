using UnityEngine;

public class ObjectJoiner : MonoBehaviour
{
    public GameObject Join(string objectName, GameObject[] components, Vector3 center)
    {
        GameObject parent = new GameObject(objectName);
        Transform parentT = parent.transform;
        Rigidbody parentRigidbody = parent.AddComponent<Rigidbody>();
        parentT.position = center;
        foreach (GameObject target in components) 
        {
            foreach (GameObject other in components)
            {
                Physics.IgnoreCollision(target.GetComponent<Collider>(), other.GetComponent<Collider>());
            }
        }
        float totalMass = 0;
        foreach (GameObject comp in components)
        {
            FixedJoint parentJoint = parent.AddComponent<FixedJoint>();
            Rigidbody compRigibody = comp.GetComponent<Rigidbody>();
            compRigibody.freezeRotation = true;
            compRigibody.isKinematic = false;
            compRigibody.useGravity = true;
            totalMass += compRigibody.mass;
            parentJoint.connectedBody = compRigibody;
            comp.transform.SetParent(parentT);
        }
        parentRigidbody.mass = totalMass;
        parent.AddComponent<Catapult>();
        return parent;
    }
}
