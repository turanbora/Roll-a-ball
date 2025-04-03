using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    public GameObject textPrefab;  // Oluþturulan text prefabýný buraya atayýn
    public float displayTime = 2f; // Text'in ne kadar süre görüneceði

    public void ShowText(Vector3 position, string message)
    {
        GameObject textObject = Instantiate(textPrefab, position, Quaternion.identity);
        TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();

        if (textMesh != null)
        {
            textMesh.text = message;
            StartCoroutine(FadeOutText(textObject));
        }
    }

    private IEnumerator FadeOutText(GameObject textObject)
    {
        TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();
        Color originalColor = textMesh.color;

        float elapsed = 0f;
        while (elapsed < displayTime)
        {
            elapsed += Time.deltaTime;
            textMesh.color = Color.Lerp(originalColor, Color.clear, elapsed / displayTime);
            yield return null;
        }

        Destroy(textObject);
    }
}
