using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Vars 
    [SerializeField] Transform[] povs;
    [SerializeField] float speed;

    private int index = 1;
    private Vector3 target;
    #endregion


    #region Unity Function
    private void Update()
    {
        TargetMath();
    }
    private void FixedUpdate()
    {
        CameraEngine();
    }

    #endregion

    #region Engine Function

    private void TargetMath()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) index = 3;

        target = povs[index].position;
    }

    private void CameraEngine()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.forward = povs[index].forward;
    }

    #endregion
}
