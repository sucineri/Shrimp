using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private Button _confirmButton;

    private void Awake()
    {
        _confirmButton.interactable = false;
        _inputField.onValueChanged.AddListener(OnInputChanged);

        LeaderboardRepo.Instance.OnLeaderboardUpdated += OnUserCreated;
    }

    public void OnConfirm()
    {
        if (!string.IsNullOrEmpty(_inputField.text))
        {
            _confirmButton.gameObject.SetActive(false);
            LeaderboardRepo.Instance.CreateUser(_inputField.text);
        }
    }

    private void OnInputChanged(string input)
    {
        _confirmButton.interactable = !string.IsNullOrEmpty(input);
    }

    private void OnUserCreated(List<LeaderboardEntry> leaderboard)
    {
        gameObject.SetActive(false);
    }
}
