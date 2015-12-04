using UnityEngine;
using System.Collections;

public class PointsManager : MonoBehaviour {

    public int i;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GivePoints(int n) {

        GameManager.Instance.Players[i].CurrentSceneScore += n;
        if (GameManager.Instance.Players[i].CurrentSceneScore < 0)
        {
            GameManager.Instance.Players[i].CurrentSceneScore = 0;
        }
        GUIManager.Instance.LiveScores.GetComponent<LiveScores>().UpdateCounters();
    
    }
}
