using UnityEngine;

namespace CodeBase._GAME
{
    public class Ball : MonoBehaviour
    {
        [Header("Ball Settings")]
        public string color;
        public float betAmount;

        private BallPool _ballPool;
        private BallSpawner _spawner;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        public void Initialize(string color, float betAmount, BallPool pool, BallSpawner spawner)
        {
            this.color = color;
            this.betAmount = betAmount;
            this._ballPool = pool;
            _spawner = spawner;
        }

        public void ReturnToPool()
        {
            this._rb.linearVelocity = Vector3.zero;
            this._rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            _ballPool.ReturnBall(this);
            _spawner.OnBallReturnedToPool();
        }
    }
}