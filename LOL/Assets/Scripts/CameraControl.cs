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
        Vector3 posA = target.position;
        Vector3 posB = transform.position;

        posB = Vector3.Lerp(posB, posA, 0.5f * speed * Time.deltaTime);
        transform.position = posB;
    }

    private void Update()
    {
        Track();
    }
}
