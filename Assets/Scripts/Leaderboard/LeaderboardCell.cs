using TMPro;
using UnityEngine;

public class LeaderboardCell : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _placeLabel;

    [SerializeField]
    private TextMeshProUGUI _nameLabel;

    [SerializeField]
    private TextMeshProUGUI _scoreLabel;

    private LeaderboardEntry _entry;

    public void SetEntry(LeaderboardEntry entry, int place)
    {
        _entry = entry;
        _placeLabel.text = place.ToString();
        _nameLabel.text = entry.Name;
        _scoreLabel.text = entry.Score.ToString("N0");
    }
}
