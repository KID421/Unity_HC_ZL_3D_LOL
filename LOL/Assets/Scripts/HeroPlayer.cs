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

    // override 複寫 - 可以複寫父類別包含 virtual 的成員
    protected override void Awake()
    {
        base.Awake();

        SetSkillUI();
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
