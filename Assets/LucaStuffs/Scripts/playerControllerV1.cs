using UnityEngine;
using System.Collections;


public class playerControllerV1 : MonoBehaviour {

    public string verticalAxisName;
    public string horizontalAxisName;
    public string jumpKey;
    public string dashKey;
    public GameObject[] objBodies;


    private float speed=10.0f;
    private float dashSpeed=50.0f;
    private float _dashResetTimer=1.5f;
    private float _dashDurationResetTimer=0.3f;
    private float hAxisDash, vAxisDash;
    private float _dashTimer;
    private float _dashDurationTimer;
    private bool _dash=false;
    private bool _addTorque = false;
    private float _torqueTimerReset = 0.5f;
    private float _torqueTimer;
    private float targetVelocity = 30.0f;
    private bool _isJumping;
    private float _jumpPower = 6.0f;
    private float _maxForce = 100.0f;
    private bool _canJump=false;
    private Rigidbody[] bodies;
    private Vector3 _forward;
    private Vector3 _right;
    private Rigidbody _playerRigidBody;
    private float _oldDistanceFromMidpoint;
   
    // Use this for initialization
	void Awake () {
        bodies =new Rigidbody[objBodies.Length];
        for (int i = 0; i < objBodies.Length; i++)
            bodies[i] = objBodies[i].GetComponent<Rigidbody>();
        _playerRigidBody = GetComponent<Rigidbody>();
        _dashTimer = _dashResetTimer;
        _dashDurationTimer = _dashDurationResetTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if(_dashTimer>0)
        _dashTimer -= Time.deltaTime;
        float vPower,hPower,jPower,dPower;
        vPower=Input.GetAxis(verticalAxisName);
        hPower=Input.GetAxis(horizontalAxisName);
        jPower = Input.GetAxis(jumpKey);
        dPower = Input.GetAxis(dashKey);
        if (dPower > 0 && _dashTimer <= 0&&(hPower!=0||vPower!=0))
        {
            _dash = true;
            hAxisDash = hPower;
            vAxisDash = vPower;
        }
            
        if (_dash)
        {
            Dash();
            _dashDurationTimer -= Time.deltaTime;
            if(_dashDurationTimer<=0)
            {
                _dash = false;
                _dashDurationTimer = _dashDurationResetTimer;
                _dashTimer = _dashResetTimer;
            }
        }
        else
        if(hPower!=0||vPower!=0&&!_isJumping)
            MovePlayer(hPower,vPower,dPower);
        if (jPower > 0&&!_isJumping)
            Jump();
        /*if(!RendererExtensions.IsVisibleFrom(GetComponent<Renderer>(),Camera.main))
        {
            _cameraMovement.SetVisible(this.gameObject,false);
        }else
            _cameraMovement.SetVisible(this.gameObject, true);*/

	}

    void FixedUpdate()
    {
        if (_addTorque)
        {
            _torqueTimer -= Time.fixedDeltaTime;
            if (_torqueTimer <= 0)
                _addTorque = false;
            for (int i = 0; i < bodies.Length; i++)
                bodies[i].AddTorque(transform.up*10000000f,ForceMode.VelocityChange); 
        }
           
    }
    //Function that moves the player based on the axis
  /*  void MovePlayer(float hAxis, float vAxis)
    {
        float power,ratio,v;

        for (int i = 0; i < bodies.Length; i++)
        {
            v = bodies[i].velocity.magnitude;
            if (v <= 0.01f)
                v = 0.01f;
            ratio = targetVelocity / v;
            ratio = ratio / _maxForce;
            
            power = Mathf.Lerp(0.0f, _maxForce, ratio);
            bodies[i].AddForce(Vector3.forward*vAxis*-power);
            bodies[i].AddForce(Vector3.right * hAxis * -power);

           // bodies[i].AddForce(new Vector3(hAxis * -power, 0, vAxis * -power));
        }
            
    }*/
    void MovePlayer(float hAxis,float vAxis,float dPower)
    {
        transform.Translate(new Vector3(-hAxis * Time.fixedDeltaTime* speed, 0.0f, -vAxis * Time.fixedDeltaTime* speed));
    }

    void Dash()
    {
        transform.Translate(new Vector3(-hAxisDash * Time.fixedDeltaTime * dashSpeed, 0.0f, -vAxisDash * Time.fixedDeltaTime * dashSpeed));
    }


    void Jump()
    {
        for (int i = 0; i < bodies.Length; i++)
            bodies[i].velocity = new Vector3(0, _jumpPower,0);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "GamePlane")
        _isJumping = false;

    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        col.gameObject.GetComponent<playerControllerV1>().SetTorque();
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "GamePlane")
            _isJumping = true;
        
            
    }

    public void SetTorque()
    {
        _torqueTimer = _torqueTimerReset;
        _addTorque = true;
    }
}
