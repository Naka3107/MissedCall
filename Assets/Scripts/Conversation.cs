using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase {
    private string Data;

    public Phrase (string Data) {
        this.Data = Data;
    }

    public string ToString () {
        return Data;
    }
}

public class Node {
    private Phrase Trigger, Consequence;
    private List<Node> Children;

    public Node (Phrase Trigger, Phrase Consequence) {
        this.Trigger = Trigger;
        this.Consequence = Consequence;
        this.Children = new List<Node> ();
    }

    public Phrase GetTrigger () {
        return Trigger;
    }

    public Phrase GetConsequence () {
        return Consequence;
    }

    public void AddSon (Node son) {
        Children.Add (son);
    }

    public Node GetSon (int i) {
        return Children[i];
    }

    public List<Node> GetAllSons () {
        return Children;
    }

    public string ToString () {
        return "Trigger: " + Trigger.ToString () +
            "\n" + "Consequence: " + Consequence.ToString () +
            "\n" + "Children: " + Children.Count.ToString ();
    }
}

public class Conversation {
    private Node root;
    private Node currentMessage;
    private bool isActive;
    private bool hasStarted;

    public Conversation () {
        Node parent;

        root = new Node (
            null,
            new Phrase ("Hello. I just wanna know how are you?")
        );

        root.AddSon (new Node (
            new Phrase ("Fine, thank you..."),
            new Phrase ("Good. You had me worried.")
        ));

        parent = root.GetSon (0);
        parent.AddSon (new Node (
            new Phrase ("Do we know each other?"),
            new Phrase ("I do.")
        ));

        parent.AddSon (new Node (
            new Phrase ("Why?"),
            new Phrase ("She will leave you.")
        ));

        parent.AddSon (new Node (
            new Phrase ("Who are you?"),
            new Phrase ("")
        ));

        parent = parent.GetSon (1);
        parent.AddSon (new Node (
            new Phrase ("Rosie?!"),
            new Phrase ("")
        ));

        parent.AddSon (new Node (
            new Phrase ("Mom?!"),
            new Phrase ("")
        ));

        parent.AddSon (new Node (
            new Phrase ("Who?"),
            new Phrase ("")
        ));

        currentMessage = root;

        isActive = false;
        hasStarted = false;
    }

    public void Activate () {
        isActive = true;

        if (!hasStarted) {
            Debug.Log (currentMessage.GetConsequence ().ToString ());
            int i = 0;
            foreach (Node son in currentMessage.GetAllSons ()) {
                Debug.Log (i + ") " + son.GetTrigger ().ToString ());
                i++;
            }

            hasStarted = true;
        }

    }

    public void DeActivate () {
        isActive = false;
    }

    public void Update () {
        if (!isActive) return;

        if (Input.GetKeyDown ("0")) {
            currentMessage = currentMessage.GetSon (0);

            Debug.Log (currentMessage.GetConsequence ().ToString ());
            int i = 0;
            foreach (Node son in currentMessage.GetAllSons ()) {
                Debug.Log (i + ") " + son.GetTrigger ().ToString ());
                i++;
            }
        }

        if (Input.GetKeyDown ("1")) {
            currentMessage = currentMessage.GetSon (1);

            Debug.Log (currentMessage.GetConsequence ().ToString ());
            int i = 0;
            foreach (Node son in currentMessage.GetAllSons ()) {
                Debug.Log (i + ") " + son.GetTrigger ().ToString ());
                i++;
            }
        }

        if (Input.GetKeyDown ("2")) {
            currentMessage = currentMessage.GetSon (2);

            Debug.Log (currentMessage.GetConsequence ().ToString ());
            int i = 0;
            foreach (Node son in currentMessage.GetAllSons ()) {
                Debug.Log (i + ") " + son.GetTrigger ().ToString ());
                i++;
            }
        }
    }
}