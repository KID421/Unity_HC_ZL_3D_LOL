using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("要追蹤的物件")]
    public Transform target;
    [Header("追蹤速度")]
    public float speed = 3;

    /// <summary>
    /// 追蹤
    /// </summary>
    private void Track()
    {
        Vector3 posA = target.position;                                             // 目標座標
        Vector3 posB = transform.position;                                          // 攝影機座標

        posB = Vector3.Lerp(posB, posA, 0.5f * Time.deltaTime * speed);             // 插值
        transform.position = posB;                                                  // 攝影機的座標 = 攝影機座標
    }

    private void LateUpdate()
    {
        Track();
    }
}
