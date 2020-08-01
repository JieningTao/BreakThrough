using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLauncher : MonoBehaviour
{
    [SerializeField]
    GameObject Flare;
    [SerializeField]
    private float LaunchAngle = 5;

    [SerializeField]
    private Color Red;
    [SerializeField]
    private Color Blue;
    [SerializeField]
    private Color Yellow;
    [SerializeField]
    private Color Green;
    [SerializeField]
    private Color Purple;
    [SerializeField]
    private Color Orange;


    [SerializeField]
    float TBS;
    [SerializeField]
    float FlightSpeed;
    [SerializeField]
    float  ExplosionDelay;
    [SerializeField]
    float  FlareDuration;



    private void Start()
    {
        List<Color> Test = new List<Color>();
        Test.Add(Blue);
        Test.Add(Green);
        Test.Add(Blue);

        StartCoroutine(LaunchFlares(Test));
    }




    private IEnumerator LaunchFlares(List<Color> FlareColors)
    {
        foreach (Color a in FlareColors)
        {
            LaunchOneFlare(a);
            yield return new WaitForSeconds(TBS);
        }
        
    }

    private void LaunchOneFlare(Color CTL)
    {
        GameObject NewFlare = Instantiate(Flare, transform.position, transform.rotation, null);
        NewFlare.GetComponent<Flare>().SetUpFlare(FlightSpeed, ExplosionDelay, FlareDuration, CTL);
        NewFlare.transform.Rotate(new Vector3(Random.Range(0f,LaunchAngle),0, Random.Range(0f, LaunchAngle)),Space.Self);
    }
}
