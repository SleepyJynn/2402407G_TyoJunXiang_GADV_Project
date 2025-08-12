using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    //platforms
    public GameObject[] PlatformTypes;
    public int PlatformCount = 10;
    public float MinDistance = 2f;
    public Transform PlatformParent;

    //asteroid
    public GameObject AsteroidPrefab;
    public Transform AsteroidParent;

    //collectible time
    public GameObject CollectibleTimePrefab;
    public Transform CollectibleTimeParent;

    //bounds
    private float minX = -12.6f;
    private float maxX = 61.5f;
    private float minY = -13.6f;
    private float maxY = 20f;

    private List<Vector2> placedPositions = new List<Vector2>();

    void Start()
    {
        SpawnAllPlatforms();
        SpawnAllAsteroids();
        SpawnAllCollectibleTime();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnAllPlatforms()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < PlatformCount && attempts < PlatformCount * 20)
        {
            attempts++;
            Vector2 randomPos = GetRandomPosition();

            bool farEnough = IsFarEnoughFromOthers(randomPos);

            if (farEnough)
            {
                SpawnPlatformAt(randomPos);
                placedPositions.Add(randomPos);
                spawned++;
            }
        }
    }

    void SpawnAllAsteroids()
    {
        int attempts = 0;
        while (attempts < 50) // limit tries
        {
            attempts++;
            Vector2 randomPos = GetRandomPosition();

            if (IsFarEnoughFromOthers(randomPos))
            {
                Instantiate(AsteroidPrefab, randomPos, Quaternion.identity, AsteroidParent != null ? AsteroidParent : PlatformParent);
                placedPositions.Add(randomPos);
            }
        }
    }

    void SpawnAllCollectibleTime()
    {
        int attempts = 0;
        while (attempts < 15) // limit tries
        {
            attempts++;
            Vector2 randomPos = GetRandomPosition();

            if (IsFarEnoughFromOthers(randomPos))
            {
                Instantiate(CollectibleTimePrefab, randomPos, Quaternion.identity, CollectibleTimeParent != null ? CollectibleTimeParent : PlatformParent);
                placedPositions.Add(randomPos);
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }

    bool IsFarEnoughFromOthers(Vector2 pos)
    {
        foreach (Vector2 placed in placedPositions)
        {
            if (Vector2.Distance(placed, pos) < MinDistance)
                return false;
        }
        return true;
    }

    void SpawnPlatformAt(Vector2 position)
    {
        GameObject platform = PlatformTypes[Random.Range(0, PlatformTypes.Length)];
        Instantiate(platform, position, Quaternion.identity, PlatformParent);
    }
}
