using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    private PlayerMovement pm;
    
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        print("obs");
        if (other.gameObject.CompareTag("Player"))
        {
            print("av2");
            if (pm.currentattackedCooldown <= 0f)
            {
                pm.currenthealth--;
                pm.currentattackedCooldown = pm.attackedCooldown;
                pm.hasbeenattacked = true;
            }
            
            
        } 
    }
    
}
