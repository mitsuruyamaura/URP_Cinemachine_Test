using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameUI_View : MonoBehaviour {

    [SerializeField]
    private Text txtGameTime;


    /// <summary>
    /// í èÌÇÃçXêV
    /// </summary>
    /// <param name="time"></param>
    public void UpdateDisplayGameTime(int time) {
        txtGameTime.text = time.ToString();
    }

    /// <summary>
    /// DOTween óòópéû
    /// </summary>
    /// <param name="oldtime"></param>
    /// <param name="newTime"></param>
    public void UpdateDisplayGameTime(int oldtime, int newTime, float duration = 0.25f) {
        txtGameTime.DOCounter(oldtime, newTime, duration).SetEase(Ease.InQuart);
    }
}