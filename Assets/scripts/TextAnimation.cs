using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float animationDuration = 2f; // Duração da animação em segundos
    public float displayDuration = 2f;   // Quanto tempo o texto fica visível em segundos

    private void Start()
    {
        // Certifique-se de que o texto não está visível no início.
        displayText.enabled = true;
    }

    public void ActivateAndAnimateText(string text)
    {
        // Iniciar a corrotina para exibir e animar o texto
        StartCoroutine(AnimateText(text));
    }

    private IEnumerator AnimateText(string text)
    {
        // Ativar o texto
        displayText.text = text;
        displayText.enabled = true;

        // Animar o texto subindo
        float startTime = Time.time;
        while (Time.time - startTime < animationDuration)
        {
            float progress = (Time.time - startTime) / animationDuration;
            displayText.rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, Vector2.up * 0.5f, progress);
            yield return null;
        }

        // Aguardar o tempo de exibição
        yield return new WaitForSeconds(displayDuration);

        // Desativar o texto e resetar sua posição
        displayText.enabled = false;
        displayText.rectTransform.anchoredPosition = Vector2.zero;
    }
}
