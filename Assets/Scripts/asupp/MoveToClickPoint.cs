using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour
{
    public Transform Orb;
    public float Radius;

    private Transform pivot;

    void Start()
    {
        pivot = Orb.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * Radius;
    }

    void Update()
    {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(Orb.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = Orb.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}