using UnityEngine;
using System.Collections;

/// <summary>
/// Handles particle effects in the game.
/// </summary>
public class NodeParticleEffects : MonoBehaviour
{
    public GameObject correctNodeParticlePrefab;
    public GameObject incorrectNodeParticlePrefab;

    public void PlayCorrectEffect(Vector3 position)
    {
        StartCoroutine(PlayEffect(correctNodeParticlePrefab, position));
    }

    public void PlayIncorrectEffect(Vector3 position)
    {
        StartCoroutine(PlayEffect(incorrectNodeParticlePrefab, position));
    }

    private IEnumerator PlayEffect(GameObject particlePrefab, Vector3 position)
    {
        GameObject particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f); // Effect duration
        Destroy(particleInstance);
    }
}
