using UnityEngine;
using UnityEngine.AI;

public class AgentEnemy : EnemyBase
{
    private NavMeshAgent agent;
    private Transform targetTran;
    private Vector3 enemyStartPos;
    
    [SerializeField] private float speed;
    [SerializeField] private bool isReturnStartPos;
    
    
    protected override void Start() {
        base.Start();
        
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

    /// <summary>
    /// 追跡対象の設定
    /// </summary>
    /// <param name="player"></param>
    public void SetTarget(Transform player) {
        targetTran = player;
        if(anim) anim.SetBool("Walk Forward", true);
        
        Debug.Log("ターゲット設定: " + targetTran.name);
    }

    /// <summary>
    /// 追跡対象の開放
    /// </summary>
    public void ReleaseTarget() {
        
        Debug.Log("ターゲット消失: " + targetTran.name);
        
        targetTran = null;
        
        if(anim) anim.SetBool("Walk Forward", false);
            
        // その場で移動を停止
        agent.ResetPath();

        // スタート地点へ戻る設定がある場合
        if (isReturnStartPos) {
                
            // 敵をスタート地点へ戻す
            agent.destination = enemyStartPos;
        }
    }
}
