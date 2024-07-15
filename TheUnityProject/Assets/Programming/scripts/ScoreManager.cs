using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float CurrentHighScore;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
        
        
        
    }
    
    
    public bool UpdateHighScore(float currentscore)
    {
        if (currentscore > CurrentHighScore)
        {
            CurrentHighScore = currentscore;
            return true;
        }

        return false;
    }


}
