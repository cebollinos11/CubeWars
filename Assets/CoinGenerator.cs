using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {

    public GameObject ObjectToSpawn;
    public float WaitTime;
	// Use this for initialization
	void Start () {

        Invoke("Spawn", WaitTime);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn() {
        Instantiate(ObjectToSpawn,transform.position,transform.rotation);
        Invoke("Spawn", WaitTime + Random.Range(1, 5));
    }
}
