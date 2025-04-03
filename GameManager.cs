using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


    public class GameManager : MonoBehaviour
    {
        public Text scoreText; // Sol üstteki skor Text'i
        public Text timeText; // Sað üstteki zaman Text'i
        public float initialGameDuration = 20f; // Oyunun baþýndaki süre
        public GameObject gameOverPanel;
        public GameObject gameTimeCanvas;
        public Vector3 bPosition; // B pozisyonunun koordinatlarý
        public Vector3 bRotation;
        public GameObject countdownTextObject;
        private float gameDuration; // Dinamik olarak deðiþen süre
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
                Debug.LogError("Jiroskop scripti bulunamadý!");
            }
            startTime = Time.time;
            gameDuration = initialGameDuration; // Baþlangýçta oyunun süresini ayarla
            UpdateScoreUI(); // Ýlk baþta skoru güncelle
            UpdateTimeUI(gameDuration);  // Ýlk baþta kalan süreyi güncelle

            timeRemaining = gameDuration;
            gameOverPanel.SetActive(false); // Oyunun baþýnda gameOverPanel gizli
        }

        void Update()
        {
            if (!gameEnded)
            {
                float elapsedTime = Time.time - startTime;

                // Kalan süreyi hesapla
                float remainingTime = gameDuration - elapsedTime;

                if (remainingTime <= 0f)
                {
                    remainingTime = 0f;
                    EndGame(); // Oyunu bitir
                }

                UpdateTimeUI(remainingTime); // Kalan süreyi güncelle
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
                // Kalan süreyi güncelleyerek oyunun bitiþ süresini uzat
                gameDuration += amount; // Oyunun baþlangýç zamanýný geriye alarak süre ekleme yap
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
            MoveCameraToB(); // Kamera B pozisyonuna ýþýnlanýr
            gameOverPanel.SetActive(true); // GameOver panelini göster
            gameTimeCanvas.SetActive(false);
        int lastScore = jiroskop.score;
        lastScoreText.text = "Last Score: " + lastScore.ToString();

        // Best score'u kontrol et ve gerekirse güncelle
        int bestScore = PlayerPrefs.GetInt("BestScore", 0); // Varsayýlan olarak 0
        if (lastScore > bestScore)
        {
            bestScore = lastScore;
            PlayerPrefs.SetInt("BestScore", bestScore); // Yeni en iyi skoru kaydet
            PlayerPrefs.Save(); // Deðiþiklikleri kaydet
        }

        // Best score'u ekranda göster
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
            countdownTextObject.SetActive(true); // Geri sayým metnini göster
            int countdown = 3;
            while (countdown > 0)
            {
                countdownTextObject.GetComponent<TextMeshProUGUI>().text = countdown.ToString();
                yield return new WaitForSeconds(1f);
                countdown--;
            }
            countdownTextObject.SetActive(false); // Geri sayým metnini gizle
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Sahneyi yeniden yükle
        }
    }
