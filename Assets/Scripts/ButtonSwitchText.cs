using UnityEngine;
using TMPro;

// a class that changes the button text when the button is pressed
public class ButtonSwitchText : MonoBehaviour
{
    private TextMeshPro textMesh
    {
        get
        {
            return GetComponent<TextMeshPro>();
        }
    }

    // reference enum
    public enum CurrentShaderType
    {
        Toon, Texture
    }

    public CurrentShaderType currentShaderType;

    // change the button text to indecate the toon shader
    public void ToonShaderIndicatorText()
    {
        currentShaderType = CurrentShaderType.Toon;
        textMesh.text = "toon shader";
    }

    // change the button text to indecate the texture shader
    public void TextureShaderIndicatorText()
    {
        currentShaderType = CurrentShaderType.Texture;
        textMesh.text = "texture shader";
    }
}
