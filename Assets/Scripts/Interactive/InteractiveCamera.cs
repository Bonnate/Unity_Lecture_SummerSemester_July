using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractiveCamera : MonoBehaviour
{
    private Camera mMainCamera;

    private void Awake()
    {
        mMainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Raycast();
    }

    private void Raycast()
    {
        Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "InteractiveProp")
            {
                hit.transform.GetComponent<InteractiveProps>().Force(hit.point, 2.0f);
            }
        }
    }
}
