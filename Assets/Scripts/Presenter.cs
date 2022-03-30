using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Presenter : MonoBehaviour
{
    [SerializeField]
    private GameUI_Model model;

    [SerializeField]
    private GameUI_View view;

    [SerializeField]
    private int gameTime;

    private float timer;


    void Start()
    {
        // ゲーム時間の初期値設定
        if (gameTime != 0) {
            model.GameTime.Value = gameTime;
        }

        // GameTime を監視 通常の場合(挙動に問題なし)
        //model.GameTime.Subscribe(x => view.UpdateDisplayGameTime(x)).AddTo(gameObject);

        // GameTime を監視 DOTween の場合
        model.GameTime.Zip(model.GameTime.Skip(1), (oldTime, newTime) => (oldTime, newTime)).Subscribe(x => view.UpdateDisplayGameTime(x.oldTime, x.newTime)).AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => model.GameTime.Value > 0)     // GameTime の値が 0 以上の場合のみ下の処理に行く
            .Subscribe(_ =>                           // Update で実装したい内容を書く
            { 
                timer += Time.deltaTime;
                if (timer >= 1.0f) {
                    timer = 0;

                    // 0 以下にならないように制御
                    // 直接値を変える場合(ReactiveProperty はプロパティなのでこれでも OK)
                    model.GameTime.Value = Mathf.Max(0, --model.GameTime.Value);

                    // メソッドを使う場合(挙動に問題なし)
                    //model.UpdateGameTime(Mathf.Max(0, --model.GameTime.Value));
                }
            })
            .AddTo(gameObject);
    }    
}
