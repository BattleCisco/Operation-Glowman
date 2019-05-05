using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float lerpValue=0.5f;
    Vector3 targetRotationCameraHolder;
    Vector3 targetRotationCameraHolder2;

    public Transform cameraHolder2;

    // Start is called before the first frame update
    void Start(){
        targetRotationCameraHolder = transform.rotation.eulerAngles;
        targetRotationCameraHolder2 = cameraHolder2.eulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, lerpValue);

        Vector3 rot1 = transform.rotation.eulerAngles;
        rot1.y += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(rot1);

        Vector3 rot2 = cameraHolder2.rotation.eulerAngles;
        rot2.x += -Input.GetAxis("Mouse Y");
        cameraHolder2.rotation = Quaternion.Euler(rot2);
    }
}
