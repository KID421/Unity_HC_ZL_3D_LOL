using UnityEngine;

public class Castle : Tower
{
    [Header("勝利圖片")]
    public GameObject goVictory;
    [Header("失敗圖片")]
    public GameObject goDefeat;
    [Header("勝利音效")]
    public AudioClip soundVictory;
    [Header("失敗音效")]
    public AudioClip soundDefeat;
    [Header("是否為敵方")]
    public bool isEnemy = true;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    protected override void Dead()
    {
        base.Dead();

        if(isEnemy)
        {
            goVictory.SetActive(true);
            aud.PlayOneShot(soundVictory, 1.2f);
        }
        else
        {
            goDefeat.SetActive(true);
            aud.PlayOneShot(soundDefeat, 1.2f);
        }
    }
}
