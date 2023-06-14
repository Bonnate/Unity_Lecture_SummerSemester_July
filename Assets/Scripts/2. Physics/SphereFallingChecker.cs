using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFallingChecker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        FallingSphere? fallingSphere = null;

        if (other.TryGetComponent<FallingSphere>(out fallingSphere))
        {
            fallingSphere.MoveToOrigin();
        }
    }
}
