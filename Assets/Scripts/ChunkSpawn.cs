using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawn : MonoBehaviour
{
    public PlayerBehaviour Player;
    public Chunk[] Chunks;
    public List<Chunk> ChunksOnScane = new List<Chunk>();
    [Range(0,30)]
    public float SpawnDistance=15;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
