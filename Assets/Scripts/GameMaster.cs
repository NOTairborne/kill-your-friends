using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public int lives = 3;
    public int kills = 0;

    private void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM"). GetComponent<GameMaster>();
            gm.UpdateLivesAndKills();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public Text livesText;
    public Text killText;

    public IEnumerator RespawnPlayer ()        
    {
        if (lives == 0)
        {
            yield return new WaitForSeconds(spawnDelay);
            lives = 3;
            kills = 0;
            SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
        }
        else
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
            Destroy(clone.gameObject, 3f);
            lives = lives - 1;
            UpdateLivesAndKills();
        }
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
        kills = kills + 1;
        UpdateLivesAndKills();
    }

    void UpdateLivesAndKills()
    {
        livesText.text = string.Format("Lives: {0}", lives);
        killText.text = string.Format("Kills: {0}", kills);
    }

}
