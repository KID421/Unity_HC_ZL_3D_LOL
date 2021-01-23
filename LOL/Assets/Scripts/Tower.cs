using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("攻擊範圍"), Range(0, 500)]
    public float rangeAtk;
    [Header("攻擊力"), Range(0, 500)]
    public float atk;
    [Header("生成物件")]
    public GameObject psBullet;
    [Header("速度"), Range(0, 5000)]
    public float speedBullet;
    [Header("攻擊圖層")]
    public int layer;
    [Header("冷卻"), Range(0, 5)]
    public float cd;
    [Header("血量"), Range(0, 5000)]
    public float hp = 2000;

    /// <summary>
    /// 血量最大值
    /// </summary>
    private float hpMax;
    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;
    /// <summary>
    /// 血條文字
    /// </summary>
    private Text textHp;
    /// <summary>
    /// 血條
    /// </summary>
    private Image imgHp;
    /// <summary>
    /// 畫布血條
    /// </summary>
    private Transform canvasHp;
    /// <summary>
    /// 是否死亡
    /// </summary>
    private bool dead;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);            // 顏色
        Gizmos.DrawSphere(transform.position, rangeAtk);    // 繪製圓形(中心點，半徑)
    }

    private void Start()
    {
        timer = cd;
        hpMax = hp;
        canvasHp = transform.Find("畫布血條");
        textHp = canvasHp.Find("血條文字").GetComponent<Text>();
        textHp.text = hp.ToString();
        imgHp = canvasHp.Find("血條").GetComponent<Image>();
    }

    private void Update()
    {
        if (dead) return;       // 如果 死亡 就 跳出
        Track();
    }

    /// <summary>
    /// 追蹤：進入的物件
    /// </summary>
    private void Track()
    {
        // 碰撞球體(中心點，半徑，圖層)
        Collider[] hit = Physics.OverlapSphere(transform.position, rangeAtk, 1 << layer);

        // 如果 碰撞物件的數量 大於 零
        if (hit.Length > 0)
        {
            if (timer < cd)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;

                // 生成子彈
                GameObject temp = Instantiate(psBullet, transform.position + Vector3.up * 10, Quaternion.identity);

                Bullet bullet = temp.AddComponent<Bullet>();    // 添加元件<元件名稱>
                bullet.target = hit[0].transform;               // 指定目標
                bullet.speed = speedBullet;                     // 指定速度
                bullet.atk = atk;                               // 指定攻擊力
            }
        }
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收的傷害值</param>
    public void Damage(float damage)
    {
        hp -= damage;
        textHp.text = hp.ToString();
        imgHp.fillAmount = hp / hpMax;

        if (hp <= 0) Dead();
    }
    
    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Dead()
    {
        hp = 0;
        textHp.text = 0.ToString();
        dead = true;
        gameObject.layer = 0;
    }
}
