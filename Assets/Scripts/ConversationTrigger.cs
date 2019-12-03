using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypesOfConversation {
    MOM_01,
    ROSIE_01
}

public class ConversationTrigger : MonoBehaviour {
    [SerializeField] private TypesOfConversation m_ConversationID;
    [SerializeField] private GameObject m_GameManager;

    private Conversation conversation;

    // Start is called before the first frame update
    void Start () {
        switch (m_ConversationID) {
            case TypesOfConversation.MOM_01:
                conversation = new Conversation (CreateConversationMom01 ());
                break;
        }
    }

    void OnTriggerEnter (Collider other) {
        m_GameManager.GetComponent<GameManager> ().StartConversation (conversation);
    }

    void OnTriggerStay (Collider other) {
        m_GameManager.GetComponent<GameManager> ().CheckConversationProgress ();
        if (m_GameManager.GetComponent<GameManager> ().HasConversationEnded ()) gameObject.SetActive (false);
    }

    Node CreateConversationMom01 () {
        Node root;
        Node parent;

        root = new Node (
            null,
            new Phrase ("Hello. I just wanna know how are you?")
        );

        root.AddSon (new Node (
            new Phrase ("[0] Fine, thank you..."),
            new Phrase ("Good. You had me worried.")
        ));

        parent = root.GetSon (0);
        parent.AddSon (new Node (
            new Phrase ("[0] Do we know each other?"),
            new Phrase ("I do."),
            true
        ));

        parent.AddSon (new Node (
            new Phrase ("[1] Why?"),
            new Phrase ("She will leave you.")
        ));

        parent.AddSon (new Node (
            new Phrase ("[2] Who are you?"),
            new Phrase (""),
            true
        ));

        parent = parent.GetSon (1);
        parent.AddSon (new Node (
            new Phrase ("[0] Rosie?!"),
            new Phrase (""),
            true
        ));

        parent.AddSon (new Node (
            new Phrase ("[1] Mom?!"),
            new Phrase (""),
            true
        ));

        parent.AddSon (new Node (
            new Phrase ("[2] Who?"),
            new Phrase (""),
            true
        ));

        return root;
    }
}