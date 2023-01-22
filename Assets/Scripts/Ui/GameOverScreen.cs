using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _textHeader;
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private Button _buttonExit;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
        _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
        _buttonExit.onClick.AddListener(OnButtonExitClicked);
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
        _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
        _buttonExit.onClick.RemoveListener(OnButtonExitClicked);
    }

    private void OnGameOver(string text)
    {
        _textHeader.text = text;
        _canvasGroup.alpha = 1;
        Time.timeScale = 0;
    }

    private void OnButtonRestartClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnButtonExitClicked()
    {
        Application.Quit();
    }
}
