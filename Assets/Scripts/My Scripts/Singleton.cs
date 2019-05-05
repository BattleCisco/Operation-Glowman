using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class to easily make singletons by inheriting.
/// </summary>
public class Singleton : MonoBehaviour
{
    /// <summary>
    /// The instance being held as singleton.
    /// </summary>
    public static Singleton _instance;

    /// <summary>
    /// Checks instance on awake, if null set to this, else set to instance.
    /// </summary>
    protected virtual void Awake(){
        if(_instance is null){
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
}
