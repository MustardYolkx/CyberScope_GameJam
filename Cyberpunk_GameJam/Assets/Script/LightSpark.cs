using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSpark : MonoBehaviour
{
    public bool isSpark;

    public float elapsedTime;
    public float duration;
    public AnimationCurve animCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Control());
    }

    // Update is called once per frame
    void Update()
    {
        Spark();
    }

    public void Spark()
    {
        if (isSpark)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
            {
                elapsedTime = duration;
                isSpark = false;
            }

            float intensityFactor = animCurve.Evaluate(elapsedTime / duration);

            GetComponent<Light2D>().intensity = intensityFactor;
        }
    }

    IEnumerator Control()
    {
        isSpark = true;
        elapsedTime = 0;
        yield return new WaitForSeconds(3f);
        StartCoroutine(Control());
    }
}
