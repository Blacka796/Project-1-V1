using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    private float lifeTime = 5;

    private Material mat;
    private Color originalCol;
    private float fadePercent;
    private float deathTime;
    private bool fading;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalCol = mat.color;
        deathTime = Time.time + lifeTime;

        StartCoroutine("Fade");
    }
        IEnumerator Fade()
        {
            while (true)
            {
                yield return new WaitForSeconds(.2f);
                if (fading)
                {
                    fadePercent += Time.deltaTime;
                    mat.color = Color.Lerp(originalCol, Color.clear, fadePercent);

                    if (fadePercent >=1 )
                    {
                        Destroy(gameObject);
                    }
                }
                else if (Time.time > deathTime)
                {
                    fading = true;
                }
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Ground")
        {
            GetComponent<Rigidbody>().Sleep();
        }
    }
}
