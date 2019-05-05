using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flags : MonoBehaviour
{
    public static Flags flags;

    public bool invisibility;
    public bool invisibleGoalOnMinimap;
    public bool invisibleGuardsOnMinimap;
    public bool invisiblePlayerOnMinimap;
    public bool pausedGuards;

    // Start is called before the first frame update
    void Start()
    {
        if(flags == null) {
            flags = this;
        }
        else {
            flags.invisibility = invisibility;
            flags.invisibleGuardsOnMinimap = invisibleGuardsOnMinimap;
            flags.invisiblePlayerOnMinimap = invisiblePlayerOnMinimap;
            flags.pausedGuards = pausedGuards;
        }
    }
}
