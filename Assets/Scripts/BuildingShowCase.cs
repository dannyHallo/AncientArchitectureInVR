using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShowCase : MonoBehaviour
{
    public LodSet gongDian1;
    public LodSet gongDian2;
    public LodSet gongDian3;
    public LodSet tower;
    public ButtonSwitchText buttonSwitchText;

    private Oculus.Interaction.BuildingHandInteraction buildingHandInteraction
    {
        get
        {
            return GetComponent<Oculus.Interaction.BuildingHandInteraction>();
        }
    }

    [System.Serializable]
    public struct LodSet
    {
        public GameObject LOD_0;
        public GameObject LOD_1;
        private LodSelection lodSelection;

        private enum LodSelection
        {
            Unselected, Lod0, Lod1
        }

        public bool isUsingLOD0
        {
            get
            {
                return lodSelection == LodSelection.Lod0;
            }
        }

        public bool isUsingLOD1
        {
            get
            {
                return lodSelection == LodSelection.Lod1;
            }
        }

        public void Disable()
        {
            LOD_0.SetActive(false);
            LOD_1.SetActive(false);
            lodSelection = LodSelection.Unselected;
        }

        public GameObject GetActiveLOD()
        {
            switch (lodSelection)
            {
                case LodSelection.Unselected:
                    return null;
                case LodSelection.Lod0:
                    return LOD_0;
                case LodSelection.Lod1:
                    return LOD_1;
            }

            return null;
        }

        public void ShowLOD0()
        {
            if (isUsingLOD0) return;
            // Switch gameObj, transfer rotation data, and scaling data
            LOD_0.transform.rotation = LOD_1.transform.rotation;
            LOD_0.transform.localScale = LOD_1.transform.localScale;
            LOD_0.SetActive(true);
            LOD_1.SetActive(false);
            lodSelection = LodSelection.Lod0;
        }

        public void ShowLOD1()
        {
            if (isUsingLOD1) return;
            // Switch gameObj, transfer rotation data, and scaling data
            LOD_1.transform.rotation = LOD_0.transform.rotation;
            LOD_1.transform.localScale = LOD_0.transform.localScale;
            LOD_0.SetActive(false);
            LOD_1.SetActive(true);
            lodSelection = LodSelection.Lod1;
        }
    }

    private LodSet socket;

    private void ClearRenderingArea()
    {
        gongDian1.Disable();
        gongDian2.Disable();
        gongDian3.Disable();
        tower.Disable();

        buildingHandInteraction.building = null;
    }

    public void ShowGongdian1()
    {
        ClearRenderingArea();

        if (socket.isUsingLOD0)
        {
            gongDian1.ShowLOD0();
            buttonSwitchText.TextureShaderIndicatorText();
        }
        else
        {
            gongDian1.ShowLOD1();
            buttonSwitchText.ToonShaderIndicatorText();
        }

        socket = gongDian1;
        buildingHandInteraction.building = socket.GetActiveLOD().transform;
    }

    public void ShowGongdian2()
    {
        ClearRenderingArea();

        if (socket.isUsingLOD0)
        {
            gongDian2.ShowLOD0();
            buttonSwitchText.TextureShaderIndicatorText();
        }
        else
        {
            gongDian2.ShowLOD1();
            buttonSwitchText.ToonShaderIndicatorText();
        }

        socket = gongDian2;
        buildingHandInteraction.building = socket.GetActiveLOD().transform;
    }

    public void ShowGongdian3()
    {
        ClearRenderingArea();

        if (socket.isUsingLOD0)
        {
            gongDian3.ShowLOD0();
            buttonSwitchText.TextureShaderIndicatorText();
        }
        else
        {
            gongDian3.ShowLOD1();
            buttonSwitchText.ToonShaderIndicatorText();
        }

        socket = gongDian3;
        buildingHandInteraction.building = socket.GetActiveLOD().transform;
    }

    public void ShowTower()
    {
        ClearRenderingArea();

        if (socket.isUsingLOD0)
        {
            tower.ShowLOD0();
            buttonSwitchText.TextureShaderIndicatorText();
        }
        else
        {
            tower.ShowLOD1();
            buttonSwitchText.ToonShaderIndicatorText();
        }

        socket = tower;
        buildingHandInteraction.building = socket.GetActiveLOD().transform;
    }

    public void SwitchRenderMethod()
    {
        if (socket.isUsingLOD0)
        {
            socket.ShowLOD1();
            buttonSwitchText.ToonShaderIndicatorText();
        }
        else
        {
            socket.ShowLOD0();
            buttonSwitchText.TextureShaderIndicatorText();
        }

        buildingHandInteraction.building = socket.GetActiveLOD().transform;
    }
}
