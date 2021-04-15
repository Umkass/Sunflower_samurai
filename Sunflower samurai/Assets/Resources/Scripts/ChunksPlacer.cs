using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    [SerializeField]
    Transform character;
    [SerializeField]
    Chunk[] chunkPrefabs;
    List<Chunk> spawnedChunks = new List<Chunk>();
    void SpawnChunk()
    {
        Chunk newChunk = Instantiate(chunkPrefabs[Random.Range(1, chunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
        spawnedChunks.Add(newChunk);
        if (spawnedChunks.Count > 3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }
    void Start()
    {
        Chunk startChunk = Instantiate(chunkPrefabs[0]);
        spawnedChunks.Add(startChunk);
    }
    void Update()
    {
        if(character.position.x > spawnedChunks[spawnedChunks.Count - 1].end.position.x-20f)
        {
            SpawnChunk();
        }
    }
}
