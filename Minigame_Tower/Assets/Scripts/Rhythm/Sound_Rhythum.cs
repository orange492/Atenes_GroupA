using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound_Rhythum : SingletonPuzzle<Sound_Rhythum>
{
    AudioSource audioSource;
    AudioClip ClipNote;
    AudioClip ClipMusic;

    enum MusicState
    {
        Null,
        Play,
        Pause
    }

    MusicState musicState;
    public int bpm { get; set; } = 98;
    public float startTime { get; set; } = 5.4f; // -1.6f;

    protected override void Initialize()
    {
        audioSource = this.GetComponent<AudioSource>();
        ClipNote = Resources.Load<AudioClip>("Rhythm/DM-CGS-21");
        if(TowerManager.Inst.GetDifficulty() == 0)
        {
            ClipMusic = Resources.Load<AudioClip>("Rhythm/Younha");
        }
        else if(TowerManager.Inst.GetDifficulty() == 1)
        {
            ClipMusic = Resources.Load<AudioClip>("Rhythm/OneCoin");
        }
        else if (TowerManager.Inst.GetDifficulty() == 2)
        {
            ClipMusic = Resources.Load<AudioClip>("Rhythm/Apink");
        }

        Clear();

        if (SceneManager.GetActiveScene().name != "Rhythm")
        {
            return;
        }

        audioSource.clip = ClipMusic;
        musicState = MusicState.Play;
        audioSource.Play();
    }

    public string ClipName()
    {
        if (SceneManager.GetActiveScene().name != "Rhythm" || ClipMusic == null)
        {
            return null;
        }
        return ClipMusic.name;
    }

    public void PlayNoteSound()
    {
        audioSource.PlayOneShot(ClipNote);
    }

    public void PlayMusicSound()
    {
        musicState = MusicState.Play;
        audioSource.Play();
    }
    public void Clear()
    {
        musicState = MusicState.Null;
        audioSource.Stop();
        audioSource.time = 0;
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
            ResumeMusic(); // 음악 재실행
            return true;
        }
        else
        {
            PauseMusic(); // 음악 일시정지
            return false;
        }
    }

    public void ChangeTime(float time)
    {
        if (time > audioSource.clip.length)
        {
            audioSource.time = audioSource.clip.length;
        }
        else if (time < 0)
        {
            audioSource.time = 0;
        }
        else
        {
            audioSource.time = time;
        }
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying; // 음악이 재생 중인지 체크
    }

    public float MusicTime()
    {
        return audioSource.time;
    }

    public float MusicLength()
    {
        return ClipMusic.length; // 음악의 길이 체크

        //{
        //    rectTransform = GetComponent<RectTransform>();
        //    rectTransform.anchoredPosition = new Vector2(0, 0);
        //
        //
        //    textMesh.font = Resources.Load<TMP_FontAsset>("Fonts/Arial");
        //}
    }
    //void PlayAudioClipFromTime(AudioClip clip, float time) // 음악 시간 조정
    //{
    //    audioSource.clip = clip;
    //    audioSource.time = time;
    //    audioSource.Play();
    //}
    ///  JNL-101K
    ///  // What would like the script look like?
}
