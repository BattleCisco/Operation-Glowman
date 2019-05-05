using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ScannerState { Waiting, Scanning}

/// <summary>
/// This class actually does the scanning, using 3 transform to handle the position of the scan.
/// </summary>
public class ScannerController : MonoBehaviour
{
    public float scannerInterval=10f;
    public float scanSpeed = 5f;
    public Coroutine scanner=null;

    float timeCounter;

    float currentScan = 0f;

    public Transform scanTransform;
    public Transform maxTransform;
    public Transform minTransform;

    ScannerUI scannerUI;

    private ScannerState state;

    Vector3 difference;

    // Start is called before the first frame update
    void Start() {
        timeCounter = 0f;
        difference = (maxTransform.position - minTransform.position).normalized;
        scannerUI = GetComponent<ScannerUI>();
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case ScannerState.Waiting:
                if (scanner == null) {
                    timeCounter += Time.deltaTime;

                    if (timeCounter > scannerInterval) {
                        scanTransform.position = minTransform.position;
                        state = ScannerState.Scanning;
                        timeCounter = 0f;
                    }
                }
                break;

            case ScannerState.Scanning:
                scannerUI.CurrentScanLocation(scanTransform.position);
                Collider[] scannedObjects = Physics.OverlapBox(scanTransform.position, difference + scanTransform.localScale * 0.5f);
                for(int i=0; i < scannedObjects.Length; i++) {
                    Scannable scannable = scannedObjects[i].transform.GetComponent<Scannable>();
                    if (!(scannable is null)) {
                        scannerUI.OnFoundScannable(scannedObjects[i].transform, scannable.GetScannableType());
                    }
                }
                scanTransform.position += difference * Time.deltaTime * scanSpeed;
                
                if ((maxTransform.position - scanTransform.position).sqrMagnitude < 1f || scanTransform.position.x > maxTransform.position.x) {
                    scannerUI.OnFinishedScan();
                    state = ScannerState.Waiting;
                }
                break;
            default:
                break;
        }

        
    }
}
