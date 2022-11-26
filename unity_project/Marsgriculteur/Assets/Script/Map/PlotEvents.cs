using game;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlotEvents : MonoBehaviour
{
    public Sprite plot_sprite;
    public Sprite plot_sprite_highlite;
    private Sprite seed_sprite;
    private Sprite seed_sprite_grown;


    public Canvas InterfacePlant;



    private int growthTime;
    private int growthStatus;
    private Transform plotImage;
    private Transform seedImage;
    private PlantedPlant plantedPlant;
    private Boolean contientGraine = false;


    //on appelle le dico du inventory Player, puis on le modifie selon
    //recup plante
    //ou plantation

    public PlayerInventory playerInventory;



    private void Start()
    {

        List<Transform> children = GetChildren(transform);
        foreach (Transform child in children)
        {
            if (child.gameObject.name == "seedImage")
            {
                seedImage = child;
                seedImage.gameObject.GetComponent<SpriteRenderer>().sprite = seed_sprite;
            }
            else
            {
                plotImage = child;
                plotImage.gameObject.GetComponent<SpriteRenderer>().sprite = plot_sprite;
            }
        }


        PlantedPlant pplant = CreateAllSeedPlant.dicoPlant.createPlantedPlant(EnumTypePlant.ELB);



        donnePlantedPlante(pplant);
    }

    public void donnePlantedPlante(PlantedPlant pl)
    {
        plantedPlant = pl;
        List<Sprite> listeSprites = pl.getSpriteLinks();
        seed_sprite = listeSprites[0];
        seed_sprite_grown = listeSprites[1];
        growthTime = pl.getGrowthTime();
        growthStatus = 0;

        seedImage.gameObject.GetComponent<SpriteRenderer>().sprite = seed_sprite;
        contientGraine = true;
    }

    public void fairePousser()
    {
        if (!contientGraine)
        {
            return;
        }

        if (growthStatus < growthTime)
        {
            growthStatus++;
        }

        if (growthStatus == growthTime)
        {
            seedImage.gameObject.GetComponent<SpriteRenderer>().sprite = seed_sprite_grown;
        }
    }

    public void recupPlante()
    {
        //playerInventory.addToInventory((CreateAllSeedPlant.dicoPlant.createSeed(EnumTypePlant.ELB)),15);
        //playerInventory.SubstractFromInventory((CreateAllSeedPlant.dicoPlant.createSeed(EnumTypePlant.ELB)),15);
        playerInventory.removeFromInventory(CreateAllSeedPlant.dicoPlant.createSeed(EnumTypePlant.ELB));
    }

    public void planteGraine()
    {
        //ouvre l'inventaire, pour planter une graine
    }


    void OnMouseDown()
    {
        //recupPlante();
        //InventoryPanel.SetActive(true);
        InterfacePlant.gameObject.SetActive(true);
        //RectTransform InventoryRECT = InventoryPanel.GetComponent<RectTransform>();

        //float camHeight = cam.orthographicSize;
        //float camWidth = cam.orthographicSize * cam.aspect;

        //InventoryRECT.sizeDelta.Set(camWidth/2, camHeight*0.9);
    }

    List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }

    void OnMouseOver()
    {
        plotImage.gameObject.GetComponent<SpriteRenderer>().sprite = plot_sprite_highlite;
    }

    void OnMouseExit()
    {
        plotImage.gameObject.GetComponent<SpriteRenderer>().sprite = plot_sprite;
    }
}
