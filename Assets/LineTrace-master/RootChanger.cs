using UnityEngine;
using LineTrace;

/// <summary>
/// ルートの設定用クラス
/// </summary>
public class RootChanger : MonoBehaviour
{
    [SerializeField, Header("ルートごとに登録")]
    private LineManager[] lineManagers;


    void Start() {
        // デバッグ用に、ランダムなルートを１つ選択してルートを設定
        SetLine(Random.Range(0, lineManagers.Length));
    }

    /// <summary>
    /// ルートの設定
    /// </summary>
    /// <param name="lineIndex"></param>
    public void SetLine(int lineIndex) {

        // 利用する LineManager を１つ設定し、それをルートとする
        for (int i = 0; i < lineManagers.Length; i++) {
            if (i == lineIndex) {
                lineManagers[i].gameObject.SetActive(true);
            } else {
                lineManagers[i].gameObject.SetActive(false);
            }
        }
    }
}