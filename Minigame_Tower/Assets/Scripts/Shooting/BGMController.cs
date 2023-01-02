using UnityEngine;

public enum BGMType { Stage = 0, Boss }

public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips;       // 배경음악 파일 목록
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBGM(BGMType index)
    {
        // 현재 재생 중인 배경음악 정지
        audioSource.Stop();

        // Tip. 다른 클래스에서 BGM을 설정할 때 정수를 사용하면 어떤 BGM이 몇번인지 확인하려면
        // Inspector View의 bgmClips[]를 확인해야 알 수 있기 때문에 열거형을 이용해 가독성을 높여준다.

        // 배경음악 파일 목록에서 index번째 배경음악으로 파일 교체
        audioSource.clip = bgmClips[(int)index];
        // 바뀐 배경음악 재생
        audioSource.Play();
    }
}


/*
 * File : BGMController.cs
 * Desc
 *	: 배경음악 파일을 bgmClips 배열 변수에 저장해두고, 원하는 배경음악을 재생
 *	
 * Functions
 *  : ChangeBGM() - 배경음악을 변경해 재생
 */