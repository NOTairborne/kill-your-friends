using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    private void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM"). GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;

    public IEnumerator RespawnPlayer ()
        
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy (clone.gameObject, 3f);
    }

    public static void KillPlayer (Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy (Enemy enemy)
    {
        gm._killEnemy(enemy);
    }
    public void _killEnemy(Enemy _enemy)
    {
        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone.gameObject, 5f);
        Destroy(_enemy.gameObject);
    }

}
