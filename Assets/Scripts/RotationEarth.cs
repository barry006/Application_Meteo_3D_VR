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
    public ClickOnEarth clickOnEarth;

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
    public void PositionIndicator()
    {
        
        clickOnEarth.Indicator.transform.position = clickOnEarth.RecupHit.point;
        
        
        
    }
}
