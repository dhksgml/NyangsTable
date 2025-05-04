using System.Collections;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    
    public Vector2 spawnAreaMin; // 맵의 최소 x, y 좌표 (왼쪽 하단)
    public Vector2 spawnAreaMax; // 맵의 최대 x, y 좌표 (오른쪽 상단)

    public float minInterval = 5f;
    public float maxInterval = 20f;

    private float nextSpawnTime = 0f;

    public int maxSpawnCount = 3; // 최대 생성할 프리팹 개수
    public int currentSpawnCount = 0; // 현재 생성된 프리팹 개수

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentSpawnCount < maxSpawnCount)
        {
            Spawn();
            SetNextSpawnTime();
        }
    }

    private void Spawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y), 0);
        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        newPrefab.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) == 1;
        currentSpawnCount++;
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    internal void DecreaseSpawnCount()
    {
        currentSpawnCount = Mathf.Max(0, currentSpawnCount - 1);
    }

    public int GetRouchCount()
    {
        return currentSpawnCount;
    }
}
