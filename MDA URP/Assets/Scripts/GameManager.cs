using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameMenuCanvas;
    public bool GameMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        OnEscape(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameMenuOpen)
        {
            OnEscape(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameMenuOpen)
        {
            OnEscape(false);
        }
    }

    public void OnEscape(bool paused)
    {
        ChangeCursorMode(paused);
        GameMenuMode(paused);
    }

    private void GameMenuMode(bool mode)
    {
        if (mode)
        {
            GameMenuCanvas.gameObject.SetActive(true);
            GameMenuOpen = true;
        }
        else
        {
            GameMenuCanvas.gameObject.SetActive(false);
            GameMenuOpen = false;
        }
    }

    private void ChangeCursorMode(bool unlocked)
    {
        if (unlocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
