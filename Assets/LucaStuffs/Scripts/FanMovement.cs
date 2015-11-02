using UnityEngine;
using System.Collections;

public class FanMovement : MonoBehaviour {
    public float fanPower;
    public float maxWindPower;
    public float maxDistance;
    private float _maxFanSpeed =100000.0f;
    private Rigidbody _body;
    private Transform _t;
    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _body.maxAngularVelocity = _maxFanSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        _t.Rotate(0.0f, fanPower*Time.deltaTime, 0.0f);
        //_body.AddTorque(new Vector3(fanPower,0.0f,0.0f));
	}
    
    float GetWindPower()
    {
        return _body.angularVelocity.sqrMagnitude;
    }


    float GetPowerToApply(float distance)
    {
        float power,ratio;
        ratio = distance / maxDistance;
        power = Mathf.Lerp(maxWindPower, 0, ratio);
        return power;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Vector3 direction = other.GetComponent<Transform>().position - _t.position;
            float distance = Mathf.Abs(Vector3.Distance(other.GetComponent<Transform>().position, _t.position));
            Vector3 force = direction * GetPowerToApply(distance);
           // Debug.Log(GetPowerToApply(distance));
            other.gameObject.GetComponent<playerControllerV1>().ApplyForce(force);
           
        }
    }
}
