using System.Collections.Generic;
using UnityEngine;
using TMPro;
using game;

/// <summary>
/// La classe <c>DropDownMarket</c> permet d'afficher le graphe et les plantes.
/// Elle poss�de 4 attributs : dropdown, market, graphMarket, plTypeList.
/// </summary>
public class DropDownMarket : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public Market market;

    public GraphMarket graphMarket;

    private List<EnumTypePlant> plTypeList;

    /// <summary>
    /// La m�thode <c>Start</c> est utilis�e pour le d�marrage. �tant donn� que Start n'est appel�e qu'une seule fois, elle permet d'initialiser les �l�ments
    /// qui doivent persister tout au long de la vie du script, mais ne doivent �tre configur�s qu'imm�diatement avant utilisation.
    /// Pour notre cas elle r�cup�re tout les type de plante, afin de pouvoir les afficher dans un dropdown
    /// </summary>
    void Start()
    {
        plTypeList = CreateAllSeedPlant.dicoPlant.getAllPlantType();
        List<Plant> plList = new List<Plant>();
        foreach (EnumTypePlant plType in plTypeList)
        {
            plList.Add(CreateAllSeedPlant.dicoPlant.createPlant(plType));
        }


        dropdown.ClearOptions();


        List<TMP_Dropdown.OptionData> plantItems = new List<TMP_Dropdown.OptionData>();

        foreach (Plant pl in plList)
        {
            TMP_Dropdown.OptionData plantItem = new TMP_Dropdown.OptionData("", pl.getSprite());
            plantItems.Add(plantItem);
        }
        dropdown.AddOptions(plantItems);
    }

    /// <summary>
    /// La m�thode <c>updateGraph</c> permet de mettre � jour le graph
    /// </summary>
    /// <param name="newIndex">le nouveau point du graph</param>
    public void updateGraph(int newIndex)
    {
        graphMarket.changePlant(plTypeList[newIndex]);
    }
}