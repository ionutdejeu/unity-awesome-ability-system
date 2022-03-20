using System;
[Serializable]
public struct AttributeValue
{
    public AttributeScriptableObject Attribute;
    public float BaseValue;
    public float CurrentValue;
    public AttributeModifier Modifier;
}
