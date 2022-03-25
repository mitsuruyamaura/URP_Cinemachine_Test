using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

     
public class EnemyBase : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        if(TryGetComponent(out rb)){
            MoveEnemy();
        }
    }

    
    private void MoveEnemy() {
        rb.DOMoveX(Random.Range(5, 10), 1.5f).SetEase(Ease.InQuart).OnComplete(() => MoveEnemy());
    }
}
