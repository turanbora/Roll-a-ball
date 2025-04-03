using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    
    private Image buttonImage;
    public TextMeshProUGUI countdownText; // Eðer Text kullanýyorsan, Text olarak deðiþtir
    private bool isPaused = false;

    public void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f; // Oyunu durdur
           
        }
        else
        {
            StartCoroutine(ResumeWithCountdown());

        }
    }
    private IEnumerator ResumeWithCountdown()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSecondsRealtime(1f); // 1 saniye bekle
            countdown--;
        }

        countdownText.text = ""; // Geri sayým bittiðinde metni boþalt
        Time.timeScale = 1f; // Oyunu devam ettir
        isPaused = false;
    }
}
