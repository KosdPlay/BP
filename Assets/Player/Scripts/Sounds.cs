using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource audioSource => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, bool destroyed = false, float p1 = 0.9f, float p2 = 1.2f)
    {
        audioSource.pitch = Random.Range(p1, p2);

        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
