using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private Conversation conversation;
    private GameObject player;

    // Start is called before the first frame update
    void Start () {
        conversation = new Conversation ();
        player = GameObject.Find ("/Player").gameObject;
    }

    // Update is called once per frame
    void Update () {
        conversation.Update ();
    }

    public void AdvanceConversation () {
        conversation.Activate ();

        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
        player.GetComponentInChildren<RaycastInteraction> ().enabled = false;

    }

    public void CheckConversationProgress () {
        if (!conversation.isActive ()) {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
            player.GetComponentInChildren<RaycastInteraction> ().enabled = true;
        }
    }
}