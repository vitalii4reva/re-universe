using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickVFX : MonoBehaviour
{
    public Joystick joy;
    public Image green;
    public Image water;
    public Image earth;
    public Image hot;

    private void Update()
    {
        Cosmetics();
    }

    void Cosmetics()
    {
        if(joy.Direction.magnitude<=0.35f)
        {
            green.enabled = false;
            water.enabled = false;
            earth.enabled = false;
            hot.enabled = false;
        }

        else if (joy.Vertical > 0.5f && (joy.Horizontal < 0.5f && joy.Horizontal > -0.5f))
        {
            green.enabled = true;
            water.enabled = false;
            earth.enabled = false;
            hot.enabled = false;
        }
        else if(joy.Vertical < -0.5f && (joy.Horizontal < 0.5f && joy.Horizontal > -0.5f))
        {
            green.enabled = false;
            water.enabled = false;
            earth.enabled = false;
            hot.enabled = true;
        }
        else if (joy.Horizontal >0.5f && (joy.Vertical < 0.5f && joy.Vertical > -0.5f))
        {
            green.enabled = false;
            water.enabled = false;
            earth.enabled = true;
            hot.enabled = false;
        }
        else if (joy.Horizontal < -0.5f && (joy.Vertical < 0.5f && joy.Vertical > -0.5f))
        {
            green.enabled = false;
            water.enabled = true;
            earth.enabled = false;
            hot.enabled = false;
        }
    }

}
