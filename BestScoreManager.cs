using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestScoreManager : MonoBehaviour
{
    public Jiroskop jiroskopScript;
    public int bestScore ;
    public int lastScore ;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI lastScoreText;
    void Update()
    {
        if (jiroskopScript != null)
        {
            int currentScore = jiroskopScript.score;

            // Son skoru güncelle
            lastScore = currentScore;

            // Eðer mevcut skor best score'dan büyükse, best score'u güncelle
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
            }

            // Text UI elemanlarýný güncelle
            if (bestScoreText != null)
            {
                bestScoreText.text = "Best Score: " + bestScore;
            }

            if (lastScoreText != null)
            {
                lastScoreText.text = "Last Score: " + lastScore;
            }
        }
    }
    
}