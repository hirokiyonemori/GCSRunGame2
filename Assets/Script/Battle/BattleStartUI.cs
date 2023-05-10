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
        // 5秒間カウントダウンを表示
        countdownText.text = "Battle Start!";
        await UniTask.Delay((int)(countdownStartValue * 1000f));

        // カウントダウンを非表示にする
        gameObject.SetActive(false);
    }
}