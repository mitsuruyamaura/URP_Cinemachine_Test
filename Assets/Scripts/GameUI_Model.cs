using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameUI_Model : MonoBehaviour
{
    public ReactiveProperty<int> GameTime = new ReactiveProperty<int>(0);  // 初期値を入れておくことで、コストラクタを使って値の初期化が出来ます

    /// <summary>
    /// 値更新用 
    /// </summary>
    /// <param name="newTime"></param>
    public void UpdateGameTime(int newTime) {
        GameTime.Value = newTime;
    }
}