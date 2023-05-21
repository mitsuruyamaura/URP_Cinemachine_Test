using UnityEngine;
using UnityEngine.AI;

public class AgentEnemy_NonBase : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform targetTran;

    private Vector3 enemyStartPos;
    
    [SerializeField] private float speed;
    [SerializeField] private bool isReturnStartPos;

    
    void Start() {

        if (TryGetComponent(out agent)) {

            // 移動速度の設定
            agent.speed = speed;
            
            // スタート地点の登録
            enemyStartPos = transform.position;
            
            Debug.Log("Agent 初期設定 完了");
            
        }
    }


    void Update()
    {
        if (!agent) {
            return;
        }

        if (!targetTran) {
            return;
        }
        
        agent.destination = targetTran.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerController player)) {
            targetTran = player.transform;
            Debug.Log("ターゲット発見: " + targetTran.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out PlayerController player)) {
            targetTran = null;
            
            // その場で移動を停止
            agent.ResetPath();
            Debug.Log("ターゲット消失: " + player.name);

            // スタート地点へ戻る設定がある場合
            if (isReturnStartPos) {
                
                // 敵をスタート地点へ戻す
                agent.destination = enemyStartPos;
            }
        }
    }
}
