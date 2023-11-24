using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
      
        //if (!IsPointerOverUIObject())
        //{
            Vector3 Direction = PreviousPosition - _camera.ScreenToViewportPoint(Input.mousePosition);
            _camera.transform.position = TargetPos.transform.position;

            //_camera.transform.Rotate(new Vector3(1, 0, 0), angle: Direction.y * 180);
            _camera.transform.Rotate(new Vector3(0, 1, 0), angle: -Direction.x * 180, Space.World);
            _camera.transform.Translate(new Vector3(0, rotateY, -rotate));
            PreviousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        //}
    }

    // Check if the mouse pointer is over a UI element
    bool IsPointerOverUIObject()
    {
        // Check for the existence of the EventSystem
        if (EventSystem.current == null)
        {
            return false;
        }

        // Create a pointer event data with the current mouse position
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;

        // Use the EventSystem to check if the pointer is over a UI element
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Check if any of the results belong to a UI element
        return results.Count > 0;
    }
    #endregion
}
