using System.Collections;
using UnityEngine;

public class ForceExit : MonoBehaviour {
    [SerializeField]
    float secondsToWait = 3;

    [SerializeField]
    bool debugEnabled = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(exitAfterSeconds(secondsToWait));
	}
	
    IEnumerator exitAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        if (debugEnabled) Debug.Log("Quitting game");
        Application.Quit();
    }
}
