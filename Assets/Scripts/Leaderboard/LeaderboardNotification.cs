using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardNotification : MonoBehaviour
{
    [SerializeField] TMP_Text _notificationText = null;
    [SerializeField] Animator _animator = null;

    private int _lastPosition = -1;

    private void Start()
    {
        LeaderboardRepo.Instance.OnLeaderboardUpdated += OnLeaderboardUpdated;
    }

    private void OnDestroy()
    {
        LeaderboardRepo.Instance.OnLeaderboardUpdated -= OnLeaderboardUpdated;
    }

    private void OnLeaderboardUpdated(List<LeaderboardEntry> entries)
    {
        int currentPosition = entries.FindIndex(e => e.Name == LeaderboardRepo.Instance.UserName) + 1;

        // First load
        if (_lastPosition == -1)
        {
            _lastPosition = currentPosition;
            return;
        }

        // Same position, do nothing
        if (currentPosition == _lastPosition)
        {
            return;
        }

        // You're new to the top!
        if (currentPosition == 1)
        {
            _notificationText.text = "You are the Shrimp Master!";
        }
        // Moving up leaderboard
        else if (currentPosition < _lastPosition)
        {
            var entryJustPassed = entries[currentPosition];
            _notificationText.text = $"You've passed {entryJustPassed.Name}";
        }
        // Moving down leaderboard
        else if (currentPosition > _lastPosition)
        {
            var entryPassingYou = entries[currentPosition - 2];
            _notificationText.text = $"{entryPassingYou.Name} has passed you!";
        }

        _animator.SetTrigger("show");
        _lastPosition = currentPosition;
    }
}
