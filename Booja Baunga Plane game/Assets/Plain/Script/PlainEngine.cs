using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlainEngine : MonoBehaviour
{
    #region Vars 
    [Header("Plane Stats")]
    public float throttleIncrement = 0.1f;
    public float maxThrottle = 200f;
    public float responsivenss = 10f;
    public float MaxHeight;

    public float throttle;
    public float roll;
    public float pitch;
    public float yaw;
    public float lifs = 135f;

    private Rigidbody rb;
    private MeshCollider Bc;
    [SerializeField] GameObject Plane;
    [SerializeField] Transform MaxX, MinX;
    [SerializeField] GameObject text;
    Vector3 Back;
    #endregion

    #region Unity Function
    private void Awake()
    {
        //StartCoroutine(Warning());
        rb = GetComponent<Rigidbody>();

        if (PlaneParametrs.PleanMat != null)
        {
            Plane.GetComponent<Renderer>().material = PlaneParametrs.PleanMat;
        }
    }
    float times;
    private void Update()
    {

        _Warning();
        Engine();
        Ranges();
        if (!ControlBrake)
        {
            HandleInputs();
        }
    }
    #endregion

    #region Engine Function
    bool ControlBrake;
    bool iswarningMaxX;
    bool iswarningMinX;
    bool iswarningMaxY;
    private float responseModifire
    {
        get
        {
            return (rb.mass / 10f) * responsivenss;
        }
    }
    private void Ranges()
    {
        
        MaxX.transform.position = new Vector3(transform.position.x, transform.position.y, MaxX.transform.position.z);
        MinX.transform.position = new Vector3(transform.position.x, transform.position.y, MinX.transform.position.z);
        if (Vector3.Distance(transform.position,MaxX.position) < 45)
        {
            Debug.Log("carfure you over to zone");
            iswarningMaxX = true;
        }
        else
        {
            iswarningMaxX = false;
        }
        if (Vector3.Distance(transform.position, MaxX.position) < 2)
        {
            destroyPlane();
        }
        if (Vector3.Distance(transform.position, MinX.position) < 45)
        {
            iswarningMinX = true;
            Debug.Log("carfure you over to zone");
        }
        else
        {
            iswarningMaxX = false;
        }
        if (Vector3.Distance(transform.position, MinX.position) < 2)
        {
            destroyPlane();
        }
        if (Vector3.Distance(transform.position, Back) > 10)
        {
            Back = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
            iswarningMaxY = false;
        }
        else
        {
         
            if (Vector3.Distance(transform.position, Back) < 2)
            {
                destroyPlane();
            }
        }
        //Debug.Log(Back);

    }

    void _Warning()
    {
        if (iswarningMaxX || iswarningMaxY|| iswarningMinX)
        {
            if (times <= Time.time)
            {
                StartCoroutine(Warning());

            }
            else
            {
                text.SetActive(true);
            }
        }
        else
        {
            text.SetActive(false);
        }
    }

    IEnumerator Warning()
    {
        text.SetActive(false);
        yield return new WaitForSeconds(1f);
        times = Time.time + 1f;
        StopCoroutine(Warning());
       
    }

    void destroyPlane()
    {
      //  rb.useGravity = true;
        rb.velocity = Vector3.down * 10;
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space)) throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle,0f,60f);
    }
    void Engine()
    {
        if (transform.position.y < MaxHeight)
        {
           
            pitch = Input.GetAxis("Vertical");
        }
        else
        {
            if(transform.rotation.x < 30)
            {
                if (pitch < 1)
                {
                    pitch += 0.1f;
                }
                else
                {
                    pitch = 1;
                }
            }
            else
            {
                if (pitch > 0)
                {
                    pitch -= 0.1f;
                }
                else
                {
                    pitch = 0;
                }
            }
        }
        rb.AddForce(transform.forward * maxThrottle * throttle);
        rb.AddTorque(transform.up * yaw * responseModifire);


        rb.AddTorque(transform.right * pitch * responseModifire);
       // rb.AddForce(Vector3.up * rb.velocity.magnitude * lifs);

        if (roll == 0)
        {
            if (transform.rotation.z > 0)
            {
                transform.Rotate(new Vector3(0, 0, 0));
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



    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    #endregion
}
