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

            // 0 以下にならないように制御
            // 通常の UI 更新
            UpdateDisplayGameTime(Mathf.Max(0, --gameTime));

            // DOTween の場合
            //UpdateDisplayGameTime(gameTime, Mathf.Max(0, --gameTime));
        }
    }

    /// <summary>
    /// 通常の更新
    /// </summary>
    /// <param name="time"></param>
    public void UpdateDisplayGameTime(int time) {
        txtGameTime.text = time.ToString();
    }

    /// <summary>
    /// DOTween 利用時
    /// </summary>
    /// <param name="oldtime"></param>
    /// <param name="newTime"></param>
    public void UpdateDisplayGameTime(int oldtime, int newTime, float duration = 0.25f) {
        txtGameTime.DOCounter(oldtime, newTime, duration).SetEase(Ease.InQuart);
    }
}
