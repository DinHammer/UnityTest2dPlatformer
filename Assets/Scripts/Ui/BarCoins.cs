using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarCoins : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _textCoin;

    private void OnEnable()
    {
        _player.CoinsChanged += OnCoinsChanged;
    }

    private void OnDisable()
    {
        _player.CoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _textCoin.text = $"Монеток: {coins}";
    }
    
    
}
