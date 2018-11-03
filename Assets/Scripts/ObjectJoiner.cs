using UnityEngine;

public class ObjectJoiner : MonoBehaviour
{
    public GameObject Join(string objectName, GameObject[] components, Vector3 center)
    {
        GameObject parent = new GameObject(objectName);
        Transform parentT = parent.transform;
        parent.AddComponent<Rigidbody>();
        parentT.position = center;
        foreach (GameObject comp in components)
        {
            MeshFilter filter;
            FixedJoint parentJoint = parent.AddComponent<FixedJoint>();
            Rigidbody compRigibody = comp.GetComponent<Rigidbody>();
            compRigibody.isKinematic = false;
            compRigibody.useGravity = true;
            parentJoint.connectedBody = compRigibody;
            comp.transform.SetParent(parentT);
        }
        return parent;
    }
}
