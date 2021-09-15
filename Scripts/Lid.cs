using System.Collections.Generic;
using UnityEngine;

public class Lid : MonoBehaviour
{
    [SerializeField] private Transform _blenderLid;

    private List<GameObject> _fallingIngredients = new List<GameObject>();

    private bool _isOpen = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
             _fallingIngredients.Add(other.gameObject);       
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            OpenLid();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            if (_fallingIngredients != null)
            {
                _fallingIngredients.Clear();

                if (_fallingIngredients.Count == 0)
                {
                    CloseLid();
                }
            }
        }
    }

    public void OpenLid()
    {
        if (!_isOpen)
        {
            _blenderLid.transform.Rotate(new Vector3(0f, 0f, -90f));
            _isOpen = true;
        }
    }

    public void CloseLid()
    {
        if (_isOpen)
        {
            _blenderLid.transform.Rotate(new Vector3(0f, 0f, 90f));
            _isOpen = false;
        }
    }
}
