using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Scannable {
    public override ScannableType GetScannableType() {
        return ScannableType.Player;
    }
}
