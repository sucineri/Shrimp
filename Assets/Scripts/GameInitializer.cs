using Firebase;
using Firebase.Unity.Editor;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://shrimp-d34a0.firebaseio.com/");
        LeaderboardRepo.CreateInstance();
        LeaderboardRepo.Instance.GetLeaderboard();
    }
}
