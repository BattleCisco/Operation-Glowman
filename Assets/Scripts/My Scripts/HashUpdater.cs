using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HashUpdater : MonoBehaviour
{
    public TMP_InputField field;

    // Start is called before the first frame update
    void Start(){
        field = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update() {
        HashController.instance.ToHash = field.text;
    }
}
