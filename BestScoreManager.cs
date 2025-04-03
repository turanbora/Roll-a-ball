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

            // Son skoru g�ncelle
            lastScore = currentScore;

            // E�er mevcut skor best score'dan b�y�kse, best score'u g�ncelle
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
            }

            // Text UI elemanlar�n� g�ncelle
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