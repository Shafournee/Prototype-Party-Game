using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnManager : MonoBehaviour {

    // A copy of the player's list.
    public List<PlayerInformation> Players;

    // The card prefab
    [SerializeField] GameObject CardPrefab;

    // The Player's Hand
    [SerializeField] GameObject PlayerHand;

    // Current Player
    PlayerInformation CurrentPlayer = null;


    // Use this for initialization
    void Start () {
        StartRound();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Gets called at the start of a new round (when a minigame finishes)
    public void StartRound()
    {
        // Cycle players in GameManagers players list.
        PlayerInformation TempPlayer = GameManager.Instance.Players[0];
        GameManager.Instance.Players.RemoveAt(0);
        GameManager.Instance.Players.Add(TempPlayer);

        // Copy player list
        Players = GameManager.Instance.Players;

        // Display the first player's hand
        EndPlayerTurn();


    }

    // This function ends the turn for the current player
    public void EndPlayerTurn()
    {
        // If every player hasn't taken their turn yet
        //  - Disable interactions with all the cards
        //  - swap current hand with new player's hand
        //  - pop the current player off list
        //  - Reenable interactibility

        // This tells the Eventsystem which card to highlight
        GameObject FirstCard = null;

        EventSystem.current.SetSelectedGameObject(null);

        if (Players.Count != 0)
        {
            // Destroy each card the current player is holding
            foreach (Transform transform in PlayerHand.transform)
            {
                Destroy(transform.gameObject);
            }

            // Set the current player to the player in position zero, and remove them from the list
            CurrentPlayer = Players[0];
            Players.RemoveAt(0);

            for (int i = 0; i < CurrentPlayer.Hand.Count; i++)
            {
                // Create the card, set it as a child of the player hand. This handles spacing on its own
                GameObject Card = Instantiate(CardPrefab, PlayerHand.transform);

                //Set the first card to the first card created
                if (i == 0)
                    FirstCard = Card;


                // This draws the card information onto the card, and onto the screen
                Card.GetComponent<CardBehavior>().DrawCardInformation(CurrentPlayer.Hand[i]);
            }
        }

        else
        {
            // If every player has taken their turn
            // Begin minigame idiot
            GameManager.Instance.MiniGameLoader();
        }

        // TODO: Figure out how to not do this first card thing
        // Selects the first card
        EventSystem.current.SetSelectedGameObject(FirstCard.gameObject);

    }

    // Function that defines card interaction
    public void MakeCardsInteractable()
    {

    }

}
