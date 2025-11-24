using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "TaskInfoSO", menuName = "ScriptableObject/TaskMenuSO", order = 1)]
public class TaskInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;
    public TaskInfoSO[] taskPreReqs;
    
    [Header("Steps")]
    public GameObject[] taskStepPrefabs;

    [Header("Rewards")]
    public int moneyReward;
    public int seniorityReward;

// ensures that the id is always the same name of the scriptable object asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}

