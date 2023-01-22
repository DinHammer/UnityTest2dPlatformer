using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _reward;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            player.TakeCoin(_reward);
            TakeCoin();
        }
    }

    private void TakeCoin()
    {
        Destroy(gameObject);
    }
}
