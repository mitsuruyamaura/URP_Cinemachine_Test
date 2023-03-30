using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TakeSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        var array = new[] { 1, 3, 4, 7, 2, 5, 9};
        array.ToObservable()
            //.Take(3)       // Take の場合、1,3,4,OnComplete とログが出る
            //.TakeLast(3)  // TakeLast(3) の場合、2,5,9,OnComplete とログが出る
            .Take(3)
            .Subscribe(x => Debug.Log(x), () => Debug.Log("OnComplete")); // Take と TakeLast を続けて書いた場合、最初の方が優先される
    }
}
