using UnityEngine;

// ScriptableObject
// 腳本化物件：將腳本變為物件存放在專案內方便管理

[CreateAssetMenu(fileName = "英雄資料", menuName = "KID/英雄資料")]
public class HeroData : ScriptableObject
{
    [Header("血量"), Range(100, 800)]
    public float hp;
    [Header("魔力"), Range(50, 400)]
    public float mp;
    [Header("移動速度"), Range(0.5f, 50f)]
    public float speed;
    [Header("普攻"), Range(0f, 500f)]
    public float attack;
    [Header("普攻冷卻"), Range(0f, 10f)]
    public float attackCD;
    // 類型後加上 []：陣列 - 儲存多筆相同類型的資料
    [Header("技能組")]
    public Skill[] skills;
}

// 序列化：將類別資料顯示在屬性面板上
[System.Serializable]
public class Skill
{
    public string name;

    [Header("攻擊"), Range(0f, 100f)]
    public float attack;
    [Header("消耗"), Range(0f, 100f)]
    public float cost;
    [Header("圖片")]
    public Sprite image;
    [Header("冷卻"), Range(0f, 100f)]
    public float cd;
}