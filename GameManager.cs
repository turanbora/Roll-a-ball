using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


    public class GameManager : MonoBehaviour
    {
        public Text scoreText; // Sol �stteki skor Text'i
        public Text timeText; // Sa� �stteki zaman Text'i
        public float initialGameDuration = 20f; // Oyunun ba��ndaki s�re
        public GameObject gameOverPanel;
        public GameObject gameTimeCanvas;
        public Vector3 bPosition; // B pozisyonunun koordinatlar�
        public Vector3 bRotation;
        public GameObject countdownTextObject;
        private float gameDuration; // Dinamik olarak de�i�en s�re
        private float startTime;
        private Jiroskop jiroskop;
        private bool gameEnded = false;
        private int score = 0;
        private float timeRemaining;
        private bool isGameOver = false;
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI bestScoreText;

    void Start()
        {
            jiroskop = FindObjectOfType<Jiroskop>();
            if (jiroskop == null)
            {
                Debug.LogError("Jiroskop scripti bulunamad�!");
            }
            startTime = Time.time;
            gameDuration = initialGameDuration; // Ba�lang��ta oyunun s�resini ayarla
            UpdateScoreUI(); // �lk ba�ta skoru g�ncelle
            UpdateTimeUI(gameDuration);  // �lk ba�ta kalan s�reyi g�ncelle

            timeRemaining = gameDuration;
            gameOverPanel.SetActive(false); // Oyunun ba��nda gameOverPanel gizli
        }

        void Update()
        {
            if (!gameEnded)
            {
                float elapsedTime = Time.time - startTime;

                // Kalan s�reyi hesapla
                float remainingTime = gameDuration - elapsedTime;

                if (remainingTime <= 0f)
                {
                    remainingTime = 0f;
                    EndGame(); // Oyunu bitir
                }

                UpdateTimeUI(remainingTime); // Kalan s�reyi g�ncelle
            }
            UpdateScoreUI();

            if (!isGameOver)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimeText();

                if (timeRemaining <= 0)
                {
                    EndGame();
                }
            }
        }

        public void AddScore(int amount)
        {
            jiroskop.score += amount;
            Debug.Log("Score added: " + amount + ". New score: " + jiroskop.score);

            UpdateScoreUI();
        }

        public void AddTime(float amount)
        {
            if (!gameEnded)
            {
                // Kalan s�reyi g�ncelleyerek oyunun biti� s�resini uzat
                gameDuration += amount; // Oyunun ba�lang�� zaman�n� geriye alarak s�re ekleme yap
                Debug.Log("Added time: " + amount + ". New game duration: " + gameDuration);
            }
        }

        void UpdateScoreUI()
        {
            scoreText.text = "Score: " + jiroskop.score.ToString();
        }

        void UpdateTimeUI(float remainingTime)
        {
            timeText.text = "Time: " + Mathf.CeilToInt(remainingTime).ToString();
        }

        void UpdateTimeText()
        {
            timeText.text = "Time: " + Mathf.Max(0, Mathf.FloorToInt(timeRemaining)).ToString();
        }

        void EndGame()
        {
            isGameOver = true;
            gameEnded = true;
            Time.timeScale = 0f; // Oyunu durdur
            MoveCameraToB(); // Kamera B pozisyonuna ���nlan�r
            gameOverPanel.SetActive(true); // GameOver panelini g�ster
            gameTimeCanvas.SetActive(false);
        int lastScore = jiroskop.score;
        lastScoreText.text = "Last Score: " + lastScore.ToString();

        // Best score'u kontrol et ve gerekirse g�ncelle
        int bestScore = PlayerPrefs.GetInt("BestScore", 0); // Varsay�lan olarak 0
        if (lastScore > bestScore)
        {
            bestScore = lastScore;
            PlayerPrefs.SetInt("BestScore", bestScore); // Yeni en iyi skoru kaydet
            PlayerPrefs.Save(); // De�i�iklikleri kaydet
        }

        // Best score'u ekranda g�ster
        bestScoreText.text = "Best Score: " + bestScore.ToString();
        Debug.Log("Oyun Bitti!");
        }

        public void MoveCameraToB()
        {
            Camera.main.transform.position = bPosition;
            Camera.main.transform.rotation = Quaternion.Euler(bRotation);
        }

        public void RestartGame()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private IEnumerator RestartWithCountdown()
        {
            countdownTextObject.SetActive(true); // Geri say�m metnini g�ster
            int countdown = 3;
            while (countdown > 0)
            {
                countdownTextObject.GetComponent<TextMeshProUGUI>().text = countdown.ToString();
                yield return new WaitForSeconds(1f);
                countdown--;
            }
            countdownTextObject.SetActive(false); // Geri say�m metnini gizle
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Sahneyi yeniden y�kle
        }
    }
