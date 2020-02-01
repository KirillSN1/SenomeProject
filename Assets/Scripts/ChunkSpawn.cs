using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawn : MonoBehaviour
{
    [Range(2, 10)]
    public int LimitOfChunks = 5;
    [Range(0, 30)]
    public float SpawnDistance = 15;
    [Tooltip("Сюда кидать префабы!")]
    public Chunk[] Chunks;
    public List<Chunk> ChunksOnScane = new List<Chunk>();
    private GameObject Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        DeleteOldChunk();
        if (Player.transform.position.x>= ChunksOnScane[ChunksOnScane.Count - 1].EndPos.position.x - SpawnDistance)
        {
            SpawnNewChunk();
        }
        
    }

    void SpawnNewChunk()
    {
        Chunk newChunk = Instantiate(Chunks[Random.Range(0, Chunks.Length)]);
        newChunk.transform.position = ChunksOnScane[ChunksOnScane.Count-1].EndPos.position - newChunk.StartPos.localPosition;        
        ChunksOnScane.Add(newChunk); 
    }

    void DeleteOldChunk()
    {
        if (ChunksOnScane.Count > LimitOfChunks)
        {
            Destroy(ChunksOnScane[0].gameObject);
            ChunksOnScane.RemoveAt(0);
            Debug.Log("Delating");
        }
    }
}
