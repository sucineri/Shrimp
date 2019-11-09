using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void ResetClicked()
    {
        LeaderboardRepo.Instance.ResetLeaderboard();
    }
}
