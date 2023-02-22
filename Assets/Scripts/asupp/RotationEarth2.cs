using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEarth2 : MonoBehaviour
{
    public bool b;
    public bool hasGrabbedPoint;
    Vector3 grabbedPoint;
    Quaternion saveRotEarth;




	void Update()
	{

		if (Input.GetMouseButton(0))
		{
			if (!hasGrabbedPoint)
			{
				hasGrabbedPoint = true;
				grabbedPoint = GetTouchedPoint();
			}
			else
			{
				Vector3 targetPoint = GetTouchedPoint(); Quaternion rot = Quaternion.FromToRotation(grabbedPoint, targetPoint); transform.localRotation *= rot;
			}
		}
		else hasGrabbedPoint = false;


		/*
		      if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition)))
        {
            transform.rotation = saveRotEarth;
            hasGrabbedPoint = false;
            Input.ResetInputAxes();
        }*/
	}




	Vector3 GetTouchedPoint()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit rh);
        return transform.InverseTransformPoint(rh.point);
    }

   

}
