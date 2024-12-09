using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase._GAME
{
    public class BallPool : MonoBehaviour
    {
        [Header("Pool Settings")] [SerializeField]
        private GameObject ballPrefab;

        [SerializeField] private int initialPoolSize = 10;

        private readonly List<Ball> _pool = new();

        private void Start() => InitializePool();

        private void InitializePool()
        {
            for (var i = 0; i < initialPoolSize; i++)
                CreateNewBall();
        }

        private Ball CreateNewBall()
        {
            var ballObject = Instantiate(ballPrefab, transform);
            var ball = ballObject.GetComponent<Ball>();

            if (ball == null)
            {
                Destroy(ballObject);
                return null;
            }

            ballObject.SetActive(false);
            _pool.Add(ball);
            return ball;
        }

        public Ball GetPooledBall()
        {
            foreach (var ball in _pool.Where(ball => ball != null && !ball.gameObject.activeInHierarchy))
            {
                ball.gameObject.SetActive(true);
                return ball;
            }

            var newBall = CreateNewBall();
            newBall.gameObject.SetActive(true);
            return newBall;
        }

        public void ReturnBall(Ball ball)
        {
            if (_pool.Contains(ball))
                ball.gameObject.SetActive(false);
        }
    }
}