using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    private Rigidbody mRig;

    private void Start()
    {
        mRig = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "Player")    
        {
            Vector3 dir = (transform.position - other.transform.position).normalized;
            mRig.AddForce(dir * 10.0f, ForceMode.Impulse);
        }
    }
}
