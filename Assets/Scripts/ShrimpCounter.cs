using System;
using TMPro;
using UnityEngine;

public class ShrimpCounter : MonoBehaviour
{
    public static event Action<int> CountChanged;

    [SerializeField] private TMP_Text _countText = null;
    [SerializeField] private GameObject _clickParticlesPrefab = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private float _baseAudioPitch = 1.0f;
    [SerializeField] private float _audioPitchVariance = 0.15f;
    [SerializeField] private AudioClip _shotgunClip = null;

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

        Instantiate(_clickParticlesPrefab);

        _audioSource.pitch = _baseAudioPitch + UnityEngine.Random.Range(-_audioPitchVariance, _audioPitchVariance);
        _audioSource.PlayOneShot(_shotgunClip);

        CountChanged?.Invoke(_killCount);
    }
}
