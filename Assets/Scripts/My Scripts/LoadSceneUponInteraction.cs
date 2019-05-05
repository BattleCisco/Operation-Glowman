using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUponInteraction : MonoBehaviour
{
    public Transform playerTransform;
    public string sceneToLoad;
    public float sqrDistance = 4f;

    // Update is called once per frame
    void Update() {
        if ((transform.position - playerTransform.position).sqrMagnitude < sqrDistance && Input.GetKey(KeyCode.F))
            SceneManager.LoadScene(sceneToLoad);
    }
}
