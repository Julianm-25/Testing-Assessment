using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;

    void Update ()
    {
        // movement
        transform.Translate(Vector2.up * (Time.deltaTime * 5));
        // if it leaves the bounds of the game, remove it
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the laser collides with an asteroid, remove the laser and the asteroid
        if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            Game.AsteroidDestroyed();
            Destroy(gameObject);
            spawner.asteroids.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
