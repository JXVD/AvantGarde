using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource musicChannel;

    [SerializeField]
    AudioSource sfxChannel;

    [SerializeField]
    AudioSource cashChannel;

    [SerializeField]
    AudioClip sculptingClip;

    [SerializeField]
    AudioClip glassClip;


    [SerializeField]
    AudioClip cashRegisterClip;

    [SerializeField]
    AudioClip[] moneyClip;

    System.Random rando = new System.Random();
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

    public void playMoneyNoise()
    {

        cashChannel.clip = cashRegisterClip;
        cashChannel.Play();
        sfxChannel.clip = moneyClip[rando.Next(0, moneyClip.Length-1)];
        sfxChannel.Play();
        sfxChannel.loop = false;
    }



}
