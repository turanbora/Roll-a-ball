using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jiroskop : MonoBehaviour
{
    public TextManager textManager;
    public float timeBonus = 0.5f; // Eklenecek s�re
    public float displayOffsetY = 1.5f;

    public float speed = 10.0f;
    public bool isSimulatingGyroInEditor = true;  // Editor'de jiroskopu sim�le etmek i�in
    public int score = 0;
    private SpawnObject spawnObject;
    public Rigidbody rb;
    private Canvas sphereCanvas;
    private TextMeshProUGUI bonusText;

    void Start()
    {
        // Jiroskopu aktif hale getir
        Input.gyro.enabled = true;
        spawnObject = FindObjectOfType<SpawnObject>();
        if (spawnObject == null)
        {
            Debug.LogError("SpawnObject scripti bulunamad�!");
        }
       
        
        
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        // Mobil cihazda jiroskop verilerini kullan
        if (!Application.isEditor)
        {
            move = new Vector3(Input.gyro.rotationRateUnbiased.x, 0, -Input.gyro.rotationRateUnbiased.y);
        }
        else if (isSimulatingGyroInEditor)
        {
            // Klavye giri�lerini kullan (sadece X-Z d�zleminde)
            float moveX = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            float moveZ = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;

            move = new Vector3(moveX, 0, moveZ);
        }

        // Y eksenindeki hareketi s�f�rla
        move.y = 0;
        rb.velocity = move * speed;
        // K�reyi hareket ettir
       

        // Jiroskopun d�n���n� sadece Y ekseninde g�ncelle
        if (!Application.isEditor)
        {
            Quaternion gyro = Input.gyro.attitude;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnObject"))
        {
            Destroy(other.gameObject); // �arp��an nesneyi yok et
            score++; // Skoru artt�r

            // Oyun s�resine ekleme yap
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddTime(timeBonus);  // S�reye 0.5 saniye ekle
            }

            spawnObject.SpawnRandomObject();

            // "+0.5s" yaz�s�n� g�ster
            ScoreTextPop scoreTextPop = GetComponentInChildren<ScoreTextPop>();
            if (scoreTextPop != null)
            {
                scoreTextPop.ShowScoreText(transform.position + Vector3.up * displayOffsetY);
            }
        }
    }
    void AddTimeToGame(float time)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddTime(time);
        }
    }
    IEnumerator ShowBonusText(float timeBonus)
    {
        // Text'i g�ncelle ve g�ster
        bonusText.text = "+" + timeBonus.ToString("F1") + "s";
        bonusText.enabled = true;

        // Text'i 2 saniye sonra gizle
        yield return new WaitForSeconds(2f);
        bonusText.text = "";
        bonusText.enabled = false;
    }
}

