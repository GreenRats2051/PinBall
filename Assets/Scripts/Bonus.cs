using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public Transform spawnPoint;
    public int smallBonusValue = 100;
    public int bigBonusValue = 500;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            int value = CompareTag("BigBonus") ? bigBonusValue : smallBonusValue;
            ScoreManager.Instance.AddScore(value);

            BonusSpawner.Instance.FreeSpawnPoint(spawnPoint);
            Destroy(gameObject);
        }
    }
}