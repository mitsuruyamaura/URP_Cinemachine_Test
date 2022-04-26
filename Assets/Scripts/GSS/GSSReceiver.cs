using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

/// <summary>
/// スプレッドシートから取得したデータをシート単位で任意のスクリプタブル・オブジェクトに値として取り込む
/// </summary>
[RequireComponent(typeof(GSSReader))]
public class GSSReceiver : MonoBehaviour {

    public static GSSReceiver instance;

    public bool IsLoading { get; set; }


    private void Awake() {   // Awake で待機したい場合には async + UniTask にする。UniTaskVoid にしても await すれば終了まで待機するが、他のクラスは動いてしまう

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        Debug.Log("GSS データ取得開始");

        // GSS のデータ取得準備
        PrepareGSSLoadStartAsync().Forget();

        Debug.Log("Awake 終了");
    }

    /// <summary>
    /// GSS のデータ取得準備
    /// </summary>
    /// <returns></returns>
    private async UniTask PrepareGSSLoadStartAsync() {

        IsLoading = true;

        // 読み込んでない場合には、GSS を取得して SO に取得する
        await GetComponent<GSSReader>().GetFromWebAsync();

        IsLoading = false;

        Debug.Log("GSS データを SO に取得");
    }

    /// <summary>
    /// インスペクターから GSSReader の OnLoadEnd にこのメソッドを追加することで GSS の読み込み完了時にコールバックされる
    /// </summary>
    public void OnGSSLoadEnd() {
        GSSReader reader = GetComponent<GSSReader>();

        // スプレッドシートから取得した各シートの配列を List に変換
        List<SheetData> sheetDataslist = reader.sheetDatas.ToList();

        // 情報が取得できた場合
        if (sheetDataslist != null) {

            // スクリプタブル・オブジェクトに代入
            DataBaseManager.instance.enemyDataSO.enemyDataList =
                new List<EnemyData>(sheetDataslist.Find(x => x.SheetName == SheetName.EnemyData).DatasList.Select(x => new EnemyData(x)).ToList());

            // TODO 他の SO を追加する

        }
    }
}

