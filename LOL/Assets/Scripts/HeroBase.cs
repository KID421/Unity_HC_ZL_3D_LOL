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
    private float[] skillTimer = new float[4];
    /// <summary>
    /// 技能是否開始
    /// </summary>
    private bool[] skillStart = new bool[4];

    // protected 保護 - 允許子類別存取
    // virtual 虛擬 - 允許子類別複寫
    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public void Move()
    {

    }

    public void Skill1()
    {
        ani.SetTrigger("第一招");
    }

    public void Skill2()
    {
        ani.SetTrigger("第二招");
    }

    public void Skill3()
    {
        ani.SetTrigger("第三招");
    }

    public void Skill4()
    {
        ani.SetTrigger("大絕");
    }
}
