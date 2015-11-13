using UnityEngine;
using System.Collections;



public class ControlInverterManager : MonoBehaviour {
    private playerControllerV1[] _playerControllers;
    public float randomizationTimer;

    private float _timer;
    void Awake()
    {
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        _playerControllers =new  playerControllerV1[_players.Length];
        for (int i = 0; i < _players.Length; i++)
            _playerControllers[i] = _players[i].GetComponent<playerControllerV1>();
        _timer = randomizationTimer;
    }
	
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_timer);
        _timer -= Time.deltaTime;
        if(_timer<=0)
        {
            _playerControllers[(int)Random.Range(0, _playerControllers.Length - 1)].InvertControls();
            _timer = randomizationTimer;
        }
	}
}
