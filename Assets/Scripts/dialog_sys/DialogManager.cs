using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextAsset dialogJSON;
    private Dictionary<string, Dictionary<string, string>> dialogs;
    void Start()
    {
        dialogs = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(dialogJSON.text);
        Debug.Log(dialogs["test1"]["1"]);
    }

    public string GetLine(string npc, string id)
    {
        if (dialogs.ContainsKey(npc) && dialogs[npc].ContainsKey(id))
            return dialogs[npc][id];

        return $"No line:{id} for {npc}";
    }
}
