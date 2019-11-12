using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FloorIdentifier : MonoBehaviour {

    [SerializeField] private AudioClip[] m_GrassSteps;
    [SerializeField] private AudioClip[] m_ConcreteSteps;
    [SerializeField] private AudioClip[] m_WoodSteps;

    private FirstPersonController m_FirstPersonController;
    private string previousFloor;

    // Start is called before the first frame update
    void Start () {
        m_FirstPersonController = GetComponent<FirstPersonController> ();
        previousFloor = "Untagged";
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
            string floorTag = hit.collider.gameObject.tag;

            // Wood
            if (floorTag == "WoodFloor" && floorTag != previousFloor) {
                previousFloor = floorTag;
                m_FirstPersonController.ChangeFootstepAudio (m_WoodSteps);
            }

            // Concrete
            else if (floorTag == "ConcreteFloor" && floorTag != previousFloor) {
                previousFloor = floorTag;
                m_FirstPersonController.ChangeFootstepAudio (m_ConcreteSteps);
            }

            // Grass
            else if (floorTag == "Untagged" && floorTag != previousFloor) {
                previousFloor = floorTag;
                m_FirstPersonController.ChangeFootstepAudio (m_GrassSteps);
            }
        }
    }
}