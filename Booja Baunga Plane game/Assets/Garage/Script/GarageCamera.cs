using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCamera : MonoBehaviour
{
    #region Vars 
    [SerializeField] private Camera _camera;
    private Vector3 PreviousPosition;
    [SerializeField] GameObject TargetPos;
    [SerializeField] GameObject Garage;

    public float rotate;
    public float rotateY;
    
    public float SpeedScrol;

    #endregion

    #region Unity Function
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if (Garage.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PreviousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                CameraRotation();
            }
        }

    }
    private void Update()
    {
 
    }
    
    #endregion

    #region Engine Function
    void CameraRotation()
    {
        Vector3 Direction = PreviousPosition - _camera.ScreenToViewportPoint(Input.mousePosition);
        _camera.transform.position = TargetPos.transform.position;

        //_camera.transform.Rotate(new Vector3(1, 0, 0), angle: Direction.y * 180);
        _camera.transform.Rotate(new Vector3(0, 1, 0), angle: -Direction.x * 180,Space.World);
        _camera.transform.Translate(new Vector3(0, rotateY, -rotate));
        PreviousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
    }
    #endregion
}
