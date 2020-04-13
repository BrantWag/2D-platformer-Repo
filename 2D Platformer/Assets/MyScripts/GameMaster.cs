using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives = 3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

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

    public string respawnCountdownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";
    public string gameOverSoundName = "GameOver";

    public CameraShake cameraShake;
    [SerializeField]
    private GameObject gameOverUI;

    private AudioManager audioManager;

    void Start()
    {
        if (cameraShake == null) 
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }

        _remainingLives = maxLives;

        audioManager = AudioManager.instance;
        if (audioManager == null) 
        {
            Debug.LogError("FREAK OUT! no AudioManager found in the scene.");
        }

    }
    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespwanPlayer() 
    {
        audioManager.PlaySound(respawnCountdownSoundName);
        //GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound(spawnSoundName);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone, 3f);
        
    }

    public static void KillPlayer(Player player) 
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespwanPlayer());
        }
    }


    public static void KillEnemy(Enemy enemy) 
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy) 
    {
        //lets play some sounds
        audioManager.PlaySound(_enemy.deathSoundName);

        //spawn particles
        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone, 5f);

        //go camerashake
        cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLenght);
        Destroy(_enemy.gameObject);

    }

}
