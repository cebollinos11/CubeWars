using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player {

    public bool Active;
    public int AccumulatedScore = 0;
    public int CurrentSceneScore = 0;
    public string name = "";
    public Color Color;
    public int id;
    public GameObject playerObject;
    
    
    
    

    public Player(int setId) {
        id = setId;
        name = "Player " + id.ToString();
        Debug.Log("created "+name);        
    }

    

    public void AddPoints(int d) {
        Debug.Log("Added " + d.ToString() + " points to " + name);
        CurrentSceneScore += d;
        
    }

    public void ToggleActivate() {
        
        this.Active = true;
    }
}

public class GameManager : Singleton<GameManager> {
    public int tournamentPoints = 50;
    public GameObject PlayerPrefab;
    public Player[] Players = new Player[4];    
    public Color[] PlayerColors = new Color[4];
    private GameObject PlayerTogglers;
    public stageController stageController;
    
    UnityEngine.UI.Text TestString;

    public List<StageClass> _StagesDB;
    List<StageClass> shuffleBagStages;
    int shuffleBagIndex = 0;
    public StageClass currentStage;

    public Material skybox;

    public GameObject Camera;

	// Use this for initialization
	void Start () {

        Debug.Log("Initializing players array");

        //Init Players array
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player(i+1);
            Players[i].Color = PlayerColors[i];
           
        }	

        //Find TestPlayersPreview

        //TestString = GameObject.Find("TestPreview").GetComponent<Text>();

        shuffleBagStages = new List<StageClass>();
        DuplicateStageDB(); // create a copy of _stagesDB in shufflebag
        Fisher_Yates_CardDeck_Shuffle(shuffleBagStages); //shuffle
        shuffleBagIndex = 0; //init 
        Invoke("StartBGMUSIC", 0.1f);

	}

    void StartBGMUSIC() {
        
        AudioManager.PlayBgSong(2);   
    
    }

    private void DuplicateStageDB()
    {
        foreach (StageClass sc in _StagesDB)
        {
            shuffleBagStages.Add(sc);
        }

    }

    private void PrintList(List<StageClass> list)
    {
        foreach (StageClass sc in list)
        {
            Debug.Log(sc.name);        
        }
    
    }

    public static List<StageClass> Fisher_Yates_CardDeck_Shuffle(List<StageClass> aList)
    {

        System.Random _random = new System.Random();

        StageClass myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }


    public Player GetWinningPlayer(){

        int bestPoint,index=0;
        bestPoint = Players[0].CurrentSceneScore;
        for (int i = 1; i < Players.Length; i++)
        {
            if (Players[i].CurrentSceneScore > bestPoint)
           {
               bestPoint = Players[i].CurrentSceneScore;
               index = i;
           }
        }

        for (int i = 0; i < Players.Length; i++)
        {
            if(i!=index)
                if (Players[i].CurrentSceneScore == bestPoint)
                return null;
        }

        Debug.Log("winner player " + Players[index].name);
        return Players[index];
   
    }

	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P)) {
            Application.LoadLevel("Destroyer");
        }
	}

    public void checkActivatedPlayers() { 
                
    }

    void EnablePlayer(int n) {
        Players[n].ToggleActivate();
    }

    public void EnablePlayerUI(int n) {
        
        EnablePlayer(n);
       
    }

    public void StartNewTournament(){
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel(1);
    }

    public void LoadNewStage() {

        int stageIndex = GUIManager.Instance.StageSelector.value;

        currentStage = _StagesDB[stageIndex];
        string lvlname = _StagesDB[stageIndex].levelname;
        DontDestroyOnLoad(gameObject);
        Debug.Log("trying to load " + lvlname);
        Application.LoadLevel(lvlname);
        GUIManager.Instance.MainMenu.SetActive(false);
        //GUIManager.Instance.InstructionsPanel.SetActive(true);
    }


    void TEST() { Debug.Log("TESTSTA"); }
    public void QueueNextLevel()
    { //method to fix ui bug
        Debug.Log("QUEUE");
        //Time.timeScale = 1.0f;
        Invoke("LoadRandomStage", 0.0f);
    }

    public void LoadRandomStage() {
        Debug.Log("LoadRandom executed");
        //stop all audio
        AudioManager.StopAll();
        //clean ui bugg

        
        //GUIManager.Instance.EndOfRoundPanel.SetActive(false);

        if (shuffleBagIndex > shuffleBagStages.Count-1)
            shuffleBagIndex = 0;

        int stageIndex = shuffleBagIndex;

        shuffleBagIndex++;

        currentStage = shuffleBagStages[stageIndex];
        string lvlname = shuffleBagStages[stageIndex].levelname;
        DontDestroyOnLoad(gameObject);
        Debug.Log("trying to load " + lvlname);
        Application.LoadLevel(lvlname);
        GUIManager.Instance.MainMenu.SetActive(false);
    
    }

}
