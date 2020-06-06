using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCamera : MonoBehaviour
{
    [SerializeField]
    Material TransitionMaterial;
    float cutoffVal = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        TransitionMaterial.SetFloat("_Cutoff", cutoffVal);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if (cutoffVal > 0)
        {
            cutoffVal -= Time.deltaTime;
            TransitionMaterial.SetFloat("_Cutoff", cutoffVal);
        }
    }
}
