using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GimmickBase : MonoBehaviour {


    public virtual void SetUpGimmick() {

    }


    public virtual void TriggerGimmick() {

    }


    protected virtual void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Move move)) {
            TriggerGimmick();
        }
    }
}
