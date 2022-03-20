using UnityEngine;
using System;
using UnityEngine.Serialization;

[Serializable]
public struct AbilityTags
{
    /// <summary>
    /// This tag describes the gameplay tag
    /// </summary>
    [SerializeField] public TagScriptableObject AssetTag;

    /// <summary>
    /// Active Gameplay Abilities (on the same character) that have these tags will be cancelled;
    /// </summary>
    [SerializeField] public TagScriptableObject[] CancelAbilitiesWithTags;

    /// <summary>
    /// Gameplay Abilities that have these tags will be blocked from activating on the same character; 
    /// </summary>
    [SerializeField] public TagScriptableObject[] BlockAbilitiesWithTags;


    /// <summary>
    /// These tags are granted to the character while the ability is active 
    /// </summary>
    [SerializeField] public TagScriptableObject[] ActivateOwnTags;

    /// <summary>
    /// The abiltity can only be actibated if the owner character has all the required tags an none of the Ignore tags;
    /// Usually the owner is the source as well 
    /// </summary>
    [SerializeField] public TagRequiredIgnoreContainer OwnerTags;

    /// <summary>
    /// The ability can only be activated if the SOURCE character has all the required tags and none of the Ignore Tags;
    /// </summary>
    [SerializeField] public TagRequiredIgnoreContainer SourceTags;

    /// <summary>
    /// The ability can only be activate if the TARGET character has all the required tags and none of the Ignore Tags;
    /// </summary>
    [SerializeField] public TagRequiredIgnoreContainer TargetTags;



}
