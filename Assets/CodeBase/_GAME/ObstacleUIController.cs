using TMPro;
using UnityEngine;

namespace CodeBase._GAME
{
    public class ObstacleUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown  dropdown;
        [SerializeField] private ObstacleGenerator generator;

        private void Start()
        {
            dropdown.onValueChanged.AddListener(UpdateObstacleCount);
            dropdown.value = 0;
            UpdateObstacleCount(0);
        }

        private void UpdateObstacleCount(int index)
        {
            var count = index switch
            {
                1 => 14,
                2 => 16,
                _ => 12
            };

            generator.GenerateObstacles(count);
        }
    }
}