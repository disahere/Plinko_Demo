using CodeBase.Utils.SmartDebug;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase._GAME
{
    public class BallSpawner : MonoBehaviour
    {
        [Header("Spawn Points")] [SerializeField]
        private Transform[] spawnPoints;

        [Header("Ball Pool")] [SerializeField] private BallPool ballPool;

        [Header("Bet Manager")] [SerializeField]
        private BetManager betManager;

        [Header("Materials")] [SerializeField] private Material greenMaterial;
        [SerializeField] private Material yellowMaterial;
        [SerializeField] private Material redMaterial;
        
        [Header("UI Elements")]
        [SerializeField] private Button betIncreaseButton;
        [SerializeField] private Button betDecreaseButton;
        [SerializeField] private TMP_Dropdown pinsDropdown;
        
        private int _activeBalls;
        
        private readonly DSender _sender = new("BallSpawner");

        public void SpawnGreenBall()
        {
            var betAmount = betManager.GetCurrentBet();
            if (betManager.PlaceBet())
                SpawnBall("Green", betAmount, greenMaterial);
        }

        public void SpawnYellowBall()
        {
            var betAmount = betManager.GetCurrentBet();
            if (betManager.PlaceBet())
                SpawnBall("Yellow", betAmount, yellowMaterial);
        }

        public void SpawnRedBall()
        {
            var betAmount = betManager.GetCurrentBet();
            if (betManager.PlaceBet())
                SpawnBall("Red", betAmount, redMaterial);
        }

        private void SpawnBall(string color, float betAmount, Material material)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var ball = ballPool.GetPooledBall();

            ball.transform.position = spawnPoint.position;
            ball.Initialize(color, betAmount, ballPool, this);

            _activeBalls++;
            UpdateUIInteractableState(false);
            var component = ball.GetComponent<Renderer>();
            if (component == null) return;
            component.material = material;
            
            DLogger.Message(_sender)
                .WithText($"Ball spawned: Color={color}, BetAmount={betAmount}")
                .Log();
        }
        
        public void OnBallReturnedToPool()
        {
            _activeBalls--;

            if (_activeBalls <= 0)
                UpdateUIInteractableState(true);
        }
        
        private void UpdateUIInteractableState(bool state)
        {
            betIncreaseButton.interactable = state;
            betDecreaseButton.interactable = state;
            pinsDropdown.interactable = state;
        }
    }
}