using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushySpawner : MonoBehaviour {

    [SerializeField] GameObject penguin;
    //Keeps track of how many times it has looped so far
    //(Will be used for making the waves more difficult as time goes on)
    int loop = 0;
    //The vector that holds the spawn locations for enemies
    List<Vector2> spawnLocation;

	// Use this for initialization
	void Start () {
        //Populate the array with each possible location that enemies can spawn in
        spawnLocation = new List<Vector2>();
        //Repopulate the spawn list
        PopulateSpawnList();

        InvokeRepeating("Spawner", 2.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void PopulateSpawnList()
    {
        //Empty the spawn list
        spawnLocation.Clear();
        //Repopulate the list
        spawnLocation.Add(new Vector2(11.5f, 4.2f));
        spawnLocation.Add(new Vector2(11.5f, 2.8f));
        spawnLocation.Add(new Vector2(11.5f, 1.4f));
        spawnLocation.Add(new Vector2(11.5f, 0f));
        spawnLocation.Add(new Vector2(11.5f, -1.4f));
        spawnLocation.Add(new Vector2(11.5f, -2.8f));
        spawnLocation.Add(new Vector2(11.5f, -4.2f));
    }

    private void Spawner()
    {
        //Choose a number of locations equal to one less the size of the list
        int chooseNumberOfEnemiesToSpawn = Random.Range(0, spawnLocation.Count);
        for (int i = 0; i < chooseNumberOfEnemiesToSpawn; i++)
        {
            
        }

        //Repopulate the spawn list
        PopulateSpawnList();
    }

}
