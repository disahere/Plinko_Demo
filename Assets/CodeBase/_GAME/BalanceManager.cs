using TMPro;
using UnityEngine;

namespace CodeBase._GAME
{
    public class BalanceManager : MonoBehaviour
    {
        [SerializeField] private float startingBalance = 3000f;
        [SerializeField] private TextMeshProUGUI balanceText;
        private float _currentBalance;

        public float CurrentBalance => _currentBalance;

        private void Start()
        {
            _currentBalance = startingBalance;
            UpdateBalanceUI();
        }

        public bool PlaceBet(float betAmount)
        {
            if (betAmount > _currentBalance || betAmount <= 0) return false;

            _currentBalance -= betAmount;
            UpdateBalanceUI();
            return true;
        }

        public void AddToBalance(float amount)
        {
            _currentBalance += amount;
            UpdateBalanceUI();
        }

        private void UpdateBalanceUI() => 
            balanceText.text = _currentBalance.ToString("F2");
    }
}