using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private Conversation conversation;

    // Start is called before the first frame update
    void Start () {
        conversation = new Conversation ();
    }

    // Update is called once per frame
    void Update () {
        conversation.Update ();
    }

    public void AdvanceConversation () {
        conversation.Activate ();
    }
}