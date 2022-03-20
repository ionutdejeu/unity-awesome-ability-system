using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Abilities/Tag")]
public class TagScriptableObject : ScriptableObject
{

    [SerializeField]
    private TagScriptableObject _parent;
    public TagScriptableObject Parent { get { return _parent; } }
    
}
