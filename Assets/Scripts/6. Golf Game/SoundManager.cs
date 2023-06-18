using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SoundManager 스크립트는 오디오 관리를 담당합니다.
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // SoundManager 인스턴스를 저장하는 변수
    public static SoundManager Instance { get { return instance; } } // 외부에서 SoundManager에 접근하기 위한 인스턴스 접근자

    private Dictionary<string, AudioSource> mAudios = new Dictionary<string, AudioSource>(); // 오디오 소스를 저장하기 위한 딕셔너리

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // 이미 인스턴스가 존재하면 해당 오브젝트를 파괴하여 중복 생성을 방지합니다.
            return;
        }
        else
        {
            instance = this; // 인스턴스가 존재하지 않을 경우, 현재 인스턴스로 설정합니다.
        }

        // Scene에 있는 모든 AudioSource를 찾아서 딕셔너리에 추가합니다.
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            mAudios.Add(audio.clip.name, audio);
        }
    }

    // 지정된 오디오 클립 이름에 해당하는 오디오를 재생합니다.
    public void Play(string clipName)
    {
        mAudios[clipName].Play();
    }
}
