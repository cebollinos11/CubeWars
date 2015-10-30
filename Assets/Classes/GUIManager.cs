using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager> {

    public GameObject MainMenu;
    public Dropdown StageSelector;
    public GameObject InstructionsPanel;
    public GameObject EndOfRoundPanel;
    public Text EndOfRoundScoreText;

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(gameObject);

        //Init stage selector
        StageSelector.options.Clear();
        foreach(StageClass S in GameManager.Instance._StagesDB)
        {
            StageSelector.options.Add(new Dropdown.OptionData(S.name));
        }
        StageSelector.value = 1;
        StageSelector.value = 0;
        
	
	}

    public void CloseInstructionsPanel() {
        InstructionsPanel.SetActive(false);
        GameManager.Instance.stageController.ClosedInstructionsPanel();
    }

    public void StageOverButtonPressed() {

        EndOfRoundPanel.SetActive(false);
        Application.LoadLevel("TitleScreen");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
