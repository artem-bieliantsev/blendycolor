using UnityEngine;

public class Plates
{
    private Vector3 _startPosition = new Vector3(-0.4f, 1, -1);
    private Vector3 _platesDistance = new Vector3(0.4f, 0, 0);

    public GameObject[] InstantiatePlates(GameObject platePrefab, int platesValue)
    {
        GameObject[] plates = new GameObject[platesValue];

        for (int i = 0; i < platesValue; i++)
        {
            plates[i] = Object.Instantiate(platePrefab, _startPosition, Quaternion.identity);
            _startPosition += _platesDistance;
        }

        return plates;
    }
}

