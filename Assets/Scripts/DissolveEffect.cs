using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    private float dissolveAmount;
    public bool isDissolving = false;
    private void Update()
    {
        material = GetComponent<Renderer>().material;
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount += Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount -= Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }

        if (dissolveAmount >= 1)
        {
            Destroy(gameObject);
        }
    }

    public void BeginDissolving()
    {
        isDissolving = true;

    }
}
