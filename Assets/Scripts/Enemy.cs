using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats

    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 20;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();

    public Transform deathParticles;

    private void Start()
    {
        if (deathParticles == null)
        {
            Debug.LogError("No particles boii");
        }
    }

    public void DamageEnemy (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)
		{
			GameMaster.KillEnemy (this);
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemy(9999999);
        }
    }
}
