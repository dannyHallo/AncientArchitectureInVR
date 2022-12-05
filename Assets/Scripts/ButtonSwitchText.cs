using UnityEngine;
using TMPro;

public class ButtonSwitchText : MonoBehaviour
{
    private TextMeshPro textMesh
    {
        get
        {
            return GetComponent<TextMeshPro>();
        }
    }

    public enum CurrentShaderType
    {
        Toon, Texture
    }

    public CurrentShaderType currentShaderType;

    public void ToonShaderIndicatorText()
    {
        currentShaderType = CurrentShaderType.Toon;
        textMesh.text = "toon shader";
    }


    public void TextureShaderIndicatorText()
    {
        currentShaderType = CurrentShaderType.Texture;
        textMesh.text = "texture shader";
    }

    // public void SwitchShader()
    // {
    //     if (currentShaderType == CurrentShaderType.Toon)
    //     {
    //         print("toon -> texture");
    //         currentShaderType = CurrentShaderType.Texture;
    //         TextureShaderIndicatorText();
    //     }
    //     else
    //     {
    //         print("texture -> toon");
    //         currentShaderType = CurrentShaderType.Toon;
    //         ToonShaderIndicatorText();
    //     }

    // }
}
