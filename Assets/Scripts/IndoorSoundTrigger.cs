using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorSoundTrigger : MonoBehaviour {
    // Start is called before the first frame update

    public GameObject OutdoorSound;
    private AudioSource outdoorSource;
    private AudioLowPassFilter outdoorFilter;
    public GameObject IndoorSound;
    private AudioSource indoorSource;
    private AudioLowPassFilter indoorFilter;

    void Start () {
        outdoorSource = OutdoorSound.transform.GetComponent<AudioSource> ();
        indoorSource = IndoorSound.transform.GetComponent<AudioSource> ();

        outdoorFilter = OutdoorSound.transform.GetComponent<AudioLowPassFilter> ();
        indoorFilter = IndoorSound.transform.GetComponent<AudioLowPassFilter> ();

        outdoorSource.Play ();
        indoorSource.Stop ();

        outdoorFilter.enabled = false;
        indoorFilter.enabled = false;
    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            indoorSource.Play ();
            outdoorFilter.enabled = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            indoorSource.Stop ();
            outdoorFilter.enabled = false;
        }
    }
}