using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Vars 
    [SerializeField] Transform povs;
    [SerializeField] float AvarageSpeed;
    float speed;
    float MaxSpeed;

    float distance = 15;

    private int index = 1;
    private Vector3 target;
    #endregion


    #region Unity Function
    private void Start()
    {
        speed = AvarageSpeed;
        MaxSpeed = AvarageSpeed * 2;
    }
    private void Update()
    {
        if (povs != null)
        {
            if (IsPlayerVisible())
            {
               if(distance > 15)
               {
                    distance -= 0.5f;
               }
               else if(distance < 14)
               {
                   distance += 0.5f;
               }

            }
            else
            {
                if(transform.position.x < povs.position.x && distance > 5)
                {
                    distance-= 1f;
                }
                else 
                {
                    distance += 0.5f;
                }
            
            }
       
            TargetMath();
            CameraEngine();
        }
        
    }
    private void FixedUpdate()
    {
        if (povs != null)
        {
            transform.LookAt(new Vector3(povs.position.x + 10, povs.position.y, povs.position.z));
        }
    }

    #endregion

    #region Engine Function

    private void TargetMath()
    {
       
            if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha4)) index = 3;
            target = new Vector3(povs.position.x-distance, povs.position.y + 5, povs.position.z);
        
        
    }

    private void CameraEngine()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //if (Input.GetKey(KeyCode.V)) transform.forward = -povs[index].forward;
        //else transform.forward = povs[index].forward;

    }
    bool IsPlayerVisible()
    {
        if (Time.time > 9)
        {

            // Perform a raycast from the camera towards the player
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (hit.transform.tag == "Player")
                {
                    return true;
                }
            }

            // Player is not visible to the camera
            return false;
        }
        else
        {
            return true;
        }
    }


    #endregion
}
