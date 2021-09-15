using UnityEngine;

public class CharacterVisitor
{
    public GameObject InstantiateVisitor(GameObject visitor)
    {
        GameObject _visitor = Object.Instantiate(visitor, visitor.transform.position, visitor.transform.rotation);

        return _visitor;
    }
}
