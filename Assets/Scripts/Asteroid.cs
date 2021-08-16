using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1;
    private float maxY = -5;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        // moves the asteroid
        transform.Translate(Vector2.down * (Time.deltaTime * speed));
        // if it leaves the y bounds of the game, destroy it
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if it collides with the ship, destroy it and end the game
        if (collision.gameObject.name == "ShipModel")
        {
            Game.GameOver();
            Destroy(gameObject);
        }
    }
}
