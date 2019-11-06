using System;
using TMPro;
using UnityEngine;

public class ShrimpCounter : MonoBehaviour
{
    public static event Action<int> CountChanged;

    [SerializeField] private TMP_Text _countText = null;

    private int _killCount;

    private void Start()
    {
        _killCount = 0;
        UpdateCount();
    }

    private void UpdateCount()
    {
        _countText.text = _killCount.ToString();
    }

    public void IncrementClicked()
    {
        _killCount++;
        UpdateCount();

        CountChanged?.Invoke(_killCount);
    }
}
