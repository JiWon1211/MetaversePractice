using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    public Transform target;
    public float zOffset = -10f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("FollowCamera: Target is not assigned! Please assign a target in the Inspector.");
            enabled = false;
            return;
        }

        transform.position = new Vector3(target.position.x, target.position.y, target.position.z + zOffset);
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x = target.position.x;
        newCameraPosition.y = target.position.y;
        newCameraPosition.z = target.position.z + zOffset;
        transform.position = newCameraPosition;
    }
}

