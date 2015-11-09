using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
    public GameObject BombParticle;
    public float Timer=5f;

    private float currentTimer;
	// Use this for initialization
	void Awake () {
        currentTimer = Timer;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            GameObject.Instantiate(BombParticle, GetComponent<Transform>().position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
	}
}
