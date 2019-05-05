using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General settingsmanager making sure it wont be destroyed between scenes by being a singleton.
/// </summary>
class SettingsManager : Singleton
{
    /// <summary>
    /// Indicates if debug mode is enabled or not.
    /// </summary>
    public static bool debug;

    protected override void Awake(){
        base.Awake();
        #if UNITY_EDITOR
            debug = true;
        #else
            debug = false;
        #endif

    }

}