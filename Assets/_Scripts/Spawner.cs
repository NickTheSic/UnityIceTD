using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Vector3 spawnPos = new Vector3(-9, 1.5f, -0.1f);

    public static int CurrentWave = 0;
    public static int CurrentSpawns = 0;
    public int maxRoundSpawns = 0;
    public float SpawnDelayTimer;
    public float delayTime = 3.0f;

    public GameObject Enemy;
    public Button btn;

    bool AutoPlay = false;
    public Button auto;

    public void AutoPlayButton()
    {
        AutoPlay = !AutoPlay;
    }

    public enum WaveProgress
    {
        StartOfGame,
        Spawning,
        InProgress,
        Ended
    }

    public WaveProgress wp;

    public bool CanStartNextWave = true;

    // Start is called before the first frame update
    void Start()
    {
        wp = WaveProgress.StartOfGame;
    }

    // Update is called once per frame
    void Update()
    {
        btn.gameObject.SetActive(CanStartNextWave);

        if (wp == WaveProgress.StartOfGame)
        {
            return;
        }

        if (wp == WaveProgress.Spawning)
        {
            SpawnDelayTimer += Time.deltaTime;

            int myEquation = (CurrentWave * ((CurrentWave + 1) / 3)) + 4;

            if (SpawnDelayTimer > delayTime)
            {
                if (maxRoundSpawns < myEquation)
                {
                    GameObject enem = Instantiate(Enemy, spawnPos, Quaternion.identity) as GameObject;
                    enem.name = "Enemy" + CurrentSpawns.ToString();
                    maxRoundSpawns++;
                    CurrentSpawns++;
                    SpawnDelayTimer -= delayTime;
                }
                else if (maxRoundSpawns >= myEquation)
                {
                    wp = WaveProgress.InProgress;
                }
            }
        }

        if (wp == WaveProgress.InProgress)
        {

            if (CurrentSpawns == 0)
            {
                wp = WaveProgress.Ended;
            }
        }

        if (wp == WaveProgress.Ended)
        {
            CanStartNextWave = true;
            SpawnDelayTimer = 0;
            maxRoundSpawns = 0;
            CurrentSpawns = 0;
            if (AutoPlay)
                StartWave();
        }

    }


    public void StartWave()
    {
        if (CanStartNextWave)
        {
            SpawnDelayTimer = 1.8f;
            CurrentWave++;
            wp = WaveProgress.Spawning;
            CanStartNextWave = false;
        }
    }

}
