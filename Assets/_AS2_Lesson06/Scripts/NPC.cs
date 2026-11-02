using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [Header("* * * Navigation Settings.")]
    public Transform TargetPoint;           // 目的地のオブジェクト
    public Transform[] PatrolPoints;        // 巡回ポイントの配列
    public NavMeshAgent Agent;              // NavMeshAgentコンポーネント

    private int _currentPatrolIndex = 0;    // 現在の巡回ポイントの番号


    void Start()
    {
        // NavMeshAgentコンポーネントの目的地を設定
        // Agent.SetDestination(目的地の3次元ベクトル);
        Agent.SetDestination(TargetPoint.position);
    }


    void Update()
    {
        /*
         * プレイヤーが近くにいるときはプレイヤーを追いかける
         * 一定距離離れると巡回に戻る
         */

        if(FinishNavigation())
        {
            NextPoint();  // 次の巡回ポイントを設定
        }

        SearchPlayer();  // プレイヤーを探して追跡
    }


    /// <summary>
    /// 目的地に到着しました
    /// </summary>
    public bool FinishNavigation()
    {
        // 目的地までの距離を計算
        float distance = Vector3.Distance(TargetPoint.position, transform.position);

        // 一定距離まで近づいたら...
        if(distance < 1.5f)
        {
            return true;
        }

        // まだ、目的地ではない
        return false;
    }


    public void NextPoint()
    {
        // 配列が設定されていない場合は処理を終了
        if (PatrolPoints == null ||
            PatrolPoints.Length <= 0) return;

        _currentPatrolIndex++;  // 次の巡回ポイントの番号を設定

        // 巡回ポイント配列の番号によって TargetPoint を設定
        TargetPoint = PatrolPoints[_currentPatrolIndex % PatrolPoints.Length];

        // MavMeshAgentコンポーネントの目的地を設定
        Agent.SetDestination(TargetPoint.position);
    }


    public void SearchPlayer()
    {
        Player player = GameObject.FindAnyObjectByType<Player>();
        if(player == null)
        {
            Debug.LogError("Player が存在しません。");
            return;
        }

        Vector3 targetPoint = player.transform.position;
        // Player との距離を計算
        float distance = Vector3.Distance(targetPoint, transform.position);

        // 一定距離まで近づいたら...
        if(distance < 3.0f)
        {
            TargetPoint = player.transform;  // 目的地を Player に設定
        }
        else
        {
            NextPoint();  // 巡回ポイントに戻る
        }

        // NavMeshAgentコンポーネントの目的地を設定
        Agent.SetDestination(TargetPoint.position);
    }
}
