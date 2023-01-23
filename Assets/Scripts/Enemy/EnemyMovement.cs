using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _time;

    private void Start()
    {
        transform.DOMove(_endPoint.position, _time).SetLoops(-1,LoopType.Yoyo);
    }
}
