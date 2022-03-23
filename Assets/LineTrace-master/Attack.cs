using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Attack : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        TryGetComponent(out anim);

        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Fire1"))
            .ThrottleFirst(System.TimeSpan.FromSeconds(1.5f))
            .Subscribe(_ => PrepareAttack())
            .AddTo(gameObject);
    }

    
    private void PrepareAttack() {
        anim.SetTrigger("Attack");
    }

    private void Hit() {
        
    }
}
