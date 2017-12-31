using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats
        
    {
        public int maxHealth = 100;
        public bool isDead = false;
        private int _curHealth = 100;

        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth);  }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();

    public int fallBoundary = -20;

    private void Update()
    {
        if (transform.position.y <= fallBoundary) 
        {
            DamagePlayer(999999999);
        }
    }

    public void DamagePlayer (int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0 && !stats.isDead)
        {
            stats.isDead = true;
            GameMaster.KillPlayer(this);

        }
    }

    public void PlayFootStepSound()
    {
        
    }

}
