using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGimmick : GimmickBase
{
    [SerializeField]
    private EnemyBase[] enemyPrefabs;   // あとでスクリプタブル・オブジェクトに変える

    [SerializeField]
    private Transform[] targetTrans;

    [SerializeField]
    private float duration;

    private List<EnemyBase> enemyList = new List<EnemyBase>();


    public override void TriggerGimmick() {
        GetComponent<Collider>().enabled = false;

        for (int i = 0; i < targetTrans.Length; i++) {

            Vector3 scale = enemyPrefabs[i].transform.localScale;
            enemyPrefabs[i].transform.localScale = Vector3.zero;
            EnemyBase enemy = Instantiate(enemyPrefabs[i], targetTrans[i].position, Quaternion.identity);
            enemy.transform.DOScale(scale, duration).SetEase(Ease.OutBack);
        }
    }
}
