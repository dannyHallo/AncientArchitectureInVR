using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShowCase : MonoBehaviour
{
    public LodSet gongDian1;
    public LodSet gongDian2;
    public LodSet gongDian3;
    public LodSet tower;

    [System.Serializable]
    public struct LodSet
    {
        public GameObject LOD_0;
        public GameObject LOD_1;

        public void SetActive(bool value)
        {
            LOD_0.SetActive(value);
            LOD_1.SetActive(value);
        }

        public void ShowLOD0()
        {
            LOD_0.SetActive(true);
            LOD_1.SetActive(false);
        }

        public void ShowLOD1()
        {
            LOD_0.SetActive(false);
            LOD_1.SetActive(true);
        }
    }

    private LodSet socket;

    private void ClearRenderingArea()
    {
        gongDian1.SetActive(false);
        gongDian2.SetActive(false);
        gongDian3.SetActive(false);
        tower.SetActive(false);
    }

    public void ShowGongdian1()
    {
        ClearRenderingArea();
        gongDian1.ShowLOD1();
        socket = gongDian1;
    }

    public void ShowGongdian2()
    {
        ClearRenderingArea();
        gongDian2.ShowLOD1();
        socket = gongDian2;
    }

    public void ShowGongdian3()
    {
        ClearRenderingArea();
        gongDian3.ShowLOD1();
        socket = gongDian3;
    }

    public void ShowTower()
    {
        ClearRenderingArea();
        tower.ShowLOD1();
        socket = tower;
    }

    public void RenderBuilding()
    {
        socket.ShowLOD0();
    }
}
