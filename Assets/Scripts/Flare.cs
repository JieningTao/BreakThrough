using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField]
    float FlightSpeed;

    [SerializeField]
    float ExplosionDelay;

    [SerializeField]
    float FlareDuration;

    [SerializeField]
    private GameObject Actualflare;

    [SerializeField]
    public Color FlareColor;



    private LensFlare FlareComponent;
    private bool Exploded;
    private float FlareIntensity;



    // Start is called before the first frame update
    void Start()
    {
        Exploded = false;
        Destroy(this.gameObject, ExplosionDelay + FlareDuration);
        StartCoroutine(DelayedExplosion());
        FlareComponent = Actualflare.GetComponent<LensFlare>();
        FlareComponent.color = FlareColor;
        Actualflare.SetActive(false);
        FlareIntensity = FlareComponent.brightness;
    }

    public void SetUpFlare(float _FlightSpeed, float _ExplosionDelay, float _FlareDuration, Color _FlareColor)
    {
        FlightSpeed = _FlightSpeed;
        ExplosionDelay = _ExplosionDelay;
        FlareDuration = _FlareDuration;
        FlareColor = _FlareColor;
        Start();
    }

    void Update()
    {
        if (!Exploded)
            transform.Translate(Vector3.up * FlightSpeed * Time.deltaTime);
        else
            Flicker();
    }

    private void Explode()
    {
        Actualflare.SetActive(true);
        Exploded = true;
    }

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(ExplosionDelay);
        Explode();
    }

    private void Flicker()
    {
        FlareComponent.brightness = FlareIntensity + (Random.Range(-0.2f, 0.2f) * FlareIntensity);
    }



}
