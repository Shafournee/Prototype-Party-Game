using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data Container that contains all the information about 1 player.
// Includes things like: cards in hand, number of coins/stars/victory points, any other misc. stats that is important for determining success.
public class PlayerInformation {
	// ----------------------------------- Fields and Properties ----------------------------------- //

	// Name and other info.
	public readonly string Name;
	
	// The cards that the player currently has in their hand.
	public List<Card> Hand { get; private set; }



	// ------------------------------------------ Methods ------------------------------------------ //
	
	// Constructor
	public PlayerInformation(string name) {
		Name = name;

		// Initialize the player's hand to some starter hand
		Hand = new List<Card>();



	}




}
