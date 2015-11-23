using UnityEngine;
using System.Collections;
using System;


public class SuperInstant : MonoBehaviour {

    public GameObject gm;
    public GameObject guim;

	// Use this for initialization
	void Start () {

        GameObject go = GameObject.Find("GameManager");
        if (go == null)
            InstantStuff();


        GUIManager.Instance.MainMenu.SetActive(true);
	
	}

    void InstantStuff() {

        GameObject go = (GameObject)Instantiate(gm, Vector3.zero, Quaternion.identity);
        go.name = "GameManager";
        GameObject go2 = (GameObject)Instantiate(guim, Vector3.zero, Quaternion.identity);
        go2.name = "GUIManager";



       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
