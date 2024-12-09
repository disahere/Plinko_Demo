using System.Collections.Generic;
using UnityEngine;

namespace CodeBase._GAME
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [Header("General Settings")] [SerializeField]
        private GameObject obstaclePrefab;

        [SerializeField] private Transform parent;
        [SerializeField] private BoxCollider fieldCollider;

        [Header("Obstacle Settings")] [SerializeField]
        private float spacingX = 3f;

        [SerializeField] private float spacingY = 2f;

        [Header("Other Settings")] [SerializeField]
        private SlotGenerator slotGenerator;

        private int _obstacleCount = 12;

        public void GenerateObstacles(int count)
        {
            ClearExistingObstacles();
            _obstacleCount = count;

            var fieldSize = fieldCollider.size;
            var fieldCenter = fieldCollider.transform.position + fieldCollider.center;

            var maxWidth = fieldSize.x;
            var maxHeight = fieldSize.y;

            (spacingX, spacingY) = GetSpacingForObstacleCount(count);

            for (var rowIndex = 0; rowIndex < _obstacleCount; rowIndex++)
            {
                var rowCount = CalculateRowCount(rowIndex);
                GenerateRow(rowIndex, rowCount, maxWidth, maxHeight, fieldCenter);
            }

            var bottomRowPositions = GetBottomRowPositions();
            slotGenerator.GenerateSlots(bottomRowPositions, fieldCollider, count);
        }

        private void ClearExistingObstacles()
        {
            foreach (Transform child in parent)
                Destroy(child.gameObject);
        }

        private int CalculateRowCount(int rowIndex) => 3 + rowIndex;

        private void GenerateRow(int rowIndex, int rowCount, float maxWidth, float maxHeight, Vector3 fieldCenter)
        {
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            for (var elementIndex = 0; elementIndex < rowCount; elementIndex++)
            {
                var position = CalculatePosition(rowIndex,
                    elementIndex,
                    rowCount,
                    spacingX,
                    spacingY,
                    maxHeight,
                    maxWidth,
                    fieldCenter);
                var obstacle = Instantiate(obstaclePrefab, position, rotation, parent);
                ApplyScale(obstacle, spacingX, spacingY);
            }
        }

        private (float spacingX, float spacingY) GetSpacingForObstacleCount(int count)
        {
            return count switch
            {
                12 => (4f, 3f),
                14 => (3.5f, 2.5f),
                16 => (3.2f, 2.2f),
                _ => (4f, 3f)
            };
        }

        private Vector3 CalculatePosition(int rowIndex, int elementIndex, int rowCount, float spacingX, float spacingY,
            float maxHeight, float maxWidth, Vector3 fieldCenter)
        {
            var xOffset = -(rowCount - 1) * spacingX / 2;
            var xPosition = elementIndex * spacingX + xOffset;
            var yPosition = maxHeight / 2 - rowIndex * spacingY;

            return new Vector3(
                Mathf.Clamp(fieldCenter.x + xPosition, fieldCenter.x - maxWidth / 2, fieldCenter.x + maxWidth / 2),
                Mathf.Clamp(fieldCenter.y + yPosition, fieldCenter.y - maxHeight / 2, fieldCenter.y + maxHeight / 2),
                fieldCenter.z
            );
        }


        private void ApplyScale(GameObject obstacle, float spacingX, float spacingY)
        {
            var scaleFactor = Mathf.Min(spacingX, spacingY) * 0.3f;
            obstacle.transform.localScale = Vector3.one * scaleFactor;
        }

        private List<Vector3> GetBottomRowPositions()
        {
            var positions = new List<Vector3>();
            var bottomRowCount = CalculateRowCount(_obstacleCount - 2);

            for (var i = 0; i < bottomRowCount; i++)
            {
                var xOffset = -(bottomRowCount - 1) * spacingX / 2;
                var xPosition = i * spacingX + xOffset;
                var yPosition = fieldCollider.transform.position.y - fieldCollider.size.y / 2;

                positions.Add(new Vector3(xPosition, yPosition, 0));
            }

            return positions;
        }
    }
}