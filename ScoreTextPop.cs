using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreTextPop : MonoBehaviour
{
    public float displayTime = 1f; // Text'in ekranda ne kadar s�re kalaca��n� belirleyin.
    private TMP_Text textMesh;

    void Start()
    {
        // "Move Text" Canvas'�ndaki TMP_Text bile�enini al
        textMesh = GetComponentInChildren<TMP_Text>();
        if (textMesh == null)
        {
            Debug.LogError("TMP_Text bile�eni bulunamad�!");
        }
    }

    public void ShowScoreText(Vector3 position)
    {
        // Text'in pozisyonunu k�renin konumuna ayarla
        transform.position = position;

        // '+0.5s' yaz�s�n� ayarla ve g�ster
        textMesh.text = "+0.5s";
        textMesh.enabled = true;

        // Text'i belirli bir s�re sonra gizle
        StartCoroutine(HideTextAfterTime());
    }

    private IEnumerator HideTextAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        textMesh.enabled = false;
    }
}
