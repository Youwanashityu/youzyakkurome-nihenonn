using TMPro;
using UnityEngine;

public class LicensesText : MonoBehaviour
{
    [SerializeField] private TextAsset licenseTextAsset;
    [SerializeField] private TMP_Text textComponent;

    private void Awake()
    {
        if (licenseTextAsset == null || textComponent == null)
        {
            Debug.LogWarning("License text asset or text component is not assigned.");
            return;
        }
        
        textComponent.text = licenseTextAsset.text;
        
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying) return;
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
    }
}
