using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


namespace game
{
    /// <summary>
    /// La classe <c>Inventory</c> s'occupe des inventaires.
    /// Elle possède les attributs suivant: panel (qui correspond au panel sur lequel l'inventaire va être présenté),
    /// weightMax (tous les inventaires ont une capacité maximum), slots (qui correspond aux items qui vont remplir l'inventaire)
    /// et currentWeight qui correspond à la capacité actuelle de l'inventaire
    /// </summary>
    public class Inventory : MonoBehaviour
    {

        public InventoryInterface panel;

        private int weightMax = 1000;

        //dictionnaire des items pour remplir le inventory
        //si on ajoute un item on doit ajouter à ce dico pour le afficher
        private Dictionary<BasicItem, int> slots = new Dictionary<BasicItem, int>();

        private int currentWeight = 0;


        /// <summary>
        /// La méthode <c>addToInventory</c> permet d'ajouter un slot au dictionnaire
        /// </summary>
        /// <param name="item">l'item qui sera ajouté à l'inventaire</param>
        /// <param name="qtt">la quantité de cet item</param>
        public void addToInventory(BasicItem item, int qtt)
        {
            bool trouve = false;

            if (qtt < 1)
                return;
            else
            {
                //on parcourt chaque key pour acceder a son getId()
                foreach (BasicItem kvp in slots.Keys.ToList())
                {
                    if (kvp.getId() == item.getId())
                    {
                        slots[kvp] += qtt;
                        trouve = true;
                        break;
                    }
                }
                //si on le trouve pas on l'ajoute a notre 
                if (!trouve)
                {
                    slots.Add(item, qtt);
                }
                currentWeight += item.getWeight() * qtt;
            }

            //displayInventory();

        }

        /// <summary>
        /// La méthode <c>removeFromInventory</c> permet d'enlever instantanément un item.
        /// </summary>
        /// <param name="item">l'item qui sera supprimé de l'inventaire</param>
        //permet d'ajouter un slot au dictionnaire
        //surcharge la methode addToInventory avec un seul argument
        public void addToInventory(BasicItem item, int qtt, Dictionary<BasicItem, int> dico)
        {
            bool trouve = false;

            if (qtt < 1)
                return;
            else
            {
                //on parcourt chaque key pour acceder a son getId()
                foreach (BasicItem kvp in dico.Keys.ToList())
                {
                    if (kvp.getId() == item.getId())
                    {
                        dico[kvp] += qtt;
                        trouve = true;
                        break;
                    }
                }
                //si on le trouve pas on l'ajoute a notre 
                if (!trouve)
                {
                    dico.Add(item, qtt);
                }
                //currentWeight += item.getWeight() * qtt;
            }



        }
        //removes an item instantly
        public void removeFromInventory(BasicItem item)
        {
            foreach (BasicItem kvp in slots.Keys.ToList())
            {
                if (kvp.getId() == item.getId())
                {
                    slots.Remove(kvp);
                    break;
                }
            }
            //displayInventory();
        }

        // Il y a 2 fonctions surchargées, une où on envoie le dico comme paramètre, une où on utilise le dico global value.

        /// <summary>
        /// La méthode <c>SubstractFromInventory</c> permet de soustraire une quantité à un item qui se trouve déjà dans notre inventory ou de l'éliliminer complètement.
        /// </summary>
        /// <param name="item">l'item</param>
        /// <param name="qttToRemove">la quantité qui va être soustraite</param>
        public void SubstractFromInventory(BasicItem item, int qttToRemove)
        {
            if (qttToRemove < 1)
                return;
            else
            {
                foreach (BasicItem kvp in slots.Keys.ToList())
                {
                    if (kvp.getId() == item.getId())
                    {
                        if (slots[kvp] > qttToRemove)
                            slots[kvp] -= qttToRemove;
                        else
                            slots.Remove(kvp);

                        break;
                    }
                }
            }

            //displayInventory();
        }

        /// <summary>
        /// La méthode <c>SubstractFromInventory</c> avec comme paramètre le dicoASoustraire permet de soustraire une quantité à un item qui se trouve déjà dans notre inventory ou de l'éliliminer complètement.
        /// </summary>
        /// <param name="item">l'item</param>
        /// <param name="qttToRemove">la quantité qui va être soustraite</param>
        /// <param name="dicoASoustraire">le dictionnaire qui contient les items</param>
        public void SubstractFromInventory(BasicItem item, int qttToRemove, Dictionary<BasicItem, int> dicoASoustraire)
        {
            if (qttToRemove < 1)
                return;
            else
            {
                foreach (BasicItem kvp in dicoASoustraire.Keys.ToList())
                {
                    if (kvp.getId() == item.getId())
                    {
                        if (dicoASoustraire[kvp] > qttToRemove)
                            dicoASoustraire[kvp] -= qttToRemove;
                        else
                            dicoASoustraire.Remove(kvp);

                        break;
                    }
                }
            }

            //displayInventory();
        }

        /// <summary>
        /// La méthode <c>getInventory</c> permet d'obtenir l'inventory
        /// </summary>
        /// <returns>Elle renvoie un dictionnaire (l'item avec sa quantité)</returns>
        public Dictionary<BasicItem, int> getInventory()
        {
            return this.slots;
        }

        /// <summary>
        /// La méthode <c>getWeightMax</c> permet d'obtenir la capacité max de l'inventory
        /// </summary>
        /// <returns>Elle retourne sa capacité max</returns>
        public int getWeightMax()
        {
            return this.weightMax;
        }

        /// <summary>
        /// La méthode <c>getCurrentWeight</c> permet d'obtenir la capacité actuelle de l'inventory
        /// </summary>
        /// <returns>Elle retourne sa capacité actuelle</returns>
        public int getCurrentWeight()
        {
            return this.currentWeight;
        }

        /// <summary>
        /// La méthode <c>isDicoVide</c> permet de savoir si l'inventaire est vide
        /// </summary>
        /// <returns>Elle retourne un booléen : false s'il n'est pas vide et true si c'est le cas</returns>
        public bool isDicoVide()
        {
            //Debug.Log("nb slots : " + slots.Count);
            //Debug.Log("nb slots THIS : " + this.slots.Count);

            if (slots.Count > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// La méthode <c>displayInventory</c> instancie le slot pour tout élément : il aura son image etc et on l'ajoute à l'inventory
        /// </summary>
        public void displayInventory()
        {
            panel.afficheInventory(slots);
        }

        /// <summary>
        /// La méthode <c>ToString</c> permet d'afficher l'inventaire.
        /// </summary>
        /// <returns>Elle retourne une chaîne de caractère avec son nom et sa quantité</returns>
        override public string ToString()
        {
            string rtr = string.Empty;
            foreach (KeyValuePair<BasicItem, int> kvp in slots)
            {
                rtr += kvp.Key.getName() + "\t : " + kvp.Value.ToString() + "\n";
            }
            return rtr;
        }


    }
}