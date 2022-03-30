using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtGameTime;

    [SerializeField]
    private int gameTime;

    private float timer;


    void Update() {
        if (gameTime <= 0) {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1.0f) {
            timer = 0;

            // 0 �ȉ��ɂȂ�Ȃ��悤�ɐ���
            // �ʏ�� UI �X�V
            UpdateDisplayGameTime(Mathf.Max(0, --gameTime));

            // DOTween �̏ꍇ
            //UpdateDisplayGameTime(gameTime, Mathf.Max(0, --gameTime));
        }
    }

    /// <summary>
    /// �ʏ�̍X�V
    /// </summary>
    /// <param name="time"></param>
    public void UpdateDisplayGameTime(int time) {
        txtGameTime.text = time.ToString();
    }

    /// <summary>
    /// DOTween ���p��
    /// </summary>
    /// <param name="oldtime"></param>
    /// <param name="newTime"></param>
    public void UpdateDisplayGameTime(int oldtime, int newTime, float duration = 0.25f) {
        txtGameTime.DOCounter(oldtime, newTime, duration).SetEase(Ease.InQuart);
    }
}
