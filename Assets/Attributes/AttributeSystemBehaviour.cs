using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 
/// </summary>
public class AttributeSystemBehaviour : MonoBehaviour
{

    [SerializeField] private AbstractAttributeEventHandler[] AttributeSystemEvents;

    /// <summary>
    /// Attributes sets assigned to the game characters
    /// </summary>
    [SerializeField] private List<AttributeScriptableObject> Attributes;

    [SerializeField] private List<AttributeValue> AttributeValues;

    private bool mAttributeDictStale;
    public Dictionary<AttributeScriptableObject, int> mAttributeIndexCache { get; private set; } = new Dictionary<AttributeScriptableObject, int>();

    /// <summary>
    /// Markes the attribute cache as dirty, so it can be recreated next time if required
    /// </summary>
    public void MarkAttributesDirty()
    {
        this.mAttributeDictStale = true;
    }
    /// <summary>
    /// Gets the value of an index. note that the value of this attr is a copy struct
    /// so modifying it will not affect the original attr.
    /// </summary>
    /// <param name="attr">Attribute object</param>
    /// <param name="v">returned attribute</param>
    /// <returns>true if it was found, false otherwise.</returns>
    public bool GetAttributeValue(AttributeScriptableObject attr,out AttributeValue v)
    {
        RecomputeAttributeCacheIfNeeded();
        if(mAttributeIndexCache.TryGetValue(attr,out var index))
        {
            v = AttributeValues[index];
            return true;
        }
        v = new AttributeValue();
        return false;
    }

    public void SetAttributeBaseValue(AttributeScriptableObject attr,float value)
    {
        RecomputeAttributeCacheIfNeeded();
        if (mAttributeIndexCache.TryGetValue(attr, out var index))
        {
           return;
        }
        var attrValue = AttributeValues[index];
        attrValue.BaseValue = value;
        AttributeValues[index] = attrValue;
    }

    /// <summary>
    /// Update the modifier of an attribute.
    /// </summary>
    /// <param name="attr">Attribute to se</param>
    /// <param name="modif">Modifier</param>
    /// <param name="value">Copy of the struct. Any change made to this value</param>
    /// <returns></returns>
    public bool UpdateAttributeModifiers(AttributeScriptableObject attr, AttributeModifier modif,out AttributeValue value)
    {
        RecomputeAttributeCacheIfNeeded();
        if (mAttributeIndexCache.TryGetValue(attr, out var index))
        {
            value = AttributeValues[index];
            value.Modifier = value.Modifier.Combine(modif);
            AttributeValues[index] = value;
            return true;
        }
        value = new AttributeValue();
        return false;
    }

    /// <summary>
    /// Add attributes to this atrr system. Duplicates are ignored.
    /// </summary>
    /// <param name="attrs">Attributes to add</param>
    public void AddAttributes(params AttributeScriptableObject[] attrs)
    {
        RecomputeAttributeCacheIfNeeded();
        for(int i = 0;i< attrs.Length; i++)
        {
            if (mAttributeIndexCache.ContainsKey(attrs[i]))
            {
                continue;
            }
            Attributes.Add(attrs[i]);
            mAttributeIndexCache.Add(attrs[i], Attributes.Count - 1);
        }
    }


    /// <summary>
    /// Add attributes to this atrr system. Duplicates are ignored.
    /// </summary>
    /// <param name="attrs">Attributes to add</param>
    public void RemoveAttributes(params AttributeScriptableObject[] attrs)
    {
        for (int i = 0; i < attrs.Length; i++)
        {
            Attributes.Remove(attrs[i]);
        }
        mAttributeDictStale = true;
        RecomputeAttributeCacheIfNeeded();

    }


    public void ResetAllAttributes()
    {
        for(int i = 0; i < AttributeValues.Count; i++)
        {
            var defaultAttribute = new AttributeValue();
            defaultAttribute.Attribute = this.AttributeValues[i].Attribute;
            this.AttributeValues[i] = defaultAttribute;
        }
    }

    public void ResetAllAttributeModifiers()
    {
        for (int i = 0; i < AttributeValues.Count; i++)
        {
            var attrValue = AttributeValues[i];
            attrValue.Modifier = default;
            this.AttributeValues[i] = attrValue;
        }
    }

    private void RecomputeAttributeCacheIfNeeded()
    {
        if (mAttributeDictStale)
        {
            mAttributeIndexCache.Clear();
            for (int i = 0; i < Attributes.Count; i++)
            {
                mAttributeIndexCache.Add(AttributeValues[i].Attribute, i);
            }
            this.mAttributeDictStale = false;
        }
    }


    private void InitialiseAttributeValues()
    {
        this.AttributeValues = new List<AttributeValue>();
        for (var i = 0; i < Attributes.Count; i++)
        {
            this.AttributeValues.Add(new AttributeValue()
            {
                Attribute = this.Attributes[i],
                Modifier = new AttributeModifier()
                {
                    Add = 0f,
                    Multiply = 0f,
                    Override = 0f
                }
            }
            );
        }

    }

    private List<AttributeValue> prevAttributeValues = new List<AttributeValue>();
    public void UpdateAttributeCurrentValues()
    {
        prevAttributeValues.Clear();
        for (var i = 0; i < this.AttributeValues.Count; i++)
        {
            var _attribute = this.AttributeValues[i];
            prevAttributeValues.Add(_attribute);
            this.AttributeValues[i] = _attribute.Attribute.CalculateCurrentAttributeValue(_attribute, this.AttributeValues);
        }

        for (var i = 0; i < this.AttributeSystemEvents.Length; i++)
        {
            this.AttributeSystemEvents[i].PreAttributeChange(this, prevAttributeValues, ref this.AttributeValues);
        }
    }



    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
