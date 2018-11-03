using UnityEngine;

public class CatapultFunctionalTest : MonoBehaviour {
    [SerializeField]
    ObjectJoiner joiner;

    [SerializeField]
    GameObject[] sculptureComponents;

    [SerializeField]
    Vector3 joinCenter;

    void Start () {
        GameObject sculpture = joiner.Join("SculptureTest", sculptureComponents, joinCenter);
        Catapult catapult = sculpture.GetComponent<Catapult>();
        catapult.thrust = 5;
        catapult.EnableDebug();
	}
}
