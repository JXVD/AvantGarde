using UnityEngine;

public class CatapultFunctionalTest : MonoBehaviour {
    [SerializeField]
    ObjectJoiner joiner;

    [SerializeField]
    GameObject[] sculptureComponents;

    [SerializeField]
    Vector3 joinCenter;

    [SerializeField]
    Vector2 testThrust;

    [SerializeField]
    float testJitterX = 1;

    Catapult catapult;

    void Start () {
        GameObject sculpture = joiner.Join("SculptureTest", sculptureComponents, joinCenter);
        catapult = sculpture.GetComponent<Catapult>();
        catapult.SetThrust(testThrust, testJitterX);
        catapult.EnableDebug();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            catapult.StartCountdown(0);
        }
    }
}
