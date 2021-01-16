using UnityEngine;

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

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);            // 顏色
        Gizmos.DrawSphere(transform.position, rangeAtk);    // 繪製圓形(中心點，半徑)
    }

    private void Start()
    {
        timer = cd;
    }

    private void Update()
    {
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
}
