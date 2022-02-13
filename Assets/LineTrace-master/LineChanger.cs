using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineTrace;

public class LineChanger : MonoBehaviour
{
    public LineManager[] lineManagers;


    void Start() {
        // �f�o�b�O�p
        SetLine(Random.Range(0, lineManagers.Length));
    }

    /// <summary>
    /// �i�s�����̐ݒ�
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

