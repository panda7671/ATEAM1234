using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string Scene_A; // Inspector 창에서 입력할 씬 이름

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"충돌 감지됨: {other.name}"); // 충돌이 감지되는지 확인

        if (other.CompareTag("MainCamera"))  // MainCamera로 설정된 카메라만 감지
        {
            Debug.Log("카메라와 충돌 확인. 씬을 변경합니다.");
            LoadNextScene();
        }
        else
        {
            Debug.Log("충돌한 오브젝트의 태그가 MainCamera가 아님.");
        }
    }

    private void LoadNextScene()
    {
        Debug.Log($"씬을 로드합니다: {Scene_A}");
        SceneManager.LoadScene(Scene_A);  // Inspector에서 입력한 씬 이름으로 이동
    }
}
