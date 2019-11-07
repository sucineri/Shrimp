using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private LeaderboardCell _cellPrefab;

    private void Start()
    {
        LeaderboardRepo.Instance.OnLeaderboardUpdated += ShowLeaderboard;
        ShowLeaderboard(LeaderboardRepo.Instance.Leaderboard);
    }

    private void ShowLeaderboard(List<LeaderboardEntry> list)
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < list.Count; i++)
        {
            var cell = Instantiate(_cellPrefab, _content);
            cell.SetEntry(list[i], i + 1);
        }
    }
}
