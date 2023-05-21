using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{

    /// <summary>
    /// 最小値を持つ要素を返します
    /// 戻り値の TSource の型は Line 型
    /// </summary>
    /// <param name="self">IEnumerable<Line> 型</param>
    /// <param name="selector">引数に渡されてきたデリゲート</param>
    /// <typeparam name="TSource">Line 型</typeparam>
    /// <typeparam name="TResult">float 型</typeparam>
    /// <returns></returns>
    public static TSource FindMin<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, TResult> selector)
    {
        // line => selector(line).Equals(self.Min(selector)) の処理結果の中から、最小値を持つ Line を探し、その後、First で取り出す 
        // FirstOfDefault を使っていないので、self が null だとエラーで止まる 
        return self.First(line => selector(line).Equals(self.Min(selector)));
        
        // 詳細な解説
        // このコードは、ジェネリックメソッド FindMin<TSource, TResult> であり、拡張メソッドとして定義されている
        
        // このメソッドは、与えられた IEnumerable<TSource> オブジェクトから、
        // 指定された selector デリゲートによって生成される値が最小となる要素を検索して返す
        
        // self = IEnumerable<Line> 型。この場合、List<Line> 型
        
        // selector = デリゲート。Func<TSource, TResult>は、TSource型の引数を1つ受け取り、TResult型の値を返すメソッド
        
        // この部分が、実行元から届く処理
        // line => // line(Line型) が TSource として渡される
        // {
        //     // FindMin に渡す selector デリゲート部分
        //     {
        //         // 各 Line オブジェクトの中心点を見つける
        //         var center = Vector3.Lerp(line.back, line.front, 0.5F);
        //
        //         // position 引数との距離を計算し、絶対値として返す(return している float 型の値が TResult として渡される)
        //         // この return は selector デリゲートの戻り値として使われる情報
        //         return Mathf.Abs(Vector3.Distance(position, center)); // ← TResult 型は float 型
        //     }
        // });
        
        // この場合、Func の TSource型はLineであり、TResult型はfloat
        // line 変数は LineList から取り出した各 Line が要素として使われる
        // それを selector メソッドに引数として使う
        
        // selector デリゲートは、Line 型の引数を1つ受け取り、その Line オブジェクトから float 値を生成する匿名メソッド
        // この場合、selector デリゲートは、var center = Vector3.Lerp(l.back, l.front, 0.5F); を実行し、
        // その結果として中心点の座標を表す float 値を絶対値として返す

        // また、self.Min(selector) は、self(List<Line>) の要素に対して selector デリゲートを適用した結果の最小値を返す
        // Min メソッドは引数に指定した List の中から最小値を見つける
        
        // Equals は、selector(line) の結果である float の値と、List<Line> 内の最小値(self.Min(selector))が等しいかどうかを比較している
        
        // よって、line => selector(line).Equals(self.Min(selector)) は、各要素 line に対して selector を適用した結果が、
        // self の最小値(self.Min(selector))と等しいかどうか(Equals)を調べる式。この式が真となる最初の要素が、self から取り出されて返す(First)
    }

    /// <summary>
    /// 最大値を持つ要素を返します
    /// </summary>
    public static TSource FindMax<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, TResult> selector)
    {
        // FirstOfDefault を使っていないので、self が null だとエラーで止まる 
        return self.First(c => selector(c).Equals(self.Max(selector)));
    }
}