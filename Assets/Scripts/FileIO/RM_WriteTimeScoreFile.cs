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
    
    /**
     * @brief Writes the time score file to Application.persistenDataPath (I.E %appdata%/LocalLow/COMPANY/RocketMan/)
     * @return string time
     */
    public string WriteTimeScoreToFile() {
        string _t = Time.time.ToString().Normalize();
        string playerName = RM_GameState.GetPlayerName();
        string levelName = RM_GameState.GetCurrentMission().MissionData().missionName;

        StreamWriter w = new StreamWriter(Application.persistentDataPath + "/" + m_fileName, true);
        w.WriteLine(playerName + " : " + _t + " - " + levelName);
        w.Close();

        return _t;
    }

    /**
     * Clears the referenced file
     */
    public void ClearFile() {
        StreamWriter w = new StreamWriter(Application.persistentDataPath + "/" + m_fileName);
        w.WriteLine("");
        w.Close();
    }
}
