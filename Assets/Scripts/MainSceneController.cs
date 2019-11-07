using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _registerPanel;

    private void Awake()
    {
        if (!LeaderboardRepo.Instance.UserCreated)
        {
            _registerPanel.SetActive(true);
        }
    }
}
