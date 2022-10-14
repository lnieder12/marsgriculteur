using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    abstract public class BasicItem
    {
        public int id;
        private string name;
        private string description;
        private string imageLink;

        public string getImageLink()
        {
            return imageLink;
        }

        public string getDescription()
        {
            return description;
        }

        public string getName()
        {
            return name;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}