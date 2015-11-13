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
    private ParticleManager _particleManager;


	void Awake () {
        _index = 0;
        _target = patrolPoints[_index];
        _body = GetComponent<Rigidbody>();
        _speed = _normalSpeed;
         _particleManager = GetComponent<ParticleManager>();
    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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

        _body.velocity = (_target.transform.position - transform.position)* _speed*Time.fixedDeltaTime;
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
        {
            _speed = _pushSpeed;
            col.gameObject.GetComponent<playerControllerV1>().StunByDash();
            _particleManager.playClashParticle(col.contacts[0].point);
        }
           
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
            _speed = _normalSpeed;
    }
}
