using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushySpawner : MonoBehaviour {

    [SerializeField] GameObject penguin;
    //Keeps track of how many times it has looped so far
    //(Will be used for making the waves more difficult as time goes on)
    int loop = 0;
    //Used for determining how fast things spawn
    float spawnTimer = 2.0f;

    //Used for increasing number of penguin spawns
    float spawnerDifficulty = 0f;

    //Determines the minimum number of enemies that can spawn
    int spawnMinimum = 1;
    //The vector that holds the spawn locations for enemies
    List<Vector2> spawnLocation;

	// Use this for initialization
	void Start () {
        //Populate the array with each possible location that enemies can spawn in
        spawnLocation = new List<Vector2>();
        //Repopulate the spawn list
        PopulateSpawnList();

        InvokeRepeating("Spawner", spawnTimer, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void PopulateSpawnList()
    {
        //This will go up at the same time that the timer does so you can impliment timing points where things get harder
        spawnerDifficulty += spawnTimer;

        if (spawnerDifficulty >= 10)
        {
            spawnMinimum = 3;
        }
        else if (spawnerDifficulty >= 20)
        {
            spawnMinimum = 5;
        }

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
        int chooseNumberOfEnemiesToSpawn = Random.Range(3, spawnLocation.Count);
        for (int i = 0; i < chooseNumberOfEnemiesToSpawn; i++)
        {
            //Choose a random slot to spawn an enemy
            int chooseIndex = Random.Range(0, spawnLocation.Count);
            Vector3 spawningObjectLocation = spawnLocation[chooseIndex];
            GameObject newPenguin;

            //Instantiate a penguin in that location
            newPenguin = Instantiate(penguin, spawningObjectLocation, transform.rotation);
            //Attach the penguins to the game manager so I can freeze them all when the game ends
            newPenguin.transform.parent = gameObject.transform;
            //Remove the location
            spawnLocation.RemoveAt(chooseIndex);

        }

        //Repopulate the spawn list
        PopulateSpawnList();
    }

}
