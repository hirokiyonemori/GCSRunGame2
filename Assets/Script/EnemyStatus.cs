using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{

    //　敵のMaxHP
    [SerializeField]
    private int maxHp = 100;
    //　敵のHP
    [SerializeField]
    private int hp;
    //　敵の攻撃力
    [SerializeField]
    private int attackPower = 1;
    //private Enemy enemy;
    //　HP表示用UI
    [SerializeField]
    private GameObject HPUI;
    //　HP表示用スライダー
    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private BarAnimator barAnimator;


    void Start()
    {
      //  enemy = GetComponent<Enemy>();
        hp = maxHp;
        //hpSlider = HPUI.transform.Find("HPBar").GetComponent<Slider>();
        //hpSlider.value = 1f;
        barAnimator.value = 1;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;

        //　HP表示用UIのアップデート
        UpdateHPValue();

        if (hp <= 0)
        {
            //　HP表示用UIを非表示にする
            //HideStatusUI();
        }
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    //　死んだらHPUIを非表示にする
    public void HideStatusUI()
    {
        HPUI.SetActive(false);
    }

    public void UpdateHPValue()
    {
        //hpSlider.value = (float)GetHp() / (float)GetMaxHp();
        //LogSystem.Log("  (float)GetHp() " + (float)GetHp());
        //LogSystem.Log("  (float)GetMaxHp() " + (float)GetMaxHp());

        barAnimator.value = (float)GetHp() / (float)GetMaxHp();
    }

}