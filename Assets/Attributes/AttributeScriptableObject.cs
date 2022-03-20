using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PreAttributeChangedEventArgs : EventArgs
{
    AttributeSystemBehaviour System { get;  set; }
    float Falue { get; set; }
}

[CreateAssetMenu(menuName = "AbilitySystem/Attribute")]
public class AttributeScriptableObject : ScriptableObject
{
    public string Name;
    public event EventHandler PreAttributeChangeEventHandler;
    public void OnPreAttributeChange(object sender,PreAttributeChangedEventArgs e)
    {
        EventHandler handler = PreAttributeChangeEventHandler;
        PreAttributeChangeEventHandler?.Invoke(sender, e);

    }

    public virtual AttributeValue CalculateInitialValue(AttributeValue v,List<AttributeValue> otherValues)
    {
        return v;
    }

    public virtual AttributeValue CalculateCurrentAttributeValue(AttributeValue v,List<AttributeValue> otherValues)
    {
        v.CurrentValue = (v.BaseValue + v.Modifier.Add) * (v.Modifier.Multiply + 1);
        if(v.Modifier.Override != 0)
        {
            v.CurrentValue = v.Modifier.Override;
        }
        return v;
    }
}
