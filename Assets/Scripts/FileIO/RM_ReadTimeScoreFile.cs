using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

/// <summary>
/// Reads a timescore file stored in Application.PersistentDataPath.
/// Value can be received by using GetString()
/// </summary>
public class RM_ReadTimeScoreFile : MonoBehaviour {

    [SerializeField]
    private string m_fileName = "timescores.txt"; /** Timescore file reference*/

    [SerializeField]
    private TMP_Text m_textTarget; /** The target text component instance, if set this text will automaticly be changed to file data on start*/

    private string m_text; /** The text data read in start method*/

    private void Start() {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + m_fileName)) return;
        m_text = ReadFile();
        
        if (m_textTarget) m_textTarget.text = m_text;
    }

    /// <summary>
    /// Reads the file from m_Filename
    /// </summary>
    /// <returns>string</returns>
    private string ReadFile() {
        string[] lines = File.ReadAllLines(Application.persistentDataPath + "/" +  m_fileName);

        string _t = "";
        for (int i = 0; i < lines.Length; i++) {
            _t += lines[i] + "\n";
        }

        return _t;
    }

    /// <summary>
    /// Returns the text data
    /// </summary>
    /// <returns></returns>
    public string GetText() { return m_text; }
}
