using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stageController : Singleton<stageController> {

    private GameObject instructionsPanel;
    private StageManager2 SM;
    private string stageStatus = "";

    //time related
    public float stageLength;
    private float remainingTime;

    private bool stageFinished;

    void PauseGame() {
        Time.timeScale = 0;
        //GameManager.Instance.
        GameManager.Instance.stageController = this;
    }

    void UnPauseGame() {
        Time.timeScale = 1;
        Debug.Log("unpausing");
    }

	// Use this for initialization
	void Start () {

        remainingTime = stageLength;
        StartStage();

        //set the skybox
        RenderSettings.skybox = GameManager.Instance.skybox;
        
	}

    void StartStage(){

        instructionsPanel = GUIManager.Instance.InstructionsPanel;
        SM = GetComponent<StageManager2>();
        StartGeneration();
        //GUIManager.Instance.StageSelector.gameObject.SetActive(true);
    
    }
    void StartGeneration() {
        SM.BuildStage();
        stageStatus = "showing_instructions";
        GUIManager.Instance.InstructionsPanel.SetActive(true);
        PauseGame();
        Debug.Log("setting stagestatus to - " + stageStatus);
        ShowInstructions();
        GUIManager.Instance.ShowLiveScores();
    }
    void ShowInstructions() {

        instructionsPanel.transform.FindChild("StageName").GetComponent<Text>().text = GameManager.Instance.currentStage.name;
        instructionsPanel.transform.FindChild("InstructionText").GetComponent<Text>().text = GameManager.Instance.currentStage.description;
        //instructionsPanel.transform.FindChild("StartButton").GetComponent<Button>().onClick.AddListener(()=> PlayerClickedOnInstructionsButton());
        instructionsPanel.gameObject.SetActive(true);

    }

    void HideInstructions() {
        instructionsPanel.gameObject.SetActive(false);

    }
	// Update is called once per frame

  
	
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            StageIsOver();
            //GUIContinue();
        }	

        //decrease time of stage
        remainingTime -= Time.deltaTime;

        GUIManager.Instance.LiveScoresScript.SetTimeCounter((int)remainingTime);

        //Debug.Log(remainingTime);
        if (remainingTime < 0.0f && !stageFinished) { //set this back to zero!!
            stageFinished = true;
            StageIsOver();
            
        }

	}

    public void ClosedInstructionsPanel() {        
        UnPauseGame();
    }

    void StageIsOver() {

        PauseGame();
        Debug.Log("ACTIVATING ENDOFROUNDPANEL");
        GUIManager.Instance.EndOfRoundPanel.SetActive(true);

        //var S = GUIManager.Instance.EndOfRoundPanel.transform.Find("ScoreText").gameObject.GetComponent<Text>();
        //S.text = "";
        Text S = GUIManager.Instance.EndOfRoundScoreText;
        S.text = "";

        foreach (Player P in GameManager.Instance.Players)
        {

            int NewAccumulatedScore = P.AccumulatedScore + P.CurrentSceneScore;
            S.text += P.name + ":\t" + P.AccumulatedScore.ToString() + "\t+\t" + P.CurrentSceneScore.ToString() + "\t=\t" + NewAccumulatedScore.ToString();
            P.AccumulatedScore = NewAccumulatedScore;
            P.CurrentSceneScore = 0;
            S.text += "\n";
        }

        GUIManager.Instance.UpdateLiveScores();
    
    }

    
    

}
