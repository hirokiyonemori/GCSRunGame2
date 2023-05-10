using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class BattleStartUI : MonoBehaviour
{
    [SerializeField] private float countdownStartValue = 2f;
    [SerializeField] private TextMeshProUGUI countdownText;

    private async void Start()
    {
        // 5�b�ԃJ�E���g�_�E����\��
        countdownText.text = "Battle Start!";
        await UniTask.Delay((int)(countdownStartValue * 1000f));

        // �J�E���g�_�E�����\���ɂ���
        gameObject.SetActive(false);
    }
}