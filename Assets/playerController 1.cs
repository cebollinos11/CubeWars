using UnityEngine;
using System.Collections;


public class playerController : MonoBehaviour {

    public string verticalAxisName;
    public string horizontalAxisName;
    public string jumpKey;
    public GameObject[] objBodies;

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

	}
	
	// Update is called once per frame
	void Update () {
        float vPower,hPower,jPower;
        vPower=Input.GetAxis(verticalAxisName);
        hPower=Input.GetAxis(horizontalAxisName);
        jPower = Input.GetAxis(jumpKey);
        if(hPower!=0||vPower!=0&&!_isJumping)
            MovePlayer(hPower,vPower);
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
    void MovePlayer(float hAxis, float vAxis)
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
        col.gameObject.GetComponent<playerController>().SetTorque();
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
