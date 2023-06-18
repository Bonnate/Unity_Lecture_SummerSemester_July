using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SphereFallingChecker 스크립트는 충돌이 벗어난 객체가 FallingSphere 컴포넌트를 가지고 있는지 확인하고,
// 해당 컴포넌트를 가진 객체가 있다면 원래 위치로 이동시킵니다.
public class SphereFallingChecker : MonoBehaviour
{
    // OnTriggerExit는 Collider가 이 스크립트가 부착된 게임 오브젝트와 충돌 범위를 벗어날 때 호출됩니다.
    private void OnTriggerExit(Collider other)
    {
        FallingSphere? fallingSphere = null; // FallingSphere 컴포넌트를 저장할 Nullable 타입 변수 선언 및 초기화

        // other 객체에서 FallingSphere 컴포넌트를 가져오려 시도합니다.
        // TryGetComponent는 객체에서 지정한 컴포넌트를 가져오는 메서드이며, 가져오는데 성공하면 true를 반환합니다.
        if (other.TryGetComponent<FallingSphere>(out fallingSphere))
        {
            fallingSphere.MoveToOrigin(); // FallingSphere 객체의 MoveToOrigin() 메서드 호출하여 원래 위치로 이동시킵니다.
        }
    }
}