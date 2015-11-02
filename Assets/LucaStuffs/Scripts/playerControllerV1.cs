using UnityEngine;
using System.Collections;


public class playerControllerV1 : MonoBehaviour
{

    public string verticalAxisName;
    public string horizontalAxisName;
    public string jumpKey;
    public string dashKey;
    public GameObject[] objBodies;
    public float _jumpPower = 6.0f;
    public float _dashResetTimer = 1.5f;
    public float _dashDurationResetTimer = 0.1f;
    public float dashPower = 50.0f;

    private bool firstTimeTouch=false;
    
    private float hAxisDash, vAxisDash;
    private float _dashTimer;
    private float _dashDurationTimer;
    private bool _dash = false;
    private bool _isJumping;
    
    private float power = 10.0f;
    private Rigidbody[] bodies;
    private Vector3 _forward;
    private Vector3 _right;
    private float _oldDistanceFromMidpoint;
    private bool _canMove = true;
    private bool _pressedJump = false;
    // Use this for initialization
    void Awake()
    {
        bodies = new Rigidbody[objBodies.Length];
        for (int i = 0; i < objBodies.Length; i++)
            bodies[i] = objBodies[i].GetComponent<Rigidbody>();
        _dashTimer = _dashResetTimer;
        _dashDurationTimer = _dashDurationResetTimer;
    }

    // Update is called once per frame
    void Update()
    {

        if (_dashTimer > 0)
            _dashTimer -= Time.deltaTime;
        float vPower, hPower;
        bool jPower, dPower;
        vPower = Input.GetAxis(verticalAxisName);
        hPower = Input.GetAxis(horizontalAxisName);
        jPower = Input.GetButton(jumpKey);
        dPower = Input.GetButton(dashKey);
        if (firstTimeTouch)
        {
            if (jPower)
                _pressedJump = true;
            else
                _pressedJump = false;
            if (dPower && _dashTimer <= 0 && (hPower != 0 || vPower != 0) && !_isJumping)
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
                    else
                        NullVelocity();
                    if (jPower && !_isJumping)
                        Jump();
                }
            }
        }

    }


    void MovePlayer(float hAxis, float vAxis)
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            if (!_isJumping)
                bodies[i].velocity = new Vector3(hAxis * -power, 0.0f, vAxis * -power);
            else

                bodies[i].AddForce(new Vector3(hAxis * -power, 0, vAxis * -power));
        }

    }
    void NullVelocity()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            if (!_isJumping)
                bodies[i].velocity = Vector3.zero;
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
            bodies[i].velocity = new Vector3(0, _jumpPower, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "GamePlane")
        {
            if (!firstTimeTouch)
                firstTimeTouch = true;
            if (firstTimeTouch)
            {
                _isJumping = false;
                _canMove = true;
            }
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
        if (firstTimeTouch)
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
    public void ApplyForce(Vector3 force)
    {
        for (int i = 0; i < bodies.Length; i++)
            bodies[i].AddForce(force);
    }
   



}