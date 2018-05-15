using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyGameManager : MonoBehaviour {

    //This gives you access to the timer
    GameObject Canvas;

    List<GameObject> players;

	// Use this for initialization
	void Start () {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
        IsGameFinishedTimer();

    }

    //Must be checked on update
    //End the game if the timer reaches zero
    private void IsGameFinishedTimer()
    {
        if(Canvas.GetComponent<Timer>().time <= 0)
        {
            EndGame();
        }
    }

    //This function checks if the game is over by removing a player from the list
    //and then checking if there's only one player left, in which case they win.
    public void IsGameFinishedPlayerDeath(GameObject deadPlayer)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if(players[i] == deadPlayer)
            {
                players.RemoveAt(i);
            }
        }
        if (players.Count == 1)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        //TODO MAKE THIS ACTUALLY STOP THE PENGUINS FROM MOVING
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
        for (int i = 0; i < players.Count; i++)
        {
            //Make each finishing player unable to move
            players[i].GetComponent<PushyPlayer>().playerCanMove = false;
        }
    }
}
