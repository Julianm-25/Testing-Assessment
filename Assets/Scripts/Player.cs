using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // sets player speed
    public float speed = 1;
    // sets player life status
    public bool isAlive = true;
    // sets player shoot status
    public bool canShoot = true;

    // SerializeField allows private variables to be viewed in the Unity Editor
    // sprite for the ship
    [SerializeField]
    private SpriteRenderer sprite;
    // explosion sprite on death
    [SerializeField]
    private GameObject explosion;
    // laser sprite
    [SerializeField]
    private GameObject laser;
    // laser spawn position
    [SerializeField]
    private Transform shotSpawn;
    
    // Maximum left and right bounds of the game screen
    private float maxLeft = -8;
    private float maxRight = 8;
    private void Update()
    {
        if (isAlive)
        {
            // if left arrow is pressed, rotate left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            // if right arrow is pressed, rotate right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKey(KeyCode.Space) && canShoot)
            {
                // shoots a laser when space is pressed and the laser is not on cooldown
                ShootLaser();
            }
        }
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * speed));
        if (transform.position.x < maxLeft)
        {
            transform.position = new Vector3(maxLeft, -3.22f, 0);
        }
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
        if (transform.position.x > maxRight)
        {
            transform.position = new Vector3(maxRight, -3.22f, 0);
        }
    }
    public void ShootLaser()
    {
        // starts the IEnumerator shoot Coroutine
        StartCoroutine("Shoot");
    }
    IEnumerator Shoot()
    {
        // stops multiple shots from spawning at once
        canShoot = false;
        // creates a new laser
        GameObject laserShot = SpawnLaser();
        // sets the position of the new laser to the set laser spawn position
        laserShot.transform.position = shotSpawn.position;
        // waits for the laser cooldown
        yield return new WaitForSeconds(0.4f);
        // allows the player to shoot again
        canShoot = true;
    }
    public GameObject SpawnLaser()
    {
        // instantiates a new laser
        GameObject newLaser = Instantiate(laser);
        // activates the laser 
        newLaser.SetActive(true);
        // returns the laser that was created
        return newLaser;
    }

    public void Die()
    {
        // disables player sprite
        sprite.enabled = false;
        // shows explosion sprite
        explosion.SetActive(true);
        isAlive = false;
    }

    public void Respawn()
    {
        // removes explosion sprite
        explosion.SetActive(false);
        // re-enables player sprite
        sprite.enabled = true;
        isAlive = true;
    }
}
