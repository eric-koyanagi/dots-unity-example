using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MonoBehaviors
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public GameObject MainMenu;
        public GameObject NewGameMenu;


        void Start()
        {
            instance = this;
        }

        public void CloseAll()
        {
            MainMenu.SetActive(false);
            NewGameMenu.SetActive(false);
        }
    }
}