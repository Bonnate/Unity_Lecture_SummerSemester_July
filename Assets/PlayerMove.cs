using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator mAnimator;
    private Rigidbody mRig;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.Translate(input * Time.deltaTime * 10.0f);
        mAnimator.SetFloat("MovementSpeed", input.magnitude);

        float mouseDelta = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0f, mouseDelta, 0f) * Time.deltaTime * 720);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            mRig.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
            mAnimator.SetTrigger("Jumping");
        }
    }
}
