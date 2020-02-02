using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.

public class BlastManager : MonoBehaviour
{
    public enum BlastMode
    {
        Green, Water, Earth, Hot, None
    }
    public PlanetManager planetManager;
    public Joystick joy;
    public BlastMode blastMode;
    public bool released;
    public bool loading;
    public Transform blastParent;
    public GameObject greenBlast;
    public GameObject waterBlast;
    public GameObject earthBlast;
    public GameObject hotBlast;
    public GameObject blast;
    
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
        Debug.Log("Direction Magnitude " + joy.Direction.magnitude);
        if (joy.Direction.magnitude == 0f && !released && loading && blastMode!=BlastMode.None)
        {

            planetManager.AddStatsBtnEnd();
            released = true;
            loading = false;
            blast = null;
            blastMode = BlastMode.None;
        }
        else if (joy.Direction.magnitude > 0.001f && !loading && blastMode==BlastMode.None)
        {
            Debug.Log("We're here 1");
            released = false;
            if (joy.Vertical > 0.5f && (joy.Horizontal < 0.5f && joy.Horizontal > -0.5f))
            {
                blastMode = BlastMode.Green;
                blast = greenBlast;
            }
            else if (joy.Vertical < -0.5f && (joy.Horizontal < 0.5f && joy.Horizontal > -0.5f))
            {
                blastMode = BlastMode.Hot;
                blast = hotBlast;
            }
            else if (joy.Horizontal > 0.5f && (joy.Vertical < 0.5f && joy.Vertical > -0.5f))
            {
                blastMode = BlastMode.Earth;
                blast = earthBlast;
            }
            else if (joy.Horizontal < -0.5f && (joy.Vertical < 0.5f && joy.Vertical > -0.5f))
            {
                blastMode = BlastMode.Water;
                blast = waterBlast;
            }
            
            if(blastMode != BlastMode.None)
            {
                Debug.Log("We're here");
                loading = true;
                GameObject temp = Instantiate(blast, blastParent);
                planetManager.AddStatsBtnStart(temp);
            }
        }
        
    
    }
}
