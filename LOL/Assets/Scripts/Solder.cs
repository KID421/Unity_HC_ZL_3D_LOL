using UnityEngine;
using UnityEngine.AI;       // 引用 AI API

public class Solder : HeroBase
{
    /// <summary>
    /// 代理器
    /// </summary>
    private NavMeshAgent agent;

    [Header("對方主堡名稱")]
    public string targetName;
    [Header("停止距離"), Range(0, 10)]
    public float stopDistance = 3;
    [Header("攻擊範圍"), Range(0, 30)]
    public float rangeAttack = 15;
    [Header("敵方的圖層")]
    public int layerEnemy;

    private Transform target;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = stopDistance;

        target = GameObject.Find(targetName).transform;
    }

    protected override void Update()
    {
        base.Update();
        Move(target);
    }

    protected override void Move(Transform target)
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, rangeAttack, 1 << layerEnemy);

        if (hit.Length > 0) this.target = hit[0].transform;                         // 如果 範圍內有敵方 設定為目標

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
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);

        if (hp <= 0) Dead(false);
    }
}
