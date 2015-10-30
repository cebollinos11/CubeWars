using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {

    public GameObject InstructionsPanel;

	// Use this for initialization
	void Start () {
        InstructionsPanel.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

        if (CheckForInput()) {
            GetComponent<PauseMenuManager>().ResumeGame();
            Destroy(InstructionsPanel);
            enabled = false;
            
        }
	
	}

    private bool CheckForInput()
    {
        if (Input.anyKey) { return true; }
        return false;
    }
}
