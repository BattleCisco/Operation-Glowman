using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButton : MonoBehaviour
{
    public Button myButton;
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start(){
        myButton.onClick.AddListener(StartButtonPress);
    }

    void StartButtonPress() {
        HashController.instance.ToHash = inputField.text;
        SceneManager.LoadScene("_Scenes/TheGame");
    }
}
