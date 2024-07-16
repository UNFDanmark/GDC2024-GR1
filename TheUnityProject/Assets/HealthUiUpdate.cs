using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUiUpdate : MonoBehaviour
{

    private PlayerMovement pM;
    public TextMeshProUGUI tm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pM = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        tm = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pM.hasbeenattacked)
        {
            tm.text = "Health " + pM.currenthealth + "/" + pM.MaxHealth;
            pM.hasbeenattacked = false;
        }
    }
}