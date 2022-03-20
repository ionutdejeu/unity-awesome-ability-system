using System;
[Serializable]
public struct TagRequiredIgnoreContainer
{
    /// <summary>
    /// All of these tags must be present
    /// </summary>
    public TagScriptableObject[] RequireTags;

    /// <summary>
    /// None of these tags can be present
    /// </summary>
    public TagScriptableObject[] IgnoreTags;
}
