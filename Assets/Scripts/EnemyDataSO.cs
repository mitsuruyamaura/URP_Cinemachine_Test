using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Create EnemyDataSO")]
public class EnemyDataSO : MonoBehaviour
{
    public List<EnemyData> enemyDataList = new List<EnemyData>();
}