using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    void Awake()
    {
        if (gm == null) 
        {
           gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        } 
    }
    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public AudioClip respawnAudio;

    
    public CameraShake cameraShake;

    void Start()
    {
        if (cameraShake == null) 
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }
    }

    public IEnumerator _RespwanPlayer() 
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone, 3f);
        
    }

    public static void KillPlayer(Player player) 
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm._RespwanPlayer());
    }


    public static void KillEnemy(Enemy enemy) 
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy) 
    {
        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone, 5f);
        cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLenght);
        Destroy(_enemy.gameObject);

    }

}
