using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField]
    AudioSource musicChannel;

    [SerializeField]
    AudioClip sculptingClip;

    void Start()
    {
        musicChannel.clip = sculptingClip;
        musicChannel.Play();
    }
}
