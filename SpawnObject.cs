using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject prefabToSpawn; // Rastgele yerleþtirilecek nesne
    public Transform cube;
    public float spawnOffset = 0.5f;

    void Start()
    {
        SpawnRandomObject();
    }

    public void SpawnRandomObject()
    {
        Vector3 cubeSize = cube.localScale;

        // Küpün merkezine göre rastgele bir konum oluþtur (X ve Z ekseninde)
        float randomX = Random.Range(-cubeSize.x / 2, cubeSize.x / 2);
        float randomZ = Random.Range(-cubeSize.z / 2, cubeSize.z / 2);
        float cubeHeight = cube.position.y + (cubeSize.y / 2) + spawnOffset;

        // Rastgele konumu belirle
        Vector3 randomPosition = new Vector3(randomX, cube.position.y, randomZ);

        // Küpü 45 derece X, Y ve Z rotasyonu ile oluþtur
        Quaternion rotation = Quaternion.Euler(45f, 45f, 45f);
        Instantiate(prefabToSpawn, randomPosition, rotation);
    }
}

