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
    AudioClip trillClip;

    [SerializeField]
    AudioClip cashRegisterClip;

    [SerializeField]
    AudioClip[] moneyClip;

    [Header("Tuning")]
    [SerializeField]
    float fadeTime = 1f;
    [SerializeField]
    float volumeDown = 0.25f;
    [SerializeField]
    float fullVolume = 1.0f;

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

    public void playTrill()
    {
        StartCoroutine(lerpVolume(musicChannel, volumeDown, fadeTime));
        cashChannel.clip = trillClip;
        cashChannel.Play();
        cashChannel.loop = false;
        StartCoroutine(waitToChangeVolume(musicChannel, fullVolume, cashChannel.clip.length));
    }

    IEnumerator waitToChangeVolume(AudioSource channel, float newVolume, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(lerpVolume(channel, newVolume, fadeTime));
    }

    IEnumerator lerpVolume(AudioSource channel, float newVolume, float lerpTime) {
        float originalVolume = channel.volume;
        float timer = 0;
        while (timer <= lerpTime) {
            channel.volume = Mathf.Lerp(originalVolume, newVolume, timer / lerpTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        channel.volume = newVolume;
    }
}
