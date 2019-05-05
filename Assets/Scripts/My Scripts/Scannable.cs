using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scannable : MonoBehaviour{
    public abstract ScannableType GetScannableType();
}
