using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJoinerFunctionalTest : MonoBehaviour {
    [SerializeField]
    GameObject joiner;

    [SerializeField]
    GameObject[] objectsToJoin;

    [SerializeField]
    Vector3 joinCenter;

    void Start() {
        joiner.GetComponent<ObjectJoiner>().Join("JoinedObj", objectsToJoin, joinCenter);
    }
}
