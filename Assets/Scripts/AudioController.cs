using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip attackSound;

    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(deathSound);
    }

    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(hitSound);
    }

    public void PlayAttackSound()
    {
        _audioSource.PlayOneShot(attackSound);
    }
}
