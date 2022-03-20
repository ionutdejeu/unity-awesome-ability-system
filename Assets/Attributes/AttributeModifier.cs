using System;

[Serializable]
public struct AttributeModifier
{
    public float Add;
    public float Multiply;
    public float Override;
    public AttributeModifier Combine(AttributeModifier other)
    {
        other.Add += Add;
        other.Multiply += Multiply;
        other.Override = Override;
        return other;
    }
}