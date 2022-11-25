using game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;




public class graphMarket : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    public RectTransform graphContainer;
    public TextMeshProUGUI labelTemplateY;
    public TextMeshProUGUI labelTemplateX;
    public RectTransform dashTemplateY;
    public RectTransform dashTemplateX;
    [SerializeField] public Market market;
    public TextMeshProUGUI titre;


    private float yMaximum; //maximum de la hauteur, pour �quilibrer l'affichage
    private float xSize; //maximum de la longueur, pour �quilibrer l'affichage
    private float xMaximum; 
    private float graphHeight; //hauteur de container, calcul� en amont pour permettre certaines modifications
    private float yMin;
    private int numberOfDays;
    private List<string> monthList;

    private List<GameObject> allChildsToSuppr = new List<GameObject>();

    private void Start()
    {
        //instantie les list, c'est temporaire
        //List<int> graphList = new List<int>() { 0, 11, 9, 8, 2, 3, 7, 8, 15, 19, 17, 13, 12, 11, 9, 8, 2, 3, 7, 8, 15, 19, 17, 13 , 12, 11, 9, 8, 2, 3, 7, 8, 15, 19, 17, 13 , 12, 11, 9, 8, 2, 3, 7, 8, 15, 19, 17, 13 , 12, 11, 9, 8, 2, 3, 7, 8, 15, 19, 17, 13 };
        monthList = new List<string>() { "Janv", "Fev", "Mars", "Avril", "Mai", "Juin", "Juil", "Aout", "Sept", "Oct", "Nov", "Dec" };
        graphHeight = graphContainer.sizeDelta.y*0.9f; //hauteur du graph
        yMin = 10f; //pour rendre le graph plus beau


        //ShowGraph(graphList); //affiche le graph
    }

    public void affiche(){
        List<int> graphList = market.last60Days(EnumTypePlant.ELB);
        titre.text = "Cours du " + EnumTypePlant.ELB;

        yMaximum = graphList.Max(); //Y maximum pour l'affichage
        xSize = (float)(graphContainer.sizeDelta.x / (graphList.Count * 1.1)); //espace entre les points en X

        numberOfDays = market.getDays();

        if (graphList == null){
            Debug.Log("????");
            return;
        }

        ShowGraph(graphList); //affiche le graph
    }


    private GameObject CreateCircle(Vector2 anchoredPosition) //cr�er un cercle et le renvoie
    {
        GameObject gameobject = new GameObject("circle", typeof(Image));  //cr�er une image, qui sera le point

        gameobject.transform.SetParent(graphContainer,false);
        gameobject.GetComponent<Image>().sprite = circleSprite; //choix de l'image du point, permet de la choisir
        //on r�cup�re les donn�es type, taille, position et tt du points pour les modifier
        RectTransform rectTransform = gameobject.GetComponent<RectTransform>(); 
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        allChildsToSuppr.Add(gameobject);
        return gameobject;
    }

    private void ShowGraph(List<int> valueList) //affiche la liste donn� sous forme de graph,  avec en valeur de X les month
    {
        clearGraph();
        //cr�er les lignes horizontales
        int nbrSeperator = 8;
        for (int i = 0; i <= nbrSeperator; i++) //cr�er les valeurs Y, les lignes
        {
            float heightInScale = i * 1f / nbrSeperator;

            //creer le texte de la ligne
            TextMeshProUGUI labelY = Instantiate(labelTemplateY, graphContainer, false);
            labelY.gameObject.SetActive(true);
            labelY.rectTransform.anchoredPosition = new Vector2(0f,yMin + heightInScale * graphHeight);
            allChildsToSuppr.Add(labelY.gameObject);
            labelY.GetComponent<TextMeshProUGUI>().text = ((int)math.round(heightInScale * yMaximum)).ToString();


            //creer la ligne
            RectTransform dashY = Instantiate(dashTemplateY, graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.sizeDelta = new Vector2(graphContainer.sizeDelta.x, dashY.sizeDelta.y); //change la taille selon la taille du graph container
            dashY.anchoredPosition = new Vector2(dashY.sizeDelta.x / 2, yMin + (heightInScale * graphHeight));
            allChildsToSuppr.Add(dashY.gameObject);
        }



        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) //boucle pour chaque valeur, cr�er un point, et les relies
        {
            float xPosition = xSize + i * xSize;
            float yPosition = yMin + (valueList[i]/ yMaximum) * graphHeight;
            //on r�cup�re les donn�es de type taille, position et tt du points pour les modifier
            
            //creation du la valeur associ� au X en abssice
            if ((numberOfDays +i) % 5 == 0)
            {
                ///valeur
                TextMeshProUGUI labelX = Instantiate(labelTemplateX, graphContainer, false);
                labelX.gameObject.SetActive(true);
                labelX.rectTransform.anchoredPosition = new Vector2(xPosition, -20f);
                int monthToDisplay = ((numberOfDays + i) / 5) % 12; //calcul complique, mais donne le mois 
                labelX.GetComponent<TextMeshProUGUI>().text = monthList[monthToDisplay];
                allChildsToSuppr.Add(labelX.gameObject);


                //trait
                RectTransform dashX = Instantiate(dashTemplateX, graphContainer, false);
                dashX.gameObject.SetActive(true);
                dashX.sizeDelta = new Vector2(graphContainer.sizeDelta.y, dashX.sizeDelta.y); //change la taille selon la taille du graph container
                dashX.anchoredPosition = new Vector2(xPosition, (dashX.sizeDelta.x / 2));
                allChildsToSuppr.Add(dashX.gameObject);
            }

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if(lastCircleGameObject != null) //si pas le premier point
            {
                //cr�er une ligne entre les deux points
                CreateDotConnection(circleGameObject.GetComponent<RectTransform>().anchoredPosition, lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject; //enregistre le point, pour permettre la connexion entre les points 
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) //cr�er une ligne entre les deux points donn�s
    {
        //creation d'une ligne
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        allChildsToSuppr.Add(gameObject);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>(); 
        Vector2 dir = (dotPositionA - dotPositionB).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        float degres = AngleBetweenVector2(dotPositionB, dotPositionA); //calcul des degr� entre les deux points

        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionB + dir*distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0,degres);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2) //renvoie l'angle entre les deux points donn�s
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private void clearGraph()
    {
        for (int i = 0; i < allChildsToSuppr.Count; i++)
        {
            Destroy(allChildsToSuppr[i]);
        }
        allChildsToSuppr.Clear();

    }

}
