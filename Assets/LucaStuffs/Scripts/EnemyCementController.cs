using UnityEngine;
using System.Collections;

public class EnemyCementController : MonoBehaviour {

    GameObject _target;
    public GameObject[] patrolPoints;
    bool _pursue = false;
    private float _speed;
    public float _normalSpeed;
    public float _pushSpeed;
    private Rigidbody _body;
    private int _index;
    


	void Awake () {
        _index = 0;
        _target = patrolPoints[_index];
        _body = GetComponent<Rigidbody>();
        _speed = _normalSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (!_pursue)
        {
            _index++;
            if (_index == patrolPoints.Length - 1)
                _index = 0;
            if(Mathf.Abs(Vector3.Distance(GetComponent<Transform>().position,_target.GetComponent<Transform>().position))<2.0f&&_target.tag=="PatrolPoint")
                _target = patrolPoints[_index];
            else
                if(_target.tag!="PatrolPoint")
                _target = patrolPoints[_index];
        }
        Debug.Log(_target.name);
        _body.velocity = (_target.GetComponent<Transform>().position - GetComponent<Transform>().position)* _speed;
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _target = col.gameObject;
            _pursue = true;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _pursue = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        
            _pursue = false;
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
            _speed = _pushSpeed;
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
            _speed = _normalSpeed;
    }
}
