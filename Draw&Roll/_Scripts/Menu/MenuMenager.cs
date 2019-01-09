using UnityEngine;
using System.Collections;


namespace Game.Menu
{
    public class MenuMenager : MonoBehaviour
    {
        public static MenuMenager Instance;
        public Menu[] allPopUpMenuInScene;
        public Menu[] allMenuInScene;

        private Menu currentPopupOn;
        public Menu currentMenu;

        private void Awake() {
            Instance = GetComponent<MenuMenager>();
        }


        public void ShowPopupMenu(int indexMenu)
        {
            currentPopupOn = allPopUpMenuInScene[indexMenu];
            currentPopupOn.IsOpen = true;
        }

        public bool IsPopupOn()
        {
            if (currentPopupOn == null)
                return false;
            else
                return true;
        }


        public void ShowPopupMenu(Menu popUpMenu)
        {
            CloseCurrentPopupMenu();
            currentPopupOn = popUpMenu;
            currentPopupOn.IsOpen = true;
        }

        public void ClosePopupMenu(int indexMenu)
        {
            allPopUpMenuInScene[indexMenu].IsOpen = false;
        }

        public void ClosePopupMenu(Menu popupMenu)
        {
            popupMenu.IsOpen = false;
        }

        public void OnlyChangeMenu(Menu menu) {
            menu.IsOpen = true;
        }

        public void CloseCurrentPopupMenu()
        {
            if (currentPopupOn != null)
            {
                currentPopupOn.IsOpen = false;
                currentPopupOn = null;
            }
        }

        public void ChangeMenu(Menu showThisMenu)
        {
            if (currentMenu != null)
            {
                currentMenu.IsOpen = false;
            }

            currentMenu = showThisMenu;
            currentMenu.IsOpen = true;
        }


    }
}