using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankEffectScript : MonoBehaviour
{
    public float X;
    public float Y;
    
    // Start is called before the first frame update
    void Awake()
    {
        new Vector3(0.05f,0.05f,1);
    }

    // Update is called once per frame
    void Update()
    {
        X += 0.005f;
        Y += 0.005f;
        transform.localScale = new Vector3(X,Y,0);
        //new Vector3(X,Y,0); 
        
        if (X >= 1)
        {
            Destroy(gameObject);
        }
    }
}
