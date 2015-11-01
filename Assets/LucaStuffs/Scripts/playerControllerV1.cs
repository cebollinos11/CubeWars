using UnityEngine;
using System.Collections;


public class playerControllerV1 : MonoBehaviour {

    public string verticalAxisName;
    public string horizontalAxisName;
    public string jumpKey;
    public string dashKey;
    public GameObject[] objBodies;


    private float _dashResetTimer=1.5f;
    private float _dashDurationResetTimer=0.2f;
    private float hAxisDash, vAxisDash;
    private float _dashTimer;
    private float _dashDurationTimer;
    private bool _dash=false;
    private bool _isJumping;
    private float _jumpPower = 6.0f;
    private float power= 10.0f;
    private float dashPower=50.0f;
    private Rigidbody[] bodies;
    private Vector3 _forward;
    private Vector3 _right;
    private float _oldDistanceFromMidpoint;
    private bool _canMove = true;
    private bool _pressedJump = false;
    // Use this for initialization
	void Awake () {
        bodies =new Rigidbody[objBodies.Length];
        for (int i = 0; i < objBodies.Length; i++)
            bodies[i] = objBodies[i].GetComponent<Rigidbody>();
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
        if (jPower > 0)
            _pressedJump = true;
        else
            _pressedJump = false;
        if (dPower > 0 && _dashTimer <= 0&&(hPower!=0||vPower!=0)&&!_isJumping)
        {
            _dash = true;
            hAxisDash = hPower;
            vAxisDash = vPower;
        }

        if (_dash)
        {
            Dash();
            _dashDurationTimer -= Time.deltaTime;
            if (_dashDurationTimer <= 0)
            {
                _dash = false;
                _dashDurationTimer = _dashDurationResetTimer;
                _dashTimer = _dashResetTimer;
            }
        }
        else
        {
            if (_canMove)
            {
                if (hPower != 0 || vPower != 0 && !_isJumping)
                    MovePlayer(hPower, vPower);
                if (jPower > 0 && !_isJumping)
                    Jump();
            }
        }
        

	}

   
    void MovePlayer(float hAxis, float vAxis)
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            if(!_isJumping)
            bodies[i].velocity = new Vector3(hAxis * -power, 0.0f,vAxis * -power);
          else
            
            bodies[i].AddForce(new Vector3(hAxis * -power, 0, vAxis * -power));
        }
            
    }
   /* void MovePlayer(float hAxis,float vAxis,float dPower)
    {
        transform.Translate(new Vector3(-hAxis * Time.deltaTime* speed, 0.0f, -vAxis * Time.deltaTime* speed));
    }*/

    void Dash()
    {

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].velocity = new Vector3(hAxisDash * -dashPower, 0.0f, vAxisDash * -dashPower);


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
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "GamePlane")
        {
            _isJumping = false;
            _canMove = true;
        }
       

    }
    bool isDashing()
    {
        return _dash;
    }
    void OnCollisionStay(Collision col)
    {
        
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "GamePlane" && _pressedJump)
        {
            _isJumping = true;
        }
        else
            if (col.gameObject.tag == "GamePlane" && !_pressedJump)
            _canMove = false;
            
        
            
    }

}
