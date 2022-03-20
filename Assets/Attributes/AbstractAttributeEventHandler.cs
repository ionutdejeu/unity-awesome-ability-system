using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractAttributeEventHandler : ScriptableObject
{
    public abstract void PreAttributeChange(AttributeSystemBehaviour attributeSystem, List<AttributeValue> prevAttributeValues, ref List<AttributeValue> currentAttributeValues);
}
