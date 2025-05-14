
using UnityEngine;
using System.Linq;
using TMPro;

public class KeyCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyCount;

    void Start()
    {
        int trueCount = GameSystem.key.Count(k => k);

        keyCount.text = $"X {trueCount}";
    }
}
