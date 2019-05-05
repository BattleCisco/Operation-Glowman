using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform playerTransform;
    public float lerpSpeed;

    public Vector3 offset;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, lerpSpeed);    
    }
}
