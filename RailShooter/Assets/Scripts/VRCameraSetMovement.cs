using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class VRCameraSetMovement : MonoBehaviour
{
    [SerializeField]
    CinemachineSmoothPath path = null;

    public float point = 0f;

    void Update()
    {
        transform.position = path.EvaluatePosition(point);
        transform.rotation = path.EvaluateOrientation(point);
    }
}
