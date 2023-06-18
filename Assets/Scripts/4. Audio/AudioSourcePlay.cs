using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioSourcePlay 스크립트는 AudioSource를 사용하여 2D 사운드를 재생하고 멈추는 기능을 제공합니다.
[RequireComponent(typeof(AudioSource))]
public class AudioSourcePlay : MonoBehaviour
{
    private AudioSource mAudioSource; // AudioSource 컴포넌트를 저장할 변수

    private void Awake()
    {
        // Awake() 함수는 스크립트가 활성화되면서 최초로 호출되는 함수입니다.
        // GetComponent<AudioSource>()를 사용하여 이 스크립트가 부착된 게임 오브젝트의 AudioSource 컴포넌트를 가져옵니다.
        mAudioSource = GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        // OnGUI() 함수는 매 프레임마다 호출되며, GUI 요소를 그리기 위해 사용됩니다.
        // 여기서는 두 개의 버튼을 그립니다.

        // 첫 번째 버튼은 2D 사운드 재생 버튼입니다.
        // 버튼이 클릭되면 mAudioSource.Play() 함수를 호출하여 AudioSource를 통해 연결된 오디오 파일을 재생합니다.
        if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.2f, Screen.height * 0.1f), "2D 사운드 재생"))
        {
            mAudioSource.Play();
        }

        // 두 번째 버튼은 2D 사운드 멈춤 버튼입니다.
        // 버튼이 클릭되면 mAudioSource.Stop() 함수를 호출하여 현재 재생 중인 오디오를 멈춥니다.
        if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.1f), "2D 사운드 멈춤"))
        {
            mAudioSource.Stop();
        }        
    }
}
