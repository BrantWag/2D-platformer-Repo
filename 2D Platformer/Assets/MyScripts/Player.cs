using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     [System.Serializable]
    public class PlayerStats 
    {
        public int maxHealth = 100;
        private int _curHealth;

        public int curHealth 
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    public PlayerStats stats = new PlayerStats();

    public int fallBoundry = -20;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();
        if (statusIndicator == null)
        {
            Debug.LogError("No Stats indicator referanced");
        }
        else 
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    void Update()
    {
        if (transform.position.y <= fallBoundry) 
        {
            DamagePlayer(999999);
        }
    }

    public void DamagePlayer(int damage) 
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
            Debug.Log("Kill Player");
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
    
}
