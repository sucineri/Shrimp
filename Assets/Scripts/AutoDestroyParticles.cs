using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyParticles : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_particleSystem == null)
            return;

        if (!_particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
