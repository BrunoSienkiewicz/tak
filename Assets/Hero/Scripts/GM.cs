using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {}

    // Update is called once per frame
   void LateUpdate()
    {
        GI.updateGI();
        //if(GI.horizontal!=0) Debug.Log(GI.horizontal);
    }
}
