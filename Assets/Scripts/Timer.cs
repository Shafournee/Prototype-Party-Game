using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    
    public float time;
    //Change this bool to true once you want to start the timer
    public bool startTimer = true;
    public Text timerUI;
    [SerializeField] bool decimals;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        StartTimer();
        DrawTimerToScreen();
	}

    public void StartTimer()
    {
        if(startTimer)
        {
            time -= Time.deltaTime;
        }
    }

    void DrawTimerToScreen()
    {
        if(decimals)
        {
            timerUI.text = time.ToString("0.##");
        }
        else
        {
            timerUI.text = time.ToString();
        }
    }

}
