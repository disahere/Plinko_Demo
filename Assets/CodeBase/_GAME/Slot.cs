using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Utils.SmartDebug;
using UnityEngine;

namespace CodeBase._GAME
{
    public class Slot : MonoBehaviour
    {
        [Header("Slot Settings")] public float coefficient;
        private BalanceManager _balanceManager;
        private readonly DSender _sender = new("BallSpawner");
        public string color;

        private readonly List<Ball> _registeredBalls = new();

        [Obsolete("Obsolete")]
        private void Start() => _balanceManager = FindObjectOfType<BalanceManager>();

        public void Initialize(float coefficient, string color)
        {
            this.coefficient = coefficient;
            this.color = color;
        }

        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponent<Ball>();
            if (ball == null || _registeredBalls.Contains(ball))
                return;

            RegisterBall(ball);
        }

        private void RegisterBall(Ball ball)
        {
            _registeredBalls.Add(ball);

            if (ball.color == this.color)
            {
                DLogger.Message(_sender)
                .WithText($"Ball entered slot: BallColor={ball.color}, SlotColor={color}, Coefficient={coefficient}")
                .Log();
                float winAmount = ball.betAmount * coefficient;
                _balanceManager.AddToBalance(winAmount);
            }

            StartCoroutine(DeactivateBallAfterDelay(ball, 0.5f));
        }

        private IEnumerator DeactivateBallAfterDelay(Ball ball, float delay)
        {
            yield return new WaitForSeconds(delay);
            ball.ReturnToPool();
        }
    }
}