using UnityEngine;
using UnityEngine.AI;       // 引用 AI API

public class Solder : HeroBase
{
    [Header("對方主堡名稱")]
    public string targetName;
    [Header("停止距離"), Range(0, 10)]
    public float stopDistance = 3;
    [Header("攻擊範圍"), Range(0, 30)]
    public float rangeAttack = 15;
    [Header("敵方的圖層")]
    public int layerEnemy;
    [Header("攻擊射線位移")]
    public Vector3 posAtkOffset;
    [Header("攻擊射線長度"), Range(0, 30)]
    public float lengthAttack = 3;

    /// <summary>
    /// 代理器
    /// </summary>
    private NavMeshAgent agent;
    /// <summary>
    /// 敵方主堡
    /// </summary>
    private Transform castle;
    /// <summary>
    /// 目標
    /// </summary>
    private Transform target;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + posAtkOffset, transform.forward * lengthAttack);
    }

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = stopDistance;

        castle = GameObject.Find(targetName).transform;         // 取得敵方主堡資訊
        target = castle;                                        // 開始時目標設為敵方主堡
    }

    protected override void Update()
    {
        base.Update();
        Move(target);
    }

    protected override void Move(Transform target)
    {
        agent.isStopped = ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊");

        Collider[] hit = Physics.OverlapSphere(transform.position, rangeAttack, 1 << layerEnemy);

        if (hit.Length > 0) this.target = hit[0].transform;                         // 如果 範圍內有敵方 設定為目標
        else this.target = castle;                                                  // 否則 範圍內沒有目標 就設定目標為 敵方主堡

        canvasHp.eulerAngles = new Vector3(65, -90, 0);                             // 角度不變
        agent.SetDestination(this.target.position);                                 // 設定目的地(目標物件)
        ani.SetBool("跑步開關", agent.remainingDistance > agent.stoppingDistance);   // 當 剩餘距離 > 停止距離 時 跑步

        if (agent.remainingDistance <= agent.stoppingDistance) Attack();
    }

    /// <summary>
    /// 普攻
    /// </summary>
    private void Attack()
    {
        if (timer <= data.attackCD)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            ani.SetTrigger("普攻");

            RaycastHit hit;     // 碰撞物件資訊

            // out 將方法資訊儲存在參數內
            // 如果 (物理.射線碰撞(中心點+位移，方向，碰到的物件，長度，圖層)
            if (Physics.Raycast(transform.position + posAtkOffset, transform.forward, out hit, lengthAttack, 1 << layerEnemy))
            {
                if (hit.collider.GetComponent<Tower>()) hit.collider.GetComponent<Tower>().Damage(data.attack);
                if (hit.collider.GetComponent<Solder>()) hit.collider.GetComponent<Solder>().Damage(data.attack);
            }
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);

        if (hp <= 0) Dead(false);
    }
}
