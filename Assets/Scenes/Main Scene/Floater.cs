using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    private Vector3 Startposition;
    void Start()
    {
        Startposition = transform.position;
    }
    void Update()
    {
        transform.position = Startposition + new Vector3(0, Mathf.Sin(Time.time)/2, 0);
    }
}
