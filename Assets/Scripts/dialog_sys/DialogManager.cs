using System.Collections.Generic;
using Newtonsoft.Json;
using UI.Dialogs;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextAsset dialogJSON;
    private Dictionary<string, Dictionary<string, string>> dialogs;
    private uDialog dialog;

    public FirstPersonLook playerLook;
    private bool inConversation = false;


    // tracks if instructions have been shown (only once)
    private const string InstructionsShownKey = "InstructionsScene";
    public string instructionDialogKey = "instructions";


    void Start()
    {
        if (playerLook == null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        dialogs = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(dialogJSON.text);
        Debug.Log(dialogs["test1"]["1"]);



        // check if the instructions have been shown before
        if (PlayerPrefs.GetInt(InstructionsShownKey, 0) == 0)
        {
            
            ShowLine(instructionDialogKey, "1", "2"); 
            
            // set the flag so it doesn't show again next time
            PlayerPrefs.SetInt(InstructionsShownKey, 1);
            PlayerPrefs.Save();
        }



    }

    void Update()
    {
        if (inConversation && (dialog == null || !dialog.gameObject.activeInHierarchy))
        {
            ResetPlayerControl();
        }
    }

    public string GetLine(string npc, string id)
    {
        if (dialogs.ContainsKey(npc) && dialogs[npc].ContainsKey(id))
            return dialogs[npc][id];

        return null;
    }

    public void ShowLine(string npc, string id, string nextId = null)
    {
        string line = GetLine(npc, id);
        if(line == null)
        {
            ResetPlayerControl();
            return;
        }

        if (!inConversation)
        {
            if (playerLook != null)
            {      
                playerLook.canLook = false;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            inConversation = true;
        }

        if (dialog != null) dialog.Close();

        dialog = uDialog.NewDialog()
            .SetTitleText(npc)
            .SetContentText(line)
            .SetThemeImageSet(eThemeImageSet.Fantasy)
            .SetColorScheme("Dark")
            .SetModal(true);

        RectTransform rt = dialog.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);
        rt.anchoredPosition = new Vector2(0f, 0f);
        rt.sizeDelta = new Vector2(0f, 250f);

        int nextLineNum;
        if (nextId != null && int.TryParse(nextId, out nextLineNum) && GetLine(npc, nextId) != null)
        {
            dialog.AddButton("Next", (d) =>
            {
                d.Close();
                //ResetPlayerControl();
                ShowLine(npc, (nextLineNum).ToString(), (nextLineNum + 1).ToString());
            });
        }
        else
        {
            dialog.AddButton("Close", (d) =>
            {
                d.Close();
                ResetPlayerControl();
            });
        }

        dialog.Show();
    }
    
    private void ResetPlayerControl()
    {
        if (playerLook == null)
        {
            dialog = null;
            inConversation = false;
            return;
        }
        playerLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialog = null;
        inConversation = false;
    }
}
