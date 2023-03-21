using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    private Vector3 lastFramePos;
    private Quaternion lastFrameRotation;

    void Start()
    {
        lastFramePos = transform.position;
        lastFrameRotation = transform.rotation;
    }
    void Update()
    {
        lastFramePos = transform.position;
        lastFrameRotation = transform.rotation;
    }
    public void ShootStone(Vector3 Direction)
    {

    }

}
