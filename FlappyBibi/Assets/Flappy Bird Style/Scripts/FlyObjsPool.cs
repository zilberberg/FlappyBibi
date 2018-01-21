using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObjsPool : MonoBehaviour {
    public GameObject flyObjPrefab;                                 //The flying game object.
    public int flyObjPoolSize = 5;                                  //How many flying objects to keep on standby.
    public float flySpawnRate = 3f;                                 //How quickly flying objects spawn.
    public float flyObjMin = 0f;                                    //Minimum y value of the flying object position.
    public float flyObjMax = 4f;									//Maximum y value of the flying object position.

    private GameObject[] flyObjs;                                   //Collection of pooled colums.
    private int currentFlyObj = 0;                                  //Index of the current fly obj in the collection.

    private Vector2 flyObjectPoolPosition = new Vector2(-15, -25);  //A holding position for our unused fly obj offscreen.
    private float flySpawnXPosition = 10f;

    private float timeSinceLastSpawned;
    // Use this for initialization
    void Start () {
        timeSinceLastSpawned = 0f;

        flyObjs = new GameObject[flyObjPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < flyObjPoolSize; i++)
        {
            //...and create the individual columns.
            flyObjs[i] = (GameObject)Instantiate(flyObjPrefab, flyObjectPoolPosition, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawned += Time.deltaTime;

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= flySpawnRate)
        {
            flySpawnXPosition = 10f;
            timeSinceLastSpawned = 0f;
            
            /*
            if (gameObject.GetComponent("ColumnPool") != null)
            {
                
                float tmp = gameObject.GetComponent<ColumnPool>().getCurrentColumnX();
                if (tmp - flySpawnXPosition < 3f ||
                    tmp - flySpawnXPosition > -3f)
                {
                    flySpawnXPosition += 5f;
                }
                else
                {
                    flySpawnXPosition = 10f;
                }
                
            }
            */
            //Set a random y position for the column
            float spawnYPosition = Random.Range(flyObjMin, flyObjMax);

            //...then set the current column to that position.
            flyObjs[currentFlyObj].transform.position = new Vector2(flySpawnXPosition, spawnYPosition);

            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentFlyObj++;

            if (currentFlyObj >= flyObjPoolSize)
            {
                currentFlyObj = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            if (collision.gameObject.layer == 2)
            {
                flySpawnXPosition += 5f;
            }
        }
        
    }
}
