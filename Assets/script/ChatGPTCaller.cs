using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class ChatGPTCaller : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TMP_Text gptOutputText;
    public Button sendButton;

    public string apiKey;

    void Start()
    {
        apiKey = LoadApiKey();
    }

    public void SendRequestToGPT()
    {
        sendButton.interactable = false; // 버튼 잠금
        string prompt = userInputField.text;
        StartCoroutine(SendChatGPTRequest(prompt));
    }

    IEnumerator SendChatGPTRequest(string prompt)
    {
        string url = "https://api.openai.com/v1/chat/completions";
        string requestBody = "{\"model\": \"gpt-4o\", \"messages\": [{\"role\": \"user\", \"content\": \"" + prompt + "\"}]}";

        Debug.Log("🔹 보낼 Prompt: " + prompt);
        Debug.Log("🔹 보내는 JSON: " + requestBody);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        Debug.Log("🔸 응답 상태 코드: " + request.responseCode);
        Debug.Log("🔸 요청 결과 상태: " + request.result);
        Debug.Log("🔸 요청 에러 메시지: " + request.error);

        if (request.result == UnityWebRequest.Result.Success)
        {
            string resultJson = request.downloadHandler.text;
            Debug.Log("✅ GPT 응답 원본 JSON: " + resultJson);

            string content = ParseGPTContent(resultJson);
            Debug.Log("✅ 파싱된 응답: " + content);
            gptOutputText.text = content;
        }
        else
        {
            gptOutputText.text = "GPT 요청 실패: " + request.error;
        }

        sendButton.interactable = true;
    }

    string ParseGPTContent(string json)
    {
        try
        {
            GPTResponse response = JsonUtility.FromJson<GPTResponse>(json);
            return response.choices[0].message.content;
        }
        catch
        {
            return "응답 파싱 실패 (구조 불일치)";
        }
    }

    // 🔑 API 키를 Resources/apiKey.json 에서 불러오는 함수
    [System.Serializable]
    public class APIKeyData
    {
        public string openai_api_key;
    }

    private string LoadApiKey()
    {
        TextAsset apiKeyFile = Resources.Load<TextAsset>("apiKey");
        if (apiKeyFile != null)
        {
            APIKeyData data = JsonUtility.FromJson<APIKeyData>(apiKeyFile.text);
            return data.openai_api_key;
        }
        else
        {
            Debug.LogError("❌ API 키 파일이 없습니다! Assets/Resources/apiKey.json 경로에 파일을 넣어주세요.");
            return "";
        }
    }
}
