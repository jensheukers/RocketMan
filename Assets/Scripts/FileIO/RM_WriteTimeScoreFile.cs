using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Writes a timescore file to m_fileName destination
/// </summary>
public class RM_WriteTimeScoreFile : MonoBehaviour {
    [SerializeField]
    private string m_fileName = "timescores.txt"; /** Timescore file reference*/
    
    public void WriteTimeScoreToFile() {
        string playerName = RM_GameState.GetPlayerName();

        StreamWriter w = new StreamWriter(Application.persistentDataPath + "/" + m_fileName, true);
        w.WriteLine(playerName + " : " + Time.time.ToString());
        w.Close();
    }

    public void ClearFile() {
        StreamWriter w = new StreamWriter(Application.persistentDataPath + "/" + m_fileName);
        w.WriteLine("");
        w.Close();
    }
}
