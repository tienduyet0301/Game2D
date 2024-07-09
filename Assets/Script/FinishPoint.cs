
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //chỉ thực hiện nếu đối tượng chạm có tag là player
        {
            UnlockNewLevel(); // gọi phương thức để mở khóa lv mới
            SceneController.instance.NextLevel(); // Gọi phương thức NextLevel từ SceneController để chuyển sang cấp độ tiếp theo.
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex")) ; // Kiểm tra xem cấp độ hiện tại có lớn hơn hoặc bằng cấp độ mà người chơi đã đạt được hay không.
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex +1);
            PlayerPrefs.SetInt("UnlockedLevel1", PlayerPrefs.GetInt("UnlockedLevel1", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
