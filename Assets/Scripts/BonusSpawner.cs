using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public static BonusSpawner Instance { get; private set; }

    public GameObject smallBonusPrefab;
    public GameObject bigBonusPrefab;
    public Transform[] spawnPoints;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;

    private float nextSpawnTime;
    private List<Transform> availablePoints = new List<Transform>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        availablePoints.AddRange(spawnPoints);
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && availablePoints.Count > 0)
        {
            SpawnBonus();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnBonus()
    {
        int index = Random.Range(0, availablePoints.Count);
        Transform spawnPoint = availablePoints[index];

        bool isBigBonus = Random.value > 0.7f;
        GameObject bonusPrefab = isBigBonus ? bigBonusPrefab : smallBonusPrefab;

        GameObject bonus = Instantiate(bonusPrefab, spawnPoint.position, Quaternion.identity);
        bonus.GetComponent<Bonus>().spawnPoint = spawnPoint;

        availablePoints.RemoveAt(index);
    }

    public void FreeSpawnPoint(Transform point)
    {
        if (!availablePoints.Contains(point))
        {
            availablePoints.Add(point);
        }
    }
}