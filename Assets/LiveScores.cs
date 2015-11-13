using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LiveScores : MonoBehaviour {

    public GameObject[] Counter;
    public Text TimeCounter;
    public int TimeCounterValue;
	// Use this for initialization
	void Start () {

        //assign Counter color to player color
        int i = 0;
        foreach(Player P in GameObject.Find("GameManager").GetComponent<GameManager>().Players){
            Color Temp = P.Color;
            Temp.a = 1f;
            Debug.Log("Setting " + Counter[i].GetComponent<Text>().color + " to " + Temp);
            Counter[i].GetComponent<Text>().color = Temp;            
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //TimeCounter.text = 
        	
	}

    public void SetTimeCounter(int i) {
        TimeCounter.text = i.ToString();
    }

    public void UpdateCounters() {

        int i = 0;
        foreach (Player P in GameObject.Find("GameManager").GetComponent<GameManager>().Players)
        {            
            Counter[i].GetComponent<Text>().text = P.CurrentSceneScore.ToString();
            i++;
        }
        
    }
}
