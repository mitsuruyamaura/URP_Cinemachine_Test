using System;
using UnityEngine;

public class Rader : MonoBehaviour
{
    private AgentEnemy agentEnemy;
    


    private void Start() {
        transform.parent.TryGetComponent(out agentEnemy);
    }

    private void Update() {
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerController player)) {
            agentEnemy.SetTarget(player.transform);
            Debug.Log("ターゲット発見: " + player.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out PlayerController _)) {
            agentEnemy.ReleaseTarget();
        }
    }
}