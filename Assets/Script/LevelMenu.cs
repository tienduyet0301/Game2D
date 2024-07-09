using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    private void Awake()
    {
        ButtonstoArray();
        int unlockedLevel1 = PlayerPrefs.GetInt("UnlockedLevel1" ,1);
        for (int i = 0; i < buttons.Length; i++) //Vòng lặp đầu tiên đặt tất cả các button không hoạt động
        {
            buttons[i].interactable = false;
        }
        for (int i = 0;  i < unlockedLevel1 ; i++){ //Vòng lặp thứ hai kích hoạt các button tương ứng với số level đã mở.
            buttons[i].interactable = true ;
        }
    }


    public void OpenLevel(int levelId) 
    {
        string leveName = "lv" + levelId; 
        SceneManager.LoadScene(leveName);
    }

    void ButtonstoArray() //tìm tất cả các button con trong levelButtons và lưu chúng vào mảng buttons
    {
        int childCount = levelButtons.transform.childCount; // tìm con của lvbutton 
        buttons = new Button[childCount];
        for(int i = 0; i < childCount; i++) // lặp qua tất cả con của lvbutton
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>(); // lưu vào bảng
        }
    }
}
