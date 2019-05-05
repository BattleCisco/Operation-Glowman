using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScannableType { Wall, Guard, Player, Goal }


/// <summary>
/// Displays the found objects to the player
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ScannerUI : MonoBehaviour {
    /// <summary>
    /// Prefabs for various icons
    /// </summary>
    public GameObject guardPrefab;
    public GameObject goalPrefab;
    public GameObject playerPrefab;
    public GameObject wallPrefab;

    /// <summary>
    /// Parent is just where the script will place the objects in the hierarchy. Does not affect functionality.
    /// </summary>
    public Transform parentObject;
    public Transform playerTransform;

    /// <summary>
    /// Dictionairys to seperate objects found on previous scan and objects found during this scan to allow for deletion of object mid scan.
    /// </summary>
    public Dictionary<Transform, GameObject> currentlyScanning = new Dictionary<Transform, GameObject>();
    public Dictionary<Transform, GameObject> alreadyScanned = new Dictionary<Transform, GameObject>();
    public List<GameObject> currentlyScanningRectangles = new List<GameObject>();
    public List<GameObject> alreadyScannedRectangles = new List<GameObject>();

    /// <summary>
    /// Vectors to scale the translation when converting from world position to 2D position on the map.
    /// </summary>
    public Vector2 convertionScaler = new Vector2(1f, 1f);

    /// <summary>
    /// Wall required it's own scaler, otherwise they would be tiny and far distanced apart.
    /// </summary>
    public Vector2 wallAmplificationScaler = new Vector2(1f, 1f);

    /// <summary>
    /// The rect transform for the panel in order to check if the object found will be on the minimap if placed there.
    /// </summary>
    RectTransform rectTransform;

    public Vector3 angleOffset;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    public Vector2 TranslateVector(Vector3 inputVector, Vector3 playerAtTimeOfScan) {
        Vector3 middleStep = inputVector - playerAtTimeOfScan;
        //Debug.Log(new Vector2(middleStep.x * convertionScaler.x, middleStep.z * convertionScaler.y));
        return new Vector2(middleStep.x * convertionScaler.x, middleStep.z * convertionScaler.y);
    }

    public void OnFoundScannable(Transform other, ScannableType type) {
        if (currentlyScanning.ContainsKey(other))
            return;

        Vector3 translatedVector = TranslateVector(other.position, playerTransform.position);

        // Pre-check to abort if the objects will be placed outside of the minimap.
        switch (type) {
            case ScannableType.Goal:
                if (Flags.flags.invisibleGoalOnMinimap || (translatedVector.x + rectTransform.rect.width * 0.5f) < 0f)
                    return;
                break;
            case ScannableType.Guard:
                if (Flags.flags.invisibleGuardsOnMinimap || (translatedVector.x + rectTransform.rect.width * 0.5f) < 0f)
                    return;
                break;
            case ScannableType.Player:
                if (Flags.flags.invisiblePlayerOnMinimap || (translatedVector.x + rectTransform.rect.width * 0.5f) < 0f)
                    return;
                break;
            case ScannableType.Wall:
                if ((translatedVector.x - other.localScale.x * 0.5f + rectTransform.rect.width * 0.5f) <= 0f)
                    return;
                break;
        }

        Vector3 offset = new Vector3(0f, 0f, 0.01f);
        GameObject gameObject;
        switch (type) {
            case ScannableType.Goal:
                gameObject = Instantiate(goalPrefab, translatedVector - offset, Quaternion.Euler(angleOffset), parentObject);
                gameObject.transform.localPosition = translatedVector - offset;
                break;
            case ScannableType.Guard:
                gameObject = Instantiate(guardPrefab, translatedVector - offset, Quaternion.Euler(angleOffset), parentObject);
                gameObject.transform.localPosition = translatedVector - offset;
                break;
            case ScannableType.Player:
                gameObject = Instantiate(playerPrefab, translatedVector - offset, Quaternion.Euler(angleOffset), parentObject);
                gameObject.transform.localPosition = translatedVector - offset;
                break;
            case ScannableType.Wall:
                gameObject = Instantiate(wallPrefab, translatedVector - offset, Quaternion.Euler(angleOffset), parentObject);
                gameObject.transform.localPosition = translatedVector - offset;
                Vector3 scale = gameObject.transform.localScale;
                scale.x *= other.transform.localScale.x * wallAmplificationScaler.x;
                scale.y *= other.transform.localScale.z * wallAmplificationScaler.y;
                gameObject.transform.localScale = scale;
                break;
            default:
                throw new KeyNotFoundException();
        }

        currentlyScanning.Add(other, gameObject);
    }

    public void DrawnALine(Vector3[] points, float width, Material m) {
        GameObject gameObject = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(angleOffset), parentObject);
        gameObject.transform.position = Vector3.zero;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
        lr.material = m;
        lr.SetPositions(points);
        lr.SetWidth(width, width);
        lr.enabled = true;
        currentlyScanningRectangles.Add(gameObject);
    }

    public void CurrentScanLocation(Vector3 location) {
        List<Transform> alreadyScan = new List<Transform>();
        foreach (Transform t in alreadyScanned.Keys) {
            alreadyScan.Add(t);
        }
        
        foreach(Transform t in alreadyScan) {
            if (location.x >= t.position.x) {
                Destroy(alreadyScanned[t]);
                alreadyScanned.Remove(t);
            }
        }
    }

    public void OnStartScan() {

    }

    public void OnFinishedScan() {
        foreach (Transform t in alreadyScanned.Keys) {
            Destroy(alreadyScanned[t]);
            alreadyScanned.Remove(t);
        }
        alreadyScanned = new Dictionary<Transform, GameObject>();
        foreach (Transform t in currentlyScanning.Keys) {
            alreadyScanned.Add(t, currentlyScanning[t]);
        }    
        currentlyScanning = new Dictionary<Transform, GameObject>();

        ClearPaths();
    }

    public void ClearPaths() {
        List<GameObject> tempList = new List<GameObject>(alreadyScannedRectangles);
        foreach (GameObject go in tempList) {
            Destroy(go);
            alreadyScannedRectangles.Remove(go);
        }
        alreadyScannedRectangles = new List<GameObject>();

        tempList = new List<GameObject>(currentlyScanningRectangles);
        foreach (GameObject go in tempList) {
            alreadyScannedRectangles.Add(go);
        }
        currentlyScanningRectangles = new List<GameObject>();
    }
}
