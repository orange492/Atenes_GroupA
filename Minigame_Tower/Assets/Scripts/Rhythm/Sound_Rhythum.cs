using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Rhythum : SingletonPuzzle<Sound_Rhythum>
{
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip ClipNote;
    [SerializeField]
    public AudioClip ClipMusic;

    public void Start()
    {
        audioSource.PlayOneShot(ClipMusic);
    }

    public void PlayNoteSound()
    {
        audioSource.PlayOneShot(ClipNote);
    }

    public void PlayMusicSound()
    {
        audioSource.PlayOneShot(ClipMusic);
    }

}
