using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton script that holds all the relevant data for a single game.
// Contains all the players in the game, as well as other supporting information.
public class GameManager : Singleton<GameManager> {
	// ----------------------------------- Fields and Properties ----------------------------------- //

	// All the players currently in the game
	public List<PlayerInformation> Players { get; private set; }



	//  --------- Serialized Fields ---------  //



	// ------------------------------------------ Methods ------------------------------------------ //

	//  --------- Awake ---------  //
	protected override void Awake(){
		base.Awake();
		Players = new List<PlayerInformation>();


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



		}

	}
}
