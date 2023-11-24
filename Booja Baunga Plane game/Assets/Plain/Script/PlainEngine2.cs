using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlainEngine2 : MonoBehaviour
{
    public static PlainEngine2 Instance;
    public float flySpeedForward = 5;
    float flySpeed;
    
    private float Yaw;
    public float yawAmount = 120;

    public GameObject Plane;
    [SerializeField] GameObject text;
    [SerializeField] float maxY;

    float Pitch;
    float roll;
    float VerticalInput;
    float HorizontalInput;
    int obstacle;
    float Backwalk;
    public int interactcolider = 0;


    public float ObstacleTime;


    private void Start()
    {
        Instance = this;
        if (PlaneParametrs.PleanMat != null)
        {
            Plane.GetComponent<Renderer>().material = PlaneParametrs.PleanMat;
        }
        Yaw = 90f;
        Backwalk = flySpeedForward + flySpeedForward/2;
    }

    private void Update()
    {
       
        if (Vector3.Distance(transform.position, GameEngine.Instance.BackObj.transform.position) > 20 || obstacle == -1)
        {
            GameEngine.Instance.BackObj.transform.position = new Vector3(transform.position.x - 20, transform.position.y, transform.position.z);
        }
        Engine();

        if(transform.position.y > maxY)
        {
            VerticalInput += 0.04f;
        }
        else
        {
           
            if (ObstacleTime >= Time.time)
            {
                obstacle = -1;
                flySpeed = Backwalk;
                HorizontalInput = 0;
                VerticalInput = 0;
            }
            else
            {
                obstacle = 1;
                flySpeed = flySpeedForward;
                HorizontalInput = Input.GetAxis("Horizontal");
                OnClickKey();
            }
        }

      

       
    }
   
    private void Engine()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime * obstacle;

        
        Yaw += HorizontalInput * yawAmount * Time.deltaTime;


        Pitch = Mathf.Lerp(0, 90, Mathf.Abs(VerticalInput)) * Mathf.Sign(VerticalInput) * 0.5f;

        roll = Mathf.Lerp(0, 30, Mathf.Abs(HorizontalInput)) * -Mathf.Sign(HorizontalInput);
        

        transform.localRotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * Pitch + Vector3.forward * roll);
    }

    void OnClickKey()
    {
        if (VerticalInput < 1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                VerticalInput += 0.04f;
            }
        }
        if(VerticalInput > -1) 
        {
            if (Input.GetKey(KeyCode.S))
            {
                VerticalInput -= 0.04f;
            }
        }
 
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Terrine":
                Destroy(gameObject);
                GameEngine.Instance.EndGame = true;
                break;
            case "Point":
                GameEngine.Instance.CoinAmount--;
                Destroy(other.gameObject);
                break;
            case "EndColider":
                GameEngine.Instance.EndGame = true;
                break;
            case "Warning":
                if (interactcolider == 0)
                {
                    Debug.Log("gacdi");
                    GameEngine.Instance.Warning = true;
                }
                else
                {
                    Debug.Log("gamoxvedi");
                    GameEngine.Instance.Warning = false;
                }
                break;
            case "Border":
                GameEngine.Instance.Destroed = true;
                GameEngine.Instance.EndGame = true;
                Destroy(gameObject);
                break;
            case "Tree":
                if (ObstacleTime <= Time.time + 1)
                {

                    ObstacleTime = Time.time + 1f;
                }
                break;
        }

    }



}
