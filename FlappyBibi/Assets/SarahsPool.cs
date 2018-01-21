using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahsPool : MonoBehaviour {
    public GameObject sarahObjPrefab;                                 //The sarahing game object.
    public int sarahObjPoolSize = 5;                                  //How many sarahing objects to keep on standby.
    public float sarahSpawnRate = 3f;                                 //How quickly sarahing objects spawn.
    public float sarahObjMin = 0f;                                    //Minimum y value of the sarahing object position.
    public float sarahObjMax = 4f;									//Maximum y value of the sarahing object position.

    private GameObject[] sarahObjs;                                   //Collection of pooled colums.
    private int currentsarahObj = 0;                                  //Index of the current sarah obj in the collection.

    private Vector2 sarahObjectPoolPosition = new Vector2(-15, -25);  //A holding position for our unused sarah obj offscreen.
    private float sarahSpawnXPosition = 10f;

    private float timeSinceLastSpawned;
    // Use this for initialization
    void Start()
    {
        timeSinceLastSpawned = 0f;

        sarahObjs = new GameObject[sarahObjPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < sarahObjPoolSize; i++)
        {
            //...and create the individual columns.
            sarahObjs[i] = (GameObject)Instantiate(sarahObjPrefab, sarahObjectPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= sarahSpawnRate)
        {
            timeSinceLastSpawned = 0f;
            /*
            if (gameObject.GetComponent("ColumnPool") != null)
            {

                float tmp = gameObject.GetComponent<ColumnPool>().getCurrentColumnX();
                if (tmp - sarahSpawnXPosition > 2f ||
                    tmp - sarahSpawnXPosition < -2f)
                {
                    sarahSpawnXPosition += 2f;
                }
            }
            */

            //Set a random y position for the column
            float spawnYPosition = Random.Range(sarahObjMin, sarahObjMax);

            //...then set the current column to that position.
            sarahObjs[currentsarahObj].transform.position = new Vector2(sarahSpawnXPosition, spawnYPosition);

            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentsarahObj++;

            if (currentsarahObj >= sarahObjPoolSize)
            {
                currentsarahObj = 0;
            }
        }
    }
}
