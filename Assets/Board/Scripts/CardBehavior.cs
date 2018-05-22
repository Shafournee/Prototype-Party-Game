using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void DrawCardInformation(Card DisplayCard)
    {
        // Create an array of each text component of the card
        // Set those values equal to the values of inserted card
        Text[] CardInfo = GetComponentsInChildren<Text>();
        CardInfo[0].text = DisplayCard.Name;
        CardInfo[1].text = DisplayCard.Description;
        CardInfo[2].text = DisplayCard.Value.ToString();
    }
}
