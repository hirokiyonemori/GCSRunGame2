using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{

    //�@�G��MaxHP
    [SerializeField]
    private int maxHp = 100;
    //�@�G��HP
    [SerializeField]
    private int hp;
    //�@�G�̍U����
    [SerializeField]
    private int attackPower = 1;
    //private Enemy enemy;
    //�@HP�\���pUI
    [SerializeField]
    private GameObject HPUI;
    //�@HP�\���p�X���C�_�[
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

        //�@HP�\���pUI�̃A�b�v�f�[�g
        UpdateHPValue();

        if (hp <= 0)
        {
            //�@HP�\���pUI���\���ɂ���
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

    //�@���񂾂�HPUI���\���ɂ���
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