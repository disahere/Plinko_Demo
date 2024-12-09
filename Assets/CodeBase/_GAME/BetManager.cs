using TMPro;
using UnityEngine;

namespace CodeBase._GAME
{
    public class BetManager : MonoBehaviour
    {
        [Header("Bet Settings")]
        [SerializeField] private float minBet = 0.10f;
        [SerializeField] private float maxBet = 100f;
        [SerializeField] private float currentBet = 0.10f;
        [SerializeField] private float step = 0.10f;

        [Header("UI Elements")]
        [SerializeField] private TMP_InputField betInputField;
        [SerializeField] private BalanceManager balanceManager;

        public float CurrentBet => currentBet;

        private void Start() => UpdateBetUI();

        public void IncreaseBet()
        {
            var stepForCurrentBet = GetStepForCurrentBet(currentBet);

            if (!(currentBet + stepForCurrentBet <= maxBet)) return;
            currentBet += stepForCurrentBet;
            UpdateBetUI();
        }

        public void DecreaseBet()
        {
            var stepForCurrentBet = GetStepForCurrentBet(currentBet);

            if (!(currentBet - stepForCurrentBet >= minBet)) return;
            currentBet -= stepForCurrentBet;
            UpdateBetUI();
        }

        public void OnInputChanged(string input)
        {
            if (float.TryParse(input, out var betAmount))
            {
                if (betAmount >= minBet && betAmount <= maxBet)
                    currentBet = betAmount;
                else
                    currentBet = Mathf.Clamp(betAmount, minBet, maxBet);
            }
            else
                currentBet = minBet;

            UpdateBetUI();
        }
        
        public float GetCurrentBet() => currentBet;

        public bool PlaceBet() => balanceManager.PlaceBet(currentBet);

        private void UpdateBetUI()
        {
            if (betInputField != null)
                betInputField.text = currentBet.ToString("F2");
        }
        
        private float GetStepForCurrentBet(float bet)
        {
            return bet switch
            {
                < 0.8f => 0.1f,
                < 1.2f => 0.4f,
                < 2f => 0.8f,
                < 4f => 2f,
                < 10f => 6f,
                < 20f => 10f,
                < 50f => 30f,
                _ => 50f
            };
        }
    }
}