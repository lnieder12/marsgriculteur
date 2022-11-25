using game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace game
{
    public class NextDay : MonoBehaviour
    {
        public TextMeshProUGUI dayText;
        public Transform plots;
        List<Transform> plotList; //contient tous les plots pour les faire pousser
        private int nbrJour;
        private Market market;
        //private AllEvents allEvents;

        public static Dictionary<EventInfo, int> dicoPossessions = new Dictionary<EventInfo, int>();

        void Start()
        {
            if (plots == null) //pour �viter de planter (ahah "plant")
            {
                return;
            }


            GetPlots(plots);
            nbrJour = 0;
            dayText.SetText(nbrJour.ToString());

            market = new Market();
            market.createMarket();
            dicoPossessions = market.getActiveEvents();

            AllEvents all = new AllEvents();

            /*dicoPossessions.Add(all.allEventDico["vegeTrend"], all.allEventDico["vegeTrend"].length);
            dicoPossessions.Add(all.allEventDico["qualiMeat"], all.allEventDico["qualiMeat"].length);
            dicoPossessions.Add(all.allEventDico["solarStorm"], all.allEventDico["solarStorm"].length);*/

            Debug.Log("dic" + dicoPossessions.Count);

        }

        public static Dictionary<EventInfo, int> getInventoryNotif()
        {
            return dicoPossessions;
        }

        void OnMouseDown()
        {
            faitPousser();
            nbrJour++;
            dayText.SetText(nbrJour.ToString());
        }

        public void faitPousser() //parcours chaque plot, puis appelle leur fonction fairePousser
        {
            foreach (Transform transforme in plotList)
            {
                if(transforme.name.Length>4 || transforme.name.Substring(0, 4) == "plot")
                {
                    try
                    {
                        transforme.gameObject.SendMessage("fairePousser");
                    }
                    catch
                    {
                        //Debug.Log("Bug dans faire pousser, l'appel de la fonction de fairePousser avec \"" + transforme.name + "\" n'a pas march�");
                    }
                    
                }
                else
                {
                    //Debug.Log("Bug dans faire pousser, le transform \"" + transforme.name + "\" est dans la liste des shops :/");
                }
            }
        }


        private void GetPlots(Transform parent) //recupere les plots
        {
            plotList = new List<Transform>();
            foreach (Transform child in parent)
            {
                plotList.Add(child);
            }
            return;
        }


        //permet d'ajouter un slot au dictionnaire
        public void addToInventory(EventInfo item, int duree)
        {
            bool trouve = false;

            if (duree < 1)
                return;
            else
            {
                //on parcourt chaque key pour acceder a son getId()
                foreach (EventInfo kvp in dicoPossessions.Keys.namee.ToList())
                {
                    if (kvp.getId() == item.getId())
                    {
                        dicoPossessions[kvp] += duree;
                        trouve = true;
                        break;
                    }
                }
                //si on le trouve pas on l'ajoute a notre 
                if (!trouve)
                {
                    dicoPossessions.Add(item, duree);
                }
            }

            notif.afficheInventory(dicoPossessions);

        }

        //removes an item instantly
        public void removeFromInventory(EventInfo item)
        {
            foreach (EventInfo kvp in dicoPossessions.Keys.ToList())
            {
                if (kvp.getId() == item.getId())
                {
                    dicoPossessions.Remove(kvp);
                    break;
                }
            }

            notif.afficheInventory(dicoPossessions);
        }
    }
}
