using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;

    [SerializeField] private float _time;

    private void Start()
    {
        transform.DOMove(_endPoint.position, _time).SetLoops(-1,LoopType.Yoyo);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            player.FinishGame();
        }
    }
}
