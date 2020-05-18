using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletScript : MonoBehaviour
{
    public float explosionTimer;
    [SerializeField]
    private GameObject explosion;

    private void Start()
    {
        StartCoroutine(pelletExplode());
    }

    private IEnumerator pelletExplode()
    {
        yield return new WaitForSeconds(Random.Range(0f,explosionTimer));
        GameObject spawnedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
