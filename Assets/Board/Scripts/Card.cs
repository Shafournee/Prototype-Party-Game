using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Delegate for the execution of card effects (what happens when a card is played)
public delegate void CardEffect(Card card, PlayerInformation user);


// Contains information relating to a specific card in the game.
public class Card {
	// ----------------------------------- Fields and Properties ----------------------------------- //

	// Name and description
	public readonly string Name;
	public readonly string Description;

	// Numerical value used for effects.
	// For example, move cards would use this value to determine the number of steps to move.
	// Other kinds of cards might use this value differently, or possibly not at all.
	public readonly int Value;

	// The effect this card has when played.
	public CardEffect OnPlay { get; private set; }



	// ------------------------------------------ Methods ------------------------------------------ //

	// Constructor
	Card(string name, string desc, int value, CardEffect effect){
		Name = name;
		Description = desc;
		Value = value;
		OnPlay = effect;
	}


    // TODO: Make this functionality through scriptable objects
	// Static list of every kind of card in the game.
	public static Dictionary<string, Card> AllCards = new Dictionary<string, Card>(){
		// Move 5 card
		{ "Move 5", new Card("Move 5", "Moves u 5 spaces", 5, AllCardEffects.BasicMove) },

		// Move 7 card
		{ "Move 7", new Card("Move 7", "Moves u 7 spaces", 7, AllCardEffects.BasicMove) }

		// ...

	};

}

// TODO: Try to make this function through a scriptable object?
// Dictionary containing all the possible card effects.
public static class AllCardEffects {
	// Basic movement effect.
	public static void BasicMove(Card card, PlayerInformation user) {
		// move the player
        

	}




}
