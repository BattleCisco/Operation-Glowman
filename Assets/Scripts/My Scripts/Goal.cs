using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Scannable{
    public override ScannableType GetScannableType() {
        return ScannableType.Goal;
    }
}
