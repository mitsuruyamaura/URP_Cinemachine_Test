using System;
using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.UI;

namespace LineTrace
{
    public class LineManager : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("循環させるか")] private bool cycle = false;

        public List<Transform> waypoints;
        List<Line> lineList = new List<Line>();

        void Awake() {
            
            // UniRx で Line を作成できないので、ここで作成する(ループはさせていない)
            var wp = waypoints.Select(w => w.position).ToList();
            
            var back = wp.First();
            var front = wp.Last();
            Line l = new Line(front, back);
            if (lineList.Count > 0)
            {
                lineList.Last().next = l;
                l.prev = lineList.Last();
            }
            lineList.Add(l);
            
            // Subscribe が引数のオーバーロードで競合するので利用していない。
            // UniRx と System で競合する
            waypoints.Select(w => w.position) // w には Transform が入っており、それを Select を使って Position に変換。Linq の Select と同じ機能
                .ToObservable()  // IEnumerable<T>.ToObservable。今回はList から Observable に変換。イテレータから値を順番に発行する Observable に変換できる
                .Buffer(2, 1)    // 個数とスキップ数を指定してまとめる。今回の場合(2, 1)なので、１つ前の List の値とのペアを作る
                .Take(waypoints.Count - 1) // 先頭から指定した個数だけ OnNext メッセージを通過させる。メッセージ発行後に OnCompleted メッセージを発行してObservableを完了状態にする
                //.Subscribe(wp =>
                //{
                //    var back = wp.First();
                //    var front = wp.Last();
                //    Line l = new Line(front, back);
                //    if (lineList.Count > 0)
                //    {
                //        lineList.Last().next = l;
                //        l.prev = lineList.Last();
                //    }
                //    lineList.Add(l);
                //}, () =>
                //{
                //    // 循環させる場合、最初と最後を繋ぐ線を追加する
                //    if (cycle)
                //    {
                //        Line cycleLine = new Line(waypoints.First().position, waypoints.Last().position);
                //        cycleLine.next = lineList.First();
                //        cycleLine.prev = lineList.Last();
                //        lineList.First().prev = cycleLine;
                //        lineList.Last().next = cycleLine;
                //    }
                //})
        ;
        }

        /// <summary>
        /// 一番近い位置にあるLINEを返す
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Line GetLineAtNearDistance(Vector3 position)
        {
            //Debug.Log(lineList.Count);
            
            // lineList内で最小の距離を持つLineオブジェクトを返すためにFindMinメソッドを使用
            // FindMinは、lineList の各要素 (Line 型 の line 変数) に対して Lineオブジェクトの中心点とposition引数との距離を計算し、
            // その距離の絶対値を返すデリゲートを引数として受け取る(selector デリゲート部分)
            // そして、最終的にGetLineAtNearDistanceメソッドは、FindMinメソッドによって見つかったLineオブジェクトを返す
            return lineList.FindMin(line => // line(Line型) が TSource として渡される
            {
                // FindMin に渡す selector デリゲート部分
                {
                    // 各 Line オブジェクトの中心点を見つける
                    var center = Vector3.Lerp(line.back, line.front, 0.5F);

                    // position 引数との距離を計算し、絶対値として返す(return している float 型の値が TResult として渡される)
                    // この return は selector デリゲートの戻り値として使われる情報
                    return Mathf.Abs(Vector3.Distance(position, center));
                }
            });
            
            // 上記の処理を拡張メソッドである FindMin を使わずに書くと、以下のようになる
            
            // 計算用の値と初期値を用意
            float minDistance = float.MaxValue;
            Line minLine = null;

            // ① を使って要素を取り出し、②を処理する
            // ②の戻り値である float の値に対して ③Equals を実行し、④lineList (self)内の float の最小値(Min(selector))と比較する
            //①line => ②selector(line).③Equals(④self.Min(selector))
            
            // List 内を走査(FindMin メソッドで実行している処理)
            foreach (Line line in lineList)  // ①foreachのループ内の「Line line」部分が ①line => に該当する部分
            {
                // ②selector デリゲートの処理(中身)
                // 各 Line オブジェクトの中心点を見つける
                Vector3 center = Vector3.Lerp(line.back, line.front, 0.5f);
                
                // position 引数との距離を計算し、絶対値として返す(return している float 型の値が TResult として渡される)
                // この return は selector デリゲートの戻り値として使われる情報
                float distance = Mathf.Abs(Vector3.Distance(position, center));  // ← この値が selector の戻り値
                
                // ③最小値を探索するために、selectorの戻り値を比較する(Equals に当たる処理)
                // ④lineList内(self)に対して Min メソッドを実行し、selectorの戻り値(float 型)の最小値と比較する(self.Min(selector))
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minLine = line;
                }
            }
            if (minLine == null)
            {
                // 最小値が見つからなかった場合の処理
                return null;
            }
            
            // 最小値が見つかった場合の処理(self.First の部分)
            return lineList.First();
        }

        #region Scene上で線を表示する

        public Color gizmoColor = Color.yellow;

        void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            
            // Gizmo を出していない。ここも競合するので使っていない
            waypoints.Select(w => w.position)
                .ToObservable()
                .Buffer(2, 1)
                // .Subscribe(wp =>
                //     {
                //         Gizmos.DrawLine(wp.First(), wp.Last());
                //         Gizmos.DrawSphere(wp.First(), 0.3F);
                //     }
                //     , () =>
                //     {
                //         if (cycle)
                //         {
                //             Gizmos.DrawLine(waypoints.Last().position, waypoints.First().position);
                //         }
                //     })
                ;
        }

        #endregion
    }
}