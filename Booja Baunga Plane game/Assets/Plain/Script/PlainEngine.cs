using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainEngine : MonoBehaviour
{
    #region Vars 
    [Header("Plane Stats")]
    public float throttleIncrement = 0.1f;
    public float maxThrottle = 200f;
    public float responsivenss = 10f;

    public float throttle;
    public float roll;
    public float pitch;
    public float yaw;
    public float lifs = 135f;

    private Rigidbody rb;
    private MeshCollider Bc;
    [SerializeField] GameObject Plane;
    #endregion

    #region Unity Function
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (PlaneParametrs.PleanMat != null)
        {
            Plane.GetComponent<Renderer>().material = PlaneParametrs.PleanMat;
        }
    }
    private void FixedUpdate()
    {
        Engine();
    }

    private void Update()
    {
        if (roll == 0)
        {
            if (transform.rotation.z > 0)
            {
                transform.Rotate(new Vector3(0,0,0));
            }
            else if (transform.rotation.z == 0)
            {
                rb.AddTorque(-transform.forward * 0);
            }
            else
            {
                rb.AddTorque(-transform.forward * -0.05f);
            }
        }
        else
        {
            rb.AddTorque(-transform.forward * roll * responseModifire);
        }
        HandleInputs();
    }
    #endregion

    #region Engine Function

    private float responseModifire
    {
        get
        {
            return (rb.mass / 10f) * responsivenss;
        }
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space)) throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle,0f,100f);
    }
    float MoveSpeed;
    void Engine()
    {
        rb.AddForce(transform.forward * maxThrottle * throttle);
        rb.AddTorque(transform.up * yaw * responseModifire);


        rb.AddTorque(transform.right * pitch * responseModifire);
        rb.AddForce(Vector3.up * rb.velocity.magnitude * lifs);
        

   

        
    }

    #endregion
}
