using System;
using TMPro;
using UnityEngine;

public class ShrimpCounter : MonoBehaviour
{
    public static event Action<int> CountChanged;

    [SerializeField] private TMP_Text _countText = null;
    [SerializeField] private GameObject _clickParticles = null;

    private int _killCount;

    private void Start()
    {
        _killCount = 0;
        _clickParticles.SetActive(false);

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

        _clickParticles.SetActive(false);
        _clickParticles.SetActive(true);

        CountChanged?.Invoke(_killCount);
    }
}
