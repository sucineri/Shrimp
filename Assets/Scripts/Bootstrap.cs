using Firebase;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private float _delayTime = 1f;

    IEnumerator Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://shrimp-d34a0.firebaseio.com/");
        LeaderboardRepo.CreateInstance();
        StartCoroutine(LeaderboardRepo.Instance.GetLeaderboard());

        yield return new WaitForSeconds(_delayTime);
        yield return new WaitUntil(() => LeaderboardRepo.Instance.Initialized);

        SceneManager.LoadScene("Main");
    }
}
