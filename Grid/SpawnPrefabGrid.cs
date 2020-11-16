using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPrefabGrid : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    public Vector3 positionRandamization;

    private void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset,0,z * gridSpacingOffset) + gridOrigin;
                PickAndSpawn(RandamizedPosition(spawnPosition),Quaternion.identity);
            }
        }
    }

    Vector3 RandamizedPosition(Vector3 position)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-positionRandamization.x,positionRandamization.x),Random.Range(-positionRandamization.y,positionRandamization.y),Random.Range(-positionRandamization.z,positionRandamization.z)) + position;

        return randomPosition;
    }

    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotation)
    {
        int rand = Random.Range(0, itemsToPickFrom.Length);

        GameObject clone = Instantiate(itemsToPickFrom[rand], positionToSpawn, rotation);
    }
}
