using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public EnemyDataSO enemyDataSO;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }   
    }

    /// <summary>
    /// �w�肵���ԍ��� EnemyData �擾
    /// </summary>
    /// <param name="searchEnemyno"></param>
    /// <returns></returns>
    public EnemyData GetEnemyDataFromNo(int searchEnemyno) {
        return enemyDataSO.enemyDataList.Find(x => x.no == searchEnemyno);
    }
}
