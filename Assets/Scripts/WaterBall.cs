using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0.02f, 0);
    }

    private void OnCollisionEnter(Collision coll)
    {
        print(2);
        if (coll.gameObject.name == "Sphere")
        {
            print(1);
            Destroy(gameObject);
        }
    }
}
