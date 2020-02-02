using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Joystick joy;

    public GameObject pfbFire;
    public GameObject pfbLand;
    public GameObject pfbWater;
    public GameObject pfbGreens;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPointer();
    }


    void CheckPointer()
    {
        if (joy.Vertical > 0.75f && (joy.Horizontal <= 0.5f || joy.Horizontal >= 0.5f))
        {
            GameObject c = Instantiate(pfbLand);
            c.transform.position = transform.position;
        }

        if (joy.Vertical < 0.5f && (joy.Horizontal <= 0.5f || joy.Horizontal >= 0.5f))
        {
            GameObject c = Instantiate(pfbWater);
            c.transform.position = transform.position;
        }

        if (joy.Horizontal < 0.5f && (joy.Vertical <= 0.5f || joy.Vertical >= 0.5f))
        {
            GameObject c = Instantiate(pfbGreens);
            c.transform.position = transform.position;
        }

        if (joy.Horizontal > 0.5f && (joy.Vertical <= 0.5f || joy.Vertical >= 0.5f))
        {
            GameObject c = Instantiate(pfbFire);
            c.transform.position = transform.position;
        }
    }
}
