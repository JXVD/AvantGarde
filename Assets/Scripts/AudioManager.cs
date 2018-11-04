using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField]
    AudioSource musicChannel;

    [SerializeField]
    AudioSource sfxChannel;

    [SerializeField]
    AudioClip sculptingClip;

    [SerializeField]
    AudioClip glassClip;

    void Start()
    {
        musicChannel.clip = sculptingClip;
        musicChannel.Play();
        musicChannel.loop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        sfxChannel.clip = glassClip;
        sfxChannel.Play();
        sfxChannel.loop = false;
    }

}
