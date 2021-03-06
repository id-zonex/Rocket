﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SurvivalControler : MonoBehaviour
{
    public int maxChunksOnStage = 3;
    [SerializeField] Transform player;
    [SerializeField] Chunk[] chunksHorizontal;
    [SerializeField] Chunk[] ChunksVertical;

    [SerializeField] Chunk[] crossingChunkVerticalToHorizontal;
    [SerializeField] Chunk[] crossingChunkHorizontalToVertical;

    [SerializeField] [Range(0, 100)] int chanceSpawnVertIcalCrossing;
    [SerializeField] [Range(0, 100)] int chanceSpawnHorizontalCrossing;

    [SerializeField] Chunk FIRST_ElEMENT;
    [SerializeField] UnityEvent scoreEvent;
    [SerializeField] UnityEvent newRecordEvent;

    private List<Chunk> spawnedChunks = new List<Chunk>();
    private Chunk lastChunk;
    public static int score;
    private bool isRecordChanged;

    private enum State
    {
        Horizontal,
        Vertical
    }
    private State state;


    void Start()
    {
        state = State.Horizontal;
        spawnedChunks.Add(FIRST_ElEMENT);
        lastChunk = FIRST_ElEMENT;
    }


    void Update()
    {
        if (player.position.x + 40 > lastChunk.transform.position.x && state == State.Horizontal)
        {
            HorizontalSpawn();
        }
        else if (player.position.y + 40 > lastChunk.transform.position.y && state == State.Vertical)
        {
            VerticalSpawn();
        }
    }


    void VerticalSpawn()
    {
        Chunk newChunk = null;
        int randInt = Random.Range(0, 100);

        if (randInt < chanceSpawnHorizontalCrossing)
        {
            newChunk = GetRangomChunk(crossingChunkVerticalToHorizontal);
            state = State.Horizontal;
        }
        else
        {
            newChunk = GetRangomChunk(ChunksVertical);
        }

        lastChunk = spawnedChunks[spawnedChunks.Count - 1];

        SpawnChunk(newChunk);

        TryDelChunkAndAddScore();
    }


    void HorizontalSpawn()
    {
        Chunk newChunk = null;
        int randInt = Random.Range(0, 100);

        if (randInt < chanceSpawnVertIcalCrossing)
        {
            newChunk = GetRangomChunk(crossingChunkHorizontalToVertical);
            state = State.Vertical;
        }
        else
        {
            newChunk = GetRangomChunk(chunksHorizontal);
        }
        
        lastChunk = spawnedChunks[spawnedChunks.Count - 1];

        SpawnChunk(newChunk);

        TryDelChunkAndAddScore();
    }


    void SpawnChunk(Chunk newChunk)
    {
        Transform EndPos = lastChunk.EndPos;
        newChunk.transform.position = newChunk.MiddlePos.position - newChunk.StartPos.position + EndPos.position;
        spawnedChunks.Add(newChunk);
    }


    Chunk GetRangomChunk(Chunk[] Chucks)
    {
        return Instantiate(Chucks[Random.Range(0, Chucks.Length)]);
    }


    void TryDelChunkAndAddScore()
    {
        if (spawnedChunks.Count > maxChunksOnStage)
        {
            AddScore();
            DelChunk();
        }
    }


    void DelChunk()
    {
        Destroy(spawnedChunks[0].gameObject);
        spawnedChunks.RemoveAt(0);
        spawnedChunks[0].Wall.SetActive(true);
    }


    void AddScore()
    {
        score += 1;
        ChangeRecord();
        scoreEvent.Invoke();
    }


    void ChangeRecord()
    {
        if (!isRecordChanged)
        {
            var currentRecordScore = PlayerPrefs.GetInt("currentRecordScore");
            if (score > currentRecordScore)
            {
                PlayerPrefs.SetInt("currentRecordScore", score);
                isRecordChanged = true;
                print(currentRecordScore);
                newRecordEvent.Invoke();
            }
        }
    }

    [System.Serializable]
    public class UnityEventScoreText : UnityEvent<string> { }
}

