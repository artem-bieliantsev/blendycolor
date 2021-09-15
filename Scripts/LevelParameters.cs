using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelParameters
{
    [SerializeField] private List<LevelData> _levelData;

    [SerializeField] private Color _referenceColor;   

    [SerializeField] private GameObject _platePrefab;
    [SerializeField] private GameObject[] _currentPlates;

    [SerializeField] private GameObject[] _ingredientButtons;

    [SerializeField] private GameObject _currentVisitor;

    private int _maxIngredientValue = 3;

    private readonly string _shaderColorID = "Color_F689E8B9";

    public Color ReferenceColor { get => _referenceColor; set => _referenceColor = value; }
    public List<LevelData> LevelData { get => _levelData; set => _levelData = value; }
    public GameObject CurrentVisitor { get => _currentVisitor; set => _currentVisitor = value; }

    public void Initialize(LevelData levelData)
    {
        int ingredientValue = levelData.IngredientPrefab.Length;

        if (ingredientValue > _maxIngredientValue)
            ingredientValue = _maxIngredientValue;        

        Plates plates = new Plates();
        _currentPlates = plates.InstantiatePlates(_platePrefab, ingredientValue);

        Transform[] platesTransform = new Transform[ingredientValue];

        for (int i = 0; i < ingredientValue; i++)
        {
            platesTransform[i] = _currentPlates[i].transform;
        }

        Ingredients ingredients = new Ingredients();        
        _ingredientButtons = ingredients.InstantiateIngredientButtons(levelData, ingredientValue, platesTransform);

        CharacterVisitor visitor = new CharacterVisitor();
        _currentVisitor = visitor.InstantiateVisitor(levelData.Visitor);

        _referenceColor = GetReferenceColor(levelData);
    }

    public void ClearLevel()
    {
        for (int i = 0; i < _currentPlates.Length; i++)
        {
            Object.Destroy(_currentPlates[i]);
            Object.Destroy(_ingredientButtons[i]);
        }
    }

    private Color GetReferenceColor(LevelData levelData)
    {
        Color[] ingredientPull = new Color[10];

        MeshRenderer[] ingredient = new MeshRenderer[levelData.IngredientPrefab.Length];

        int pullValue = Random.Range(4, 10);

        for (int i = 0; i < levelData.IngredientPrefab.Length; i++)
        {
            ingredient[i] = levelData.IngredientPrefab[i].GetComponent<MeshRenderer>();
        }

        for (int i = 0; i < pullValue; i++)
        {
            int index = Random.Range(0, ingredient.Length);
            ingredientPull[i] = ingredient[index].sharedMaterials[0].GetColor(_shaderColorID);
        }

        Color color = new Color(0, 0, 0, 0);

        foreach (Color col in ingredientPull)
        {
            color += col;
        }

        color /= pullValue;

       return color;
    }
}
