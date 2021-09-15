using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField] private GameObject[] _ingredientPrefab;
    [SerializeField] private GameObject _visitor;   
   
    public GameObject[] IngredientPrefab => _ingredientPrefab;
    public GameObject Visitor  => _visitor; 
}

