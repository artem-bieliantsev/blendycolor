using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Jug : Blender
{
    [SerializeField] private List<Color> _inJugIngredients;

    [SerializeField] private Renderer _liquid;

    [SerializeField] private Transform _blenderJug;

    [SerializeField] private Lid _lid;

    private float _mixerRadius = 0.15f;
    private float _mixSpeed = 0.05f;

    private float _fillDuration = 1.5f;
    private float _maxFillCount = 0.65f;

    private readonly string _shaderColorID = "Color_F689E8B9";

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.GetComponent<Rigidbody>())
        {
            if (coll.gameObject.GetComponentInParent<Jug>() == null)
            {
                Color ingredientColor = coll.gameObject.GetComponent<MeshRenderer>().sharedMaterials[0].GetColor(_shaderColorID);
                _inJugIngredients.Add(ingredientColor);

                if (_inJugIngredients.Count != 0)
                {
                    Hud.Instance.MixButton.SetActive(true);
                }

                coll.gameObject.transform.SetParent(transform);

                Controller.Instance.AudioManager.PlaySound("DroppingObject");
                
                JugShake(0.5f, 5, 10, 90);
            }
        }
    }

    public void MixIngredients()
    {
        Controller.Instance.AudioManager.PlaySound("BlenderMix");

        _lid.CloseLid();

        StartCoroutine(DestroyIngredientsInJug(_mixSpeed));
        
        FillTheJug();
        JugShake(1.5f, 5, 10, 90);

        MixColors(_inJugIngredients);

        _liquid.material.SetColor("_color", MixedColor);
    }

    public void ClearJug()
    {   
        _liquid.material.SetFloat("_fill", 0f);
        _inJugIngredients.Clear();
        _lid.CloseLid();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void JugShake(float duration, float strength, int vibratio, float randomness)
    {
        _blenderJug.DOKill(true);
        _blenderJug.DOShakeRotation(duration, strength, vibratio, randomness, false);
    }


    private void FillTheJug()
    {
        _liquid.material.DOFloat(_maxFillCount, "_fill", _fillDuration);
    }

    IEnumerator DestroyIngredientsInJug(float mixDelay)
    {
        int index = 0;

        while (index < 15)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _mixerRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Rigidbody>())
                {
                    Destroy(colliders[i].gameObject);
                }
            }
            yield return new WaitForSeconds(mixDelay);

            index++;
        }
    }
}
