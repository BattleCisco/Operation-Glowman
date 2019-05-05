using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawAIPath : MonoBehaviour
{
    public Transform playerTransform;
    public List<Material> materialForPaths;
    public List<AIController> aIControllers;
    public float baseWidth;
    public float lengthScalar;

    ScannerUI scannerUI;

    public Vector3 scalar;

    void Start(){
        scannerUI = GetComponent<ScannerUI>();
    }

    void LateUpdate()
    {
        scannerUI.ClearPaths();
        for(int d=0; d < aIControllers.Count; d++) {
            List<Node> AIPath = aIControllers[d].GeneratePath();
            
            for(int i = 0; i < AIPath.Count - 1; i++) {
                Vector3[] points = new Vector3[]{
                    (scannerUI.TranslateVector(AIPath[i].transform.position, playerTransform.position)),
                    (scannerUI.TranslateVector(AIPath[i + 1].transform.position, playerTransform.position))
                };
                for (int c = 0; c < points.Length; c++) {
                    points[c].x *= scalar.x;
                    points[c].y *= scalar.y;
                    points[c].z *= scalar.z;
                    points[c] += transform.position;
                }

                scannerUI.DrawnALine(points, baseWidth, materialForPaths[d % materialForPaths.Count]);
            }
        }    
    }
}
