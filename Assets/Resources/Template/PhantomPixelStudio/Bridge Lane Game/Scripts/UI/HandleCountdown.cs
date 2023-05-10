using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LaneGame
{
    public class HandleCountdown : MonoBehaviour
    {
        public float countdownStartValue;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private GameObject countdownUI;

        public event UnityAction StartGame = delegate { };

        private void Update()
        {
            countdownStartValue -= Time.deltaTime;
            countdownText.text = countdownStartValue.ToString("0");

            if (countdownStartValue < 0)
            {
                // カウントダウンUIを非表示にする
                countdownUI.SetActive(false);
                // ゲームを開始する
                StartGame();
            }
        }
    }
}