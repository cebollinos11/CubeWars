using UnityEngine;
using System.Collections;



public class RandomEffectGenerator : MonoBehaviour
{
    private playerControllerV1[] _playerControllers;
    public float randomizationTimer;

    private float _timer;
    void Awake()
    {
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        _playerControllers = new playerControllerV1[_players.Length];
        for (int i = 0; i < _players.Length; i++)
            _playerControllers[i] = _players[i].GetComponent<playerControllerV1>();
        _timer = randomizationTimer;
    }


    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            int index;
            if (_playerControllers.Length > 1)
                index = (int)Random.Range(0, (_playerControllers.Length - 1) * 1000) % (_playerControllers.Length);
            else
                index = (int)Random.Range(0, (_playerControllers.Length - 1) * 1000);
            switch ((int)Random.Range(0,1000)%3)
            {
                case 0:
                    _playerControllers[index].InvertControls();
                    break;
                case 1:
                    _playerControllers[index].InvertDashJump();
                    break;
                case 2:
                    _playerControllers[index].ScaleIt(Random.Range(0.5f,3f));
                    break;
            }
           
            _timer = randomizationTimer;
        }
    }
}
