using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.TestTools;

public class GameTest
{
    private Game game;
    private Player player;
    private GameObject explosion;
    [SetUp]
    public void Setup()
    {
        GameObject gameGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGO.GetComponent<Game>();
        player = gameGO.GetComponentInChildren<Player>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }
    
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.spawner.SpawnAsteroid();
        float startYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        
        Assert.Less(asteroid.transform.position.y , startYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.spawner.SpawnAsteroid();
        asteroid.transform.position = game.shipModel.transform.position;
        yield return new WaitForSeconds(0.1f);
        
        Assert.True(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator LaserDestroyedOnAsteroidCollision()
    {
        GameObject asteroid = game.spawner.SpawnAsteroid();
        GameObject laser = player.SpawnLaser();
        asteroid.transform.position = laser.transform.position;
        yield return new WaitForSeconds(0.1f);
        
        Assert.True(laser == null);
    }

    [UnityTest]
    public IEnumerator LasersMoveUp()
    {
        GameObject laser = player.SpawnLaser();
        float startYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        
        Assert.Greater(laser.transform.position.y, startYPos);
    }

    [UnityTest]
    public IEnumerator ExplosionDeactivatedOnRespawn()
    {
        explosion = player.transform.Find("AsteroidExplosion").gameObject;
        player.Die();
        player.Respawn();

        Assert.False(explosion.activeSelf);
        yield return null;
    }
}