using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SimplePlayerController : MonoBehaviour {
    ThirdPersonCharacter thirdPersonCharacter;
    
    public List<Transform> foundGuards;

    public LayerMask guardLayer;

    Color colorAlpha;

    void Awake() {
        foundGuards = new List<Transform>();
    }

    // Start is called before the first frame update
    void Start() {
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update() {

        for (int i = 0; i < AIController.GuardsInPlay.Count; i++) {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, AIController.GuardsInPlay[i].position - transform.position, out raycastHit, float.PositiveInfinity, guardLayer) 
                && !foundGuards.Contains(raycastHit.transform)) {
                foundGuards.Add(raycastHit.transform);
            }
        }
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Vector3 v3 = Vector3.zero;
        v3 += Vector3.right * h;
        v3 += Vector3.forward * v;

        thirdPersonCharacter.Move(v3, false, false);
    }
}
