using UnityEngine;

public class CatapultFunctionalTest : MonoBehaviour {
    [SerializeField]
    ObjectJoiner joiner;

    [SerializeField]
    GameObject[] sculptureComponents;

    [SerializeField]
    Vector3 joinCenter;

    [SerializeField]
    float testThrust;

    void Start () {
        GameObject sculpture = joiner.Join("SculptureTest", sculptureComponents, joinCenter);
        Catapult catapult = sculpture.GetComponent<Catapult>();
        catapult.thrust = testThrust;
        catapult.EnableDebug();
	}
}
