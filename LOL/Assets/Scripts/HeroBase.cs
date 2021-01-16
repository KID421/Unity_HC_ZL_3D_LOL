using UnityEngine;

public class HeroBase : MonoBehaviour
{
    [Header("角色資料")]
    public HeroData data;

    /// <summary>
    /// 動畫控制器
    /// </summary>
    private Animator ani;
    /// <summary>
    /// 技能計時器：累加時間用
    /// </summary>
    protected float[] skillTimer = new float[4];
    /// <summary>
    /// 技能是否開始
    /// </summary>
    protected bool[] skillStart = new bool[4];

    private Rigidbody rig;

    // protected 保護 - 允許子類別存取
    // virtual 虛擬 - 允許子類別複寫
    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        TimerControl();
    }

    /// <summary>
    /// 受傷
    /// </summary>
    public void Damage(float damage)
    {
        data.hp -= damage;
    }

    private void TimerControl()
    {
        for (int i = 0; i < 4; i++)
        {
            if (skillStart[i])
            {
                skillTimer[i] += Time.deltaTime;

                // 如果 計時器 >= 冷卻時間 就 歸零並且設定為 尚未開始
                if (skillTimer[i] >= data.skills[i].cd)
                {
                    skillTimer[i] = 0;
                    skillStart[i] = false;
                }
            }
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="target">要前往的目標位置</param>
    public void Move(Transform target)
    {
        Vector3 pos = rig.position;
        rig.MovePosition(target.position);                  // 剛體.移動座標(座標)
        transform.LookAt(target);                           // 看向(目標物件)
        ani.SetBool("跑步開關", rig.position != pos);        // 動畫.設定布林值(跑步參數，現在座標 不等於 前面座標)
    }

    public void Skill1()
    {
        // 如果 技能已經開始 就跳出
        if (skillStart[0]) return;
        ani.SetTrigger("第一招");
        skillStart[0] = true;
    }

    public void Skill2()
    {
        if (skillStart[1]) return;
        ani.SetTrigger("第二招");
        skillStart[1] = true;
    }

    public void Skill3()
    {
        if (skillStart[2]) return;
        ani.SetTrigger("第三招");
        skillStart[2] = true;
    }

    public void Skill4()
    {
        if (skillStart[3]) return;
        ani.SetTrigger("大絕");
        skillStart[3] = true;
    }
}
