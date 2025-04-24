using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int comboMultiplier = 1;
    public int comboHits = 0;

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

    public void AddScore(int basePoints)
    {
        comboHits++;
        if (comboHits >= 3)
        {
            comboMultiplier = Mathf.Min(comboMultiplier + 1, 5);
            comboHits = 0;
        }

        score += basePoints * comboMultiplier;
        UpdateUI();
    }

    public void ResetCombo()
    {
        comboMultiplier = 1;
        comboHits = 0;
    }

    void UpdateUI()
    {
        // Обновление UI счета
    }
}