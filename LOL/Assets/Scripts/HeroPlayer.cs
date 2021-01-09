using UnityEngine;
using UnityEngine.UI;

// : 父類別 - 繼承
// 繼承：擁有父類別所有成員

public class HeroPlayer : HeroBase
{
    // 四顆招式按鈕
    private Button btnSkill1;
    private Button btnSkill2;
    private Button btnSkill3;
    private Button btnSkill4;

    private Image[] imgSkills = new Image[4];
    private Text[] textSkills = new Text[4];

    /// <summary>
    /// 目標物件
    /// </summary>
    private Transform target;
    /// <summary>
    /// 虛擬搖桿
    /// </summary>
    private Joystick joy;
    /// <summary>
    /// 攝影機根物件
    /// </summary>
    private Transform camRoot;

    [Header("移動的距離"), Range(0f, 5f)]
    public float moveDistance = 2;

    // override 複寫 - 可以複寫父類別包含 virtual 的成員
    protected override void Awake()
    {
        base.Awake();

        target = GameObject.Find("目標物件").transform;
        joy = GameObject.Find("虛擬搖桿").GetComponent<Joystick>();
        camRoot = GameObject.Find("攝影機根物件").transform;

        SetSkillUI();
    }

    protected override void Update()
    {
        base.Update();
        MoveControl();
    }

    /// <summary>
    /// 移動控制
    /// </summary>
    private void MoveControl()
    {
        float v = joy.Vertical;
        float h = joy.Horizontal;

        // 目標物件.座標 = 角色.座標 + 攝影機根物件.前方 * 垂直 * 距離 + 攝影機根物件.右邊 * 水平 * 距離
        target.position = transform.position + camRoot.forward * v * moveDistance + camRoot.right * h * moveDistance;
        // 移動(目標物件)
        Move(target);
    }

    /// <summary>
    /// 設定四個技能按鈕
    /// </summary>
    private void SetSkillUI()
    {
        // 取得四顆招式按鈕
        btnSkill1 = GameObject.Find("技能 1").GetComponent<Button>();
        btnSkill2 = GameObject.Find("技能 2").GetComponent<Button>();
        btnSkill3 = GameObject.Find("技能 3").GetComponent<Button>();
        btnSkill4 = GameObject.Find("技能 4").GetComponent<Button>();
        // 按鈕 點擊後執行 (方法)
        btnSkill1.onClick.AddListener(Skill1);
        btnSkill2.onClick.AddListener(Skill2);
        btnSkill3.onClick.AddListener(Skill3);
        btnSkill4.onClick.AddListener(Skill4);

        for (int i = 0; i < 4; i++)
        {
            imgSkills[i] = GameObject.Find("技能圖片 " + (i + 1)).GetComponent<Image>();
            textSkills[i] = GameObject.Find("技能冷卻 " + (i + 1)).GetComponent<Text>();
            // 更新技能圖片與冷卻時間
            imgSkills[i].sprite = data.skills[i].image;
            textSkills[i].text = "";
        }
    }
}
