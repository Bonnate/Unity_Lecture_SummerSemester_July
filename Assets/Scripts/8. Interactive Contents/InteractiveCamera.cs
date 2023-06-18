using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InteractiveCamera 스크립트는 카메라에서 마우스 클릭으로 상호작용 가능한 오브젝트를 감지하는 기능을 제공합니다.
public class InteractiveCamera : MonoBehaviour
{
    private Camera mMainCamera; // 주 카메라를 저장할 변수

    private void Awake()
    {
        mMainCamera = GetComponent<Camera>(); // 주 카메라를 가져와 변수에 할당합니다.
    }

    private void Update()
    {
        Raycast(); // 레이캐스트를 실행하여 상호작용 가능한 오브젝트를 감지합니다.
    }

    // 마우스 클릭 위치로 레이캐스트를 실행하여 상호작용 가능한 오브젝트를 감지합니다.
    private void Raycast()
    {
        Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition); // 마우스 클릭 위치를 스크린 좌표로 변환하여 레이를 생성합니다.
        RaycastHit hit; // 레이캐스트 결과를 저장할 변수

        if (Physics.Raycast(ray, out hit)) // 레이캐스트를 실행하고 충돌한 오브젝트가 있을 경우
        {
            if (hit.transform.tag == "InteractiveProp") // 충돌한 오브젝트의 태그가 "InteractiveProp"인 경우
            {
                hit.transform.GetComponent<InteractiveProps>().Force(hit.point, 2.0f); // 충돌한 오브젝트의 InteractiveProps 컴포넌트를 가져와서 Force() 함수를 호출합니다.
            }
        }
    }
}
