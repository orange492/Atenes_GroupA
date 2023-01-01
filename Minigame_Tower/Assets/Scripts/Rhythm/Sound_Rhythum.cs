using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Rhythum : SingletonPuzzle<Sound_Rhythum>
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip ClipNote;
    [SerializeField]
    AudioClip ClipMusic;

    enum MusicState
    {
        Null,
        Play,
        Pause
    }

    MusicState musicState;
    public int bpm { get; set; } = 98;
    public float startTime { get; set; } = 1.1f; // -1.6f;

    public void Start()
    {
        musicState = MusicState.Play;
        audioSource.PlayOneShot(ClipMusic);
    }

    public void PlayNoteSound()
    {
        audioSource.PlayOneShot(ClipNote);
    }

    public void PlayMusicSound()
    {
        musicState = MusicState.Play;
        audioSource.PlayOneShot(ClipMusic);
    }
    public void Clear()
    {
        musicState = MusicState.Null;
        audioSource.Stop();
    }
    public void PauseMusic()
    {
        musicState = MusicState.Pause;
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        musicState = MusicState.Play;
        audioSource.UnPause();
    }

    public bool PlayOrPauseButton()
    {
        if (musicState == MusicState.Null)
        {
            PlayMusicSound();
            return true;
        }
        else if (musicState==MusicState.Pause)
        {
            ResumeMusic();
            return true;
        }
        else
        {
            PauseMusic();
            return false;
        }
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
    public float MusicLength()
    {
        return ClipMusic.length;
    }

}
