using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private float _delayTime = 1f;

    float _startTime;

    void Start()
    {
        _startTime = Time.timeSinceLevelLoad + _delayTime;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > _startTime)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
