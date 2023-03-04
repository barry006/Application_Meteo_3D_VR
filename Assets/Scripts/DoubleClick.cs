using System.Collections;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{
    [SerializeField] ClickOnEarth clickOnEarth;
    [SerializeField] API_WebRequest api_WebRequest;
    [SerializeField] RotationEarth rotationEarth;

    float _firstLeftClickTime;
    float _timeBetweenLeftClick = 0.5f;
    bool _isTimeCheckAllowed = true;
    int _leftClickNum = 0;
    bool _onClickingInEarth = false;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition)))
        {
            _onClickingInEarth = true;
            if (Input.GetMouseButtonUp(0))
            {
                _leftClickNum += 1;

            }

            if (_leftClickNum == 1 && _isTimeCheckAllowed)
            {
                _firstLeftClickTime = Time.time;
                StartCoroutine(Retectdoubleclick());
            }
        }
        else
        {
            _onClickingInEarth = false;
        }
    }

    IEnumerator Retectdoubleclick()
    {
        _isTimeCheckAllowed = false;
        while (Time.time < _firstLeftClickTime + _timeBetweenLeftClick)
        {
            if (_leftClickNum == 2)
            {
                if (_onClickingInEarth)
                    QuandDoubleClickJeFais();
                
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        _leftClickNum = 0;
        _isTimeCheckAllowed = true;
    }





    public void QuandDoubleClickJeFais()
    {
        api_WebRequest.Receive(clickOnEarth.LongLat.x.ToString(), clickOnEarth.LongLat.y.ToString());
        rotationEarth.PositionIndicator();

       // tourne();
    }

    public void tourne()
    {
       //clickOnEarth.gameObject.transform.LookAt(Camera.main.transform, Vector3.left);
        //Quaternion rot = Quaternion.FromToRotation(clickOnEarth.RecupHit.point, Vector3.up);
        // clickOnEarth.gameObject.transform.rotation *= rot;
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 hitPoint = ray.origin + ray.direction * clickOnEarth.RecupHit.distance;
        clickOnEarth.gameObject.transform.position = hitPoint;
        clickOnEarth.gameObject.transform.LookAt(Camera.main.transform);
       */
        //Vector3 hitPoint = clickOnEarth.RecupHit.point;
        //clickOnEarth.gameObject.transform.position = hitPoint;

        //  Quaternion rot = Quaternion.FromToRotation(clickOnEarth.RecupHit.point, new(-4.31317138671875f, -1.190826416015625f, 7.1440558433532719f));
        //  clickOnEarth.gameObject.transform.rotation *= rot;
    }
}
