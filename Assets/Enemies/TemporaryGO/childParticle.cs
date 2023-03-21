using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childParticle : MonoBehaviour
{
    public string type;
    public float time;

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
            if(type == "DestroyWhenOrphan")
            {
                Destroy(gameObject, time);
            }
            else if(type == "OrphaneAndDieWhenOrphan")
            {
                transform.DetachChildren();
                Destroy(gameObject, time);
            }
        }
    }
}
