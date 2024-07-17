using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class mousehover : MonoBehaviour

{
    public GameObject hoverbotten;
    
    // Start is called before the first frame update
    void Start()
    {
        hoverbotten.SetActive(false);
    }

    public void OnMouseEnter()
    {
        hoverbotten.SetActive(true);
    }
    public void OnMouseExit()
    {
        hoverbotten.SetActive(false);
    }
}
