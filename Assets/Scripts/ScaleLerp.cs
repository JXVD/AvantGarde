using UnityEngine;

public class ScaleLerp : MonoBehaviour {
    [SerializeField]
    Transform sTransform;
    [SerializeField]
    Vector3 minScale = Vector3.one;
    [SerializeField]
    Vector3 maxScale = Vector3.one * 1.5f;
    [SerializeField]
    float timeToScale = 2f;
    float timer = 0;
    bool increasing = true;
	// Update is called once per frame
	void Update () {
        if (timer >= timeToScale)
        {
            timer = 0;
            increasing = !increasing;
        }
        else
        {
            sTransform.localScale = Vector3.Lerp(increasing ? minScale : maxScale,
                                                 increasing ? maxScale : minScale,
                                                 timer / timeToScale);
            timer += Time.deltaTime;
        }
	}
}
