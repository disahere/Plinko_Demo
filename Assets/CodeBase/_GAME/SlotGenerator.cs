using System;
using UnityEngine;
using System.Collections.Generic;

namespace CodeBase._GAME
{
    public class SlotGenerator : MonoBehaviour
    {
        private const float MagicValue = 12f;

        [Header("Slot Settings")] 
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform parent;

        [Header("General Settings")] 
        [SerializeField] private Material greenMaterial;
        [SerializeField] private Material yellowMaterial;
        [SerializeField] private Material redMaterial;

        [Header("Generation Settings")] 
        [SerializeField] private float spacingY = 1f;

        private readonly List<Slot> _slots = new();

        public void GenerateSlots(List<Vector3> bottomRowPositions, BoxCollider fieldCollider, int pinCount)
        {
            ClearExistingSlots();

            var baseY = fieldCollider.transform.position.y - fieldCollider.size.y / 2;

            for (var rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                var yOffset = rowIndex * spacingY;
                var coefficients = GetCoefficients(pinCount, GetColor(rowIndex));

                for (var i = 0; i < bottomRowPositions.Count; i++)
                {
                    var slotPosition = new Vector3(bottomRowPositions[i].x, baseY - yOffset + MagicValue, bottomRowPositions[i].z);
                    var slotObject = Instantiate(slotPrefab, slotPosition, Quaternion.identity, parent);

                    var slot = slotObject.GetComponent<Slot>();
                    if (slot != null && i < coefficients.Length)
                    {
                        slot.Initialize(coefficients[i], GetColor(rowIndex));
                        _slots.Add(slot);
                    }

                    ApplyMaterial(slotObject, rowIndex);
                }
            }
        }

        private void ClearExistingSlots()
        {
            foreach (Transform child in parent)
                Destroy(child.gameObject);

            _slots.Clear();
        }

        private void ApplyMaterial(GameObject slot, int rowIndex)
        {
            var slotRenderer = slot.GetComponent<Renderer>();
            if (slotRenderer == null) return;
            slotRenderer.material = rowIndex switch
            {
                0 => greenMaterial,
                1 => yellowMaterial,
                2 => redMaterial,
                _ => slotRenderer.material
            };
        }

        private float[] GetCoefficients(int pinCount, string color)
        {
            return pinCount switch
            {
                12 => color switch
                {
                    "Green" => new float[] { 11, 3.2f, 1.6f, 1.2f, 1.1f, 1, 0.5f, 0.5f },
                    "Yellow" => new float[] { 25, 8, 3.1f, 1.7f, 1.2f, 0.7f, 0.3f, 0.3f },
                    "Red" => new float[] { 141, 25, 8.1f, 2.3f, 0.7f, 0.2f, 0, 0 },
                    _ => Array.Empty<float>()
                },
                14 => color switch
                {
                    "Green" => new float[] { 18, 3.2f, 1.6f, 1.3f, 1.2f, 1.1f, 1, 0.5f, 0.5f },
                    "Yellow" => new float[] { 55, 12, 5.6f, 3.2f, 1.6f, 1, 0.7f, 0.2f, 0.2f },
                    "Red" => new float[] { 353, 49, 14, 5.3f, 2.1f, 0.5f, 0.2f, 0 },
                    _ => Array.Empty<float>()
                },
                16 => color switch
                {
                    "Green" => new float[] { 35, 7.7f, 2.5f, 1.6f, 1.3f, 1.2f, 1.1f, 1, 0.4f, 0.4f },
                    "Yellow" => new float[] { 118, 61, 12, 4.5f, 2.3f, 1.2f, 1, 0.7f, 0.2f, 0.2f },
                    "Red" => new float[] { 555, 122, 26, 8.5f, 3.5f, 2, 0.5f, 0.2f, 0 },
                    _ => Array.Empty<float>()
                },
                _ => Array.Empty<float>()
            };
        }

        private string GetColor(int rowIndex)
        {
            return rowIndex switch
            {
                0 => "Green",
                1 => "Yellow",
                2 => "Red",
                _ => "Unknown"
            };
        }
    }
}