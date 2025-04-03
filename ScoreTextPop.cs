using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreTextPop : MonoBehaviour
{
    public float displayTime = 1f; // Text'in ekranda ne kadar süre kalacaðýný belirleyin.
    private TMP_Text textMesh;

    void Start()
    {
        // "Move Text" Canvas'ýndaki TMP_Text bileþenini al
        textMesh = GetComponentInChildren<TMP_Text>();
        if (textMesh == null)
        {
            Debug.LogError("TMP_Text bileþeni bulunamadý!");
        }
    }

    public void ShowScoreText(Vector3 position)
    {
        // Text'in pozisyonunu kürenin konumuna ayarla
        transform.position = position;

        // '+0.5s' yazýsýný ayarla ve göster
        textMesh.text = "+0.5s";
        textMesh.enabled = true;

        // Text'i belirli bir süre sonra gizle
        StartCoroutine(HideTextAfterTime());
    }

    private IEnumerator HideTextAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        textMesh.enabled = false;
    }
}
