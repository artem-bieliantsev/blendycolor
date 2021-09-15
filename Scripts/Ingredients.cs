using UnityEngine;

public class Ingredients
{
    public GameObject[] InstantiateIngredientButtons(LevelData level, int ingredientsValue, Transform[] parentPlates)
    {
        GameObject[] ingredients = new GameObject[ingredientsValue];

        for (int i = 0; i < ingredientsValue; i++)
        {
            GameObject currentIngredient = level.IngredientPrefab[i];

            ingredients[i] = Object.Instantiate(currentIngredient, parentPlates[i].position
                + currentIngredient.transform.position, currentIngredient.transform.rotation);
            ingredients[i].transform.SetParent(parentPlates[i]);
            ingredients[i].gameObject.AddComponent<IngredientButton>().IngredientPrefab = level.IngredientPrefab[i];
        }

        return ingredients;
    }

}


