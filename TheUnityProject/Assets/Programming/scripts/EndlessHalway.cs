using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndlessHalway : MonoBehaviour
{
    public GameObject[] section;
    public int xPos = -1200;
    public bool creatingSection = false;
    public int sectionNumber;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(creatingSection == false)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
            
        }

      
        
    }
     IEnumerator GenerateSection()
    {
        sectionNumber = Random.Range(0, 4);
        Instantiate(section[sectionNumber], new Vector3(xPos, 0, 0), Quaternion.identity);
        xPos += -300;
        yield return new WaitForSeconds(5);
        creatingSection = false;
    }

   
}
