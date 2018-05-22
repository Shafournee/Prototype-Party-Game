using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton script that holds all the relevant data for a single game.
// Contains all the players in the game, as well as other supporting information.
public class GameManager : Singleton<GameManager> {
	// ----------------------------------- Fields and Properties ----------------------------------- //

	// All the players currently in the game
	public List<PlayerInformation> Players { get; private set; }

    // All the minigames
    [SerializeField] List<string> Scenes;

    



    //  --------- Serialized Fields ---------  //



	// ------------------------------------------ Methods ------------------------------------------ //

	//  --------- Awake ---------  //
	protected override void Awake(){
		base.Awake();
		Players = new List<PlayerInformation>();
        StartGame();


    }


	//  --------- Update ---------  //
	void Update(){
		
	}



	// Starts the game.
	public void StartGame() {
		// Initialize the players
		for(int i = 0; i < 4; i++) {
			PlayerInformation player = new PlayerInformation("Player" + i);
			player.Hand.Add(Card.AllCards["Move 5"]);
			player.Hand.Add(Card.AllCards["Move 5"]);
			player.Hand.Add(Card.AllCards["Move 7"]);
            Players.Add(player);

            if(i == 2)
            {
                player.Hand.Add(Card.AllCards["Move 5"]);
            }


		}

	}

    // Loads a minigame
    public void MiniGameLoader()
    {
        int PickGame = Random.Range(0, Scenes.Count);
        SceneManager.LoadScene(Scenes[PickGame]);

        
    }


    // Return functions for minigames once they've completed.
    public void MinigameReturn(PlayerInformation[] Placements)
    {
        // Load the drafting scene.

        // Start the drafter, giving it the placement order.

        // -- Handle player drafts, order and stuff
    }


}
