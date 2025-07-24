using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] PlatformTypes;
    public int PlatformCount = 10;
    public float MinDistance = 2f;
    public Transform PlatformParent;

    private float minX = -12.6f;
    private float maxX = 61.5f;
    private float minY = -13.6f;
    private float maxY = 20f;

    private List<Vector2> placedPositions = new List<Vector2>();
    void Start()
    {
        SpawnAllPlatforms();
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
            attempts += 1;
            Vector2 randomPos = GetRandomPosition();

            bool farEnough = IsFarEnoughFromOthers(randomPos);

            if (farEnough == true)
            {
                SpawnPlatformAt(randomPos);
                placedPositions.Add(randomPos);
                spawned += 1;
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
        for (int i = 0; i < placedPositions.Count; i++)
        {
            if (Vector2.Distance(placedPositions[i], pos) < MinDistance)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnPlatformAt(Vector2 position)
    {
        GameObject platform = PlatformTypes[Random.Range(0, PlatformTypes.Length)];
        Instantiate(platform, position, Quaternion.identity, PlatformParent);
    }
}
