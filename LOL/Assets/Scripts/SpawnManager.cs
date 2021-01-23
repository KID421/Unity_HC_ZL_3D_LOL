using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("生成物件")]
    public GameObject goSpawn;
    [Header("生成點")]
    public Transform pointSpawn;
    [Header("生成間隔"), Range(0, 30)]
    public float interval = 10;
    [Header("每一波小兵生成間隔"), Range(0, 10)]
    public float intervalOnce = 0.2f;
    [Header("每一波小兵數量"), Range(0, 10)]
    public int count = 4;

    /// <summary>
    /// 當前小兵數
    /// </summary>
    private int current;

    /// <summary>
    /// 生成小兵
    /// </summary>
    private void Spawn()
    {
        // 如果 當前小兵數 < 每波數量
        if (current < count)
        {
            Instantiate(goSpawn, pointSpawn.position, pointSpawn.rotation);
            current++;
            Invoke("Spawn", intervalOnce);      // 延遲呼叫("方法名稱"，延遲時間)
        }
        else current = 0;
    }

    private void Start()
    {
        // 重複呼叫("方法名稱"，延遲時間，間隔時間)
        InvokeRepeating("Spawn", 0, interval);
    }
}
