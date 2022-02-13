using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineTrace;

public class LineChanger : MonoBehaviour
{
    public LineManager[] lineManagers;


    void Start() {
        // デバッグ用
        SetLine(Random.Range(0, lineManagers.Length));
    }

    /// <summary>
    /// 進行方向の設定
    /// </summary>
    /// <param name="lineIndex"></param>
    public void SetLine(int lineIndex) {
        for (int i = 0; i < lineManagers.Length; i++) {
            if (i == lineIndex) {
                lineManagers[i].gameObject.SetActive(true);
            } else {
                lineManagers[i].gameObject.SetActive(false);
            }
        }
    }
}

