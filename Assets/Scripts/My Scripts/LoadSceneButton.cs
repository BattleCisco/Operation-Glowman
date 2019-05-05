using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadSceneButton : MonoBehaviour {
    public Button myButton;
    public string sceneToLoad = string.Empty;

    // Start is called before the first frame update
    void Start() {
        myButton.onClick.AddListener(ButtonPress);
    }

    void ButtonPress() {
        SceneManager.LoadScene(sceneToLoad);
    }
}
