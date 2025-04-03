using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera menuCamera;
    public Canvas gameTimeCanvas;
    public Canvas menuCanvas;

    private void Start()
    {
        // Oyun ba�lad���nda sadece mainCamera ve gameTimeCanvas aktif
        SwitchToGame();
    }

    public void SwitchToGame()
    {
        // Oyuna ge�i�
        mainCamera.enabled = true;
        menuCamera.enabled = false;

        gameTimeCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void SwitchToMenu()
    {
        // Men�ye ge�i�
        mainCamera.enabled = false;
        menuCamera.enabled = true;

        gameTimeCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
}
