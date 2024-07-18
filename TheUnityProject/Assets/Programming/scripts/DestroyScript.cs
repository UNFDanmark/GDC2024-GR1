using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        print("oiwhadouwabdiwa");

        if (other.gameObject.CompareTag("Obstacle"))
        {
            print("shiiit");
            Destroy(other.gameObject);
     
        }
    }
}
