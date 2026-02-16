using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class VersionText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TMP_Text>().text = "ver." + Application.version;
    }
}
