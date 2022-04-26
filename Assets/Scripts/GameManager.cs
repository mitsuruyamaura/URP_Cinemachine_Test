using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private SkyboxChanger skyboxChanger;

    [SerializeField]
    private int gameTime;

    private float timer;


     async UniTask Start() {  // Awake 内で await してもここは待たない。そのため、取得よりも早く動くので、一旦止める必要がある

        SoundManager.instance.PlayBGM(BgmType.Main);

        var token = this.GetCancellationTokenOnDestroy();

        // データ取得まで待機させる
        await UniTask.WaitUntil(() => !GSSReceiver.instance.IsLoading);

        Debug.Log("ゲーム開始");

        skyboxChanger.ChangeSkybox();
    }


    void Update() {
        if (gameTime <= 0) {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1.0f) {
            timer = 0;

            // 0 以下にならないように制御
            // 通常の UI 更新
            uiManager.UpdateDisplayGameTime(Mathf.Max(0, --gameTime));

            // DOTween の場合(挙動に問題なし)
            //uiManager.UpdateDisplayGameTime(gameTime, Mathf.Max(0, --gameTime));
        }
    }
}
