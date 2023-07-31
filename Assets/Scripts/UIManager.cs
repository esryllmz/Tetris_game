using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool oyunDurdumu = false;
    
    public GameObject pausePanel;
    
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanelAcKapat();
        }
    }

    public void PausePanelAcKapat()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        
        oyunDurdumu = !oyunDurdumu;


        if (pausePanel)
        {
            pausePanel.SetActive(oyunDurdumu);
            
            if (SoundManager.instance)
            {
                SoundManager.instance.SesEfektiCikar(0);
                Time.timeScale = (oyunDurdumu) ? 0 : 1;
                
            }
        }
    }

    public void YenidenOynaFNC()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    
    
}
