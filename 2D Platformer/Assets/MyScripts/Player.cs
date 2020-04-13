﻿using System.Collections;
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
    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    private AudioManager audioManger;

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

        audioManger = AudioManager.instance;
        if (audioManger == null) 
        {
            Debug.LogError("YOUR DUMB!!");
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
            //death sound
            audioManger.PlaySound(deathSoundName);
            //kill player
            GameMaster.KillPlayer(this);
            Debug.Log("Kill Player");
        }
        else 
        {
            //dmg sound
            audioManger.PlaySound(damageSoundName);
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
    
}
