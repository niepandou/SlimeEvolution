using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private PlayerInputControl playerInput;
    public GameObject skillUI;
    public GameObject deadUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        playerInput = new PlayerInputControl();
        
        //暂停面板
    }

    private void OnEnable()
    {
        playerInput.UI.Pause.started += ShowSkillUI;
    }

    private void ShowSkillUI(InputAction.CallbackContext obj)
    {
        if (!skillUI.activeInHierarchy)
        {
            skillUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            skillUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ReStartGame()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

    public void ShowDeadUI()
    {
        DisableAllUI();
        deadUI.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void DisableAllUI()
    {
        skillUI.SetActive(false);
        deadUI.SetActive(false);
    }
    
}
