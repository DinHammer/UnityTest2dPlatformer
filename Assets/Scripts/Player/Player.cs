using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxCoins;
    
    private int _coins;

    public event UnityAction<int> CoinsChanged;
    public event UnityAction<string> GameOver; 

    private void Start()
    {
        InvokeCoinsChange(_coins);
    }
    
    private void InvokeCoinsChange(int value)
    {
        CoinsChanged?.Invoke(value);
    }

    public void FinishGame(string text = "Гамовер")
    {
        GameOver?.Invoke(text);
    }
    
    public void TakeCoin(int reward)
    {
        _coins += reward;
        InvokeCoinsChange(_coins);
        
        if (_coins >= _maxCoins)
        {
            FinishGame("Вы прошли игру");
        }
    }
}
