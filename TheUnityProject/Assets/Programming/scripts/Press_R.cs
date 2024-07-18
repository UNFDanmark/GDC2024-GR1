using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Programmering");
            
            
        }
    }

    public void Titelscreen()
    {
        SceneManager.LoadScene("Programmering");
        
    }
    
    public void Programmering()
        {
            SceneManager.LoadScene("Titlescreen");
            
        }
    
}