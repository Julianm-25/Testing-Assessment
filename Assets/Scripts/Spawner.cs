using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // creates a list of active asteroids
    public List<GameObject> asteroids = new List<GameObject>();

    // creates gameobjects for the 3 asteroid sizes
    [SerializeField]
    private GameObject asteroid1;
    [SerializeField]
    private GameObject asteroid2;
    [SerializeField]
    private GameObject asteroid3;
    
    public void BeginSpawning()
    {
        // starts the IEnumerator coroutine for spawning a new asteroid
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        // acts as a cooldown for asteroid spawns
        yield return new WaitForSeconds(0.4f);
        // spawns the asteroid
        SpawnAsteroid();
        // starts the coroutine again
        StartCoroutine("Spawn");
    }

    public GameObject SpawnAsteroid()
    {
        // generates a random number between 1 and 4
        int random = Random.Range(1, 4);
        GameObject asteroid;
        // selects an asteroid size based on the random number 
        switch (random)
        {
            case 1:
                asteroid = Instantiate(asteroid1);
                break;
            case 2:
                asteroid = Instantiate(asteroid2);
                break;
            case 3:
                asteroid = Instantiate(asteroid3);
                break;
            default:
                asteroid = Instantiate(asteroid1);
                break;
        }
        // activates the asteroid
        asteroid.SetActive(true);
        // picks a random x position
        float xPos = Random.Range(-8.0f, 8.0f);
        // spawns the asteroid just above the top of screen at the random x position
        asteroid.transform.position = new Vector3(xPos, 7.35f, 0);
        // adds the new asteroid to the list of asteroids
        asteroids.Add(asteroid);

        return asteroid;
    }
    public void ClearAsteroids()
    {
        foreach(GameObject asteroid in asteroids)
        {
            // removes each asteroid in the list 
            Destroy(asteroid);
        }
        // clears the list
        asteroids.Clear();
    }

    public void StopSpawning()
    {
        // stops more asteroids from spawning
        StopCoroutine("Spawn");
    }
}
