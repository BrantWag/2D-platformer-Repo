using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;
    [SerializeField]
    Animator waveAnimator;
    [SerializeField]
    Text waveCountdwonText;
    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;


    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null) 
        {
            Debug.LogError("No Spawenr referenced!");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced!");
            this.enabled = false;
        }
        if (waveCountdwonText == null)
        {
            Debug.LogError("No waveCountdwonText referenced!");
            this.enabled = false;
        }
        if ( waveCountText== null)
        {
            Debug.LogError("No waveCountText referenced!");
            this.enabled = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State) 
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountintUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawingUI();
                break;
                
        }
        previousState = spawner.State;


    }

    void UpdateCountintUI() 
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
            Debug.Log("COUNTING");
        }
        waveCountdwonText.text = ((int)spawner.WaveCountdown).ToString();
    }
    void UpdateSpawingUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.NextWave.ToString();
            Debug.Log("SPAWNING");
        }
    }
}
