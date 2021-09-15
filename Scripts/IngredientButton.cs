using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SphereCollider))]
public class IngredientButton : MonoBehaviour, IPointerClickHandler
{
    private GameObject _ingredientPrefab;

    private Vector3 _punch = new Vector3(0.4f, 0.4f, 0.4f);
    private float _punchDuration = 0.25f;
    private int _punchVibratio = 6;   
    private float _elasticity = 1f;

    public GameObject IngredientPrefab { get => _ingredientPrefab; set => _ingredientPrefab = value; }

    private void Start()
    {
        SphereCollider collider = gameObject.GetComponent<SphereCollider>();

        collider.radius = 0.15f;
        collider.isTrigger = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        gameObject.transform.DOKill(true);
        gameObject.transform.DOPunchScale(_punch, _punchDuration, _punchVibratio, _elasticity);

        Controller.Instance.AudioManager.PlaySound("FruitPop");
        Controller.Instance.SpawnFruit(_ingredientPrefab);        
    }
}
