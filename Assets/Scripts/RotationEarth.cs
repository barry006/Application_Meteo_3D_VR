using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationEarth : MonoBehaviour
{

    public bool ClickingOnEarth;
    public bool BoolSaveRotEarth;
    Vector3 _grabbedPoint;
    Quaternion _saveRotEarth;

    private void Update()
    {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition)))
        {
            if (ClickingOnEarth)                                                    // for cancel ResetInputAxes, and use Onclick.
            {
                Input.ResetInputAxes();                                             // For cancel OnMouseDrag because if mouse is down problem with bool. /!\ Is very dangerous : cancel all event.
            }
            if (BoolSaveRotEarth)
            {
                transform.rotation = _saveRotEarth;
                ClickingOnEarth = false;
                BoolSaveRotEarth = false;
            }
        }
    }

    Vector3 GetTouchedPoint()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit rh);
        return transform.InverseTransformPoint(rh.point);
    }

    private void OnMouseDrag()
    {
        _saveRotEarth = transform.rotation;
        if (!ClickingOnEarth)
        {
            BoolSaveRotEarth = true;
            ClickingOnEarth = true;
            _grabbedPoint = GetTouchedPoint();
        }
        else
        {
            Vector3 targetPoint = GetTouchedPoint();
            Quaternion rot = Quaternion.FromToRotation(_grabbedPoint, targetPoint);
            transform.localRotation *= rot;
        }
    }

    private void OnMouseUp()
    {
        ClickingOnEarth = false;
    }










    /*
        private void OnMouseEnter()
        {
            b = true;
        }
        private void OnMouseExit()
        {
            b = false;
        }
    */

    //transform.RotateAround(transform.position, new(0f, 1f, 0f), -Input.GetAxis("Mouse X") * rotationSpeed);
    //transform.RotateAround(transform.position, new(1f, 0f, 0f), Input.GetAxis("Mouse Y") * rotationSpeed);

    /*
    public void OnGUI()
    {
        if (Event.current.type == EventType.MouseDrag)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out oldHitPoint))
            {
                rotate(Event.current.delta);
            }
        }
    }
    public void rotate(Vector2 mCurrentPos)
    {
        transform.rotation = Quaternion.Euler(-mCurrentPos.y, -mCurrentPos.x, 0) *  transform.rotation;
    }
    */

}
