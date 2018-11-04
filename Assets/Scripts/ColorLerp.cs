using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour {
    [SerializeField]
    Color[] colors;
    [SerializeField]
    float timePerTransition = 1.0f;
    [SerializeField]
    Image image;
    int currentIndex = 0;
    float timer;

	// Use this for initialization
	void Start () {
        image.color = colors[currentIndex];
	}
	
	// Update is called once per frame
	void Update () {
        image.color = Color.Lerp(colors[currentIndex], colors[(currentIndex + 1) % colors.Length], timer / timePerTransition);
        if (timer >= timePerTransition)
        {
            timer = 0;
            currentIndex++;
            currentIndex = currentIndex % colors.Length;
        }
        else
        {
            timer += Time.deltaTime;
        }
	}
}
