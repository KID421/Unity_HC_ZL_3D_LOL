using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 目標
    /// </summary>
    public Transform target;
    /// <summary>
    /// 追蹤速度
    /// </summary>
    public float speed;
    /// <summary>
    /// 攻擊力
    /// </summary>
    public float atk;

    /// <summary>
    /// 追蹤目標
    /// </summary>
    private void Track()
    {
        Vector3 posA = target.position;
        Vector3 posB = transform.position;

        posB = Vector3.Lerp(posB, posA, Time.deltaTime * 0.5f * speed);
        transform.position = posB;

        // 如果 距離 < 1 就造成傷害並刪除子彈
        if (Vector3.Distance(posB, posA) < 1)
        {
            target.GetComponent<HeroBase>().Damage(atk);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Track();
    }
}
