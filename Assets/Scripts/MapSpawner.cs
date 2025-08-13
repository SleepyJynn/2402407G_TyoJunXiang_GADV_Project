using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    // Platforms
    public GameObject[] PlatformTypes; // array of platform prefabs to spawn
    public int PlatformCount = 10; // number of platforms to spawn
    public float MinDistance = 2f; // minimum distance between spawned objects
    public Transform PlatformParent; // parent for spawned platforms in hierarchy

    // Asteroid
    public GameObject AsteroidPrefab; // prefab for the asteroid
    public Transform AsteroidParent; // parent for asteroid in hierarchy

    // Collectible Time
    public GameObject CollectibleTimePrefab; // prefab for collectible time object
    public Transform CollectibleTimeParent; // parent for collectible time objects

    // Spawn bounds
    private float minX = -12.6f; // minimum X coordinate for spawn
    private float maxX = 61.5f; // maximum X coordinate for spawn
    private float minY = -13.6f; // minimum Y coordinate for spawn
    private float maxY = 20f; // maximum Y coordinate for spawn

    // Tracking
    private List<Vector2> placedPositions = new List<Vector2>(); // tracks used positions to avoid overlap

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

        // attempt to spawn platforms until count reached or too many attempts
        while (spawned < PlatformCount && attempts < PlatformCount * 20)
        {
            attempts++;
            Vector2 randomPos = GetRandomPosition();

            bool farEnough = IsFarEnoughFromOthers(randomPos);

            // only spawn if far enough from other objects
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

        // try spawning asteroid(s) until max attempts reached
        while (attempts < 50) // limit tries
        {
            attempts++;
            Vector2 randomPos = GetRandomPosition();

            if (IsFarEnoughFromOthers(randomPos))
            {
                // instantiate asteroid and parent to hierarchy
                Instantiate(AsteroidPrefab, randomPos, Quaternion.identity, AsteroidParent != null ? AsteroidParent : PlatformParent);
                placedPositions.Add(randomPos);
            }
        }
    }

    void SpawnAllCollectibleTime()
    {
        int attempts = 0;

        // try spawning collectible time objects until max attempts reached
        while (attempts < 15) // limit tries
        {
            attempts++;
            Vector2 randomPos = GetRandomPosition();

            if (IsFarEnoughFromOthers(randomPos))
            {
                // instantiate collectible time object and parent to hierarchy
                Instantiate(CollectibleTimePrefab, randomPos, Quaternion.identity, CollectibleTimeParent != null ? CollectibleTimeParent : PlatformParent);
                placedPositions.Add(randomPos);
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        // return a random position within defined bounds
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }

    bool IsFarEnoughFromOthers(Vector2 pos)
    {
        // check all previously placed positions to ensure minimum distance
        foreach (Vector2 placed in placedPositions)
        {
            if (Vector2.Distance(placed, pos) < MinDistance)
                return false;
        }
        return true;
    }

    void SpawnPlatformAt(Vector2 position)
    {
        // choose a random platform prefab and instantiate it at given position
        GameObject platform = PlatformTypes[Random.Range(0, PlatformTypes.Length)];
        Instantiate(platform, position, Quaternion.identity, PlatformParent);
    }
}
