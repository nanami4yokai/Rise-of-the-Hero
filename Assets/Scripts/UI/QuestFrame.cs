using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestFrame : MonoBehaviour
{
    public GameObject mainQuestTitle;
    public GameObject questList;

    private CanvasGroup mainQuestTitleCanvasGroup;
    private CanvasGroup questListCanvasGroup;

    private void Start()
    {
        mainQuestTitleCanvasGroup = mainQuestTitle.GetComponent<CanvasGroup>();
        questListCanvasGroup = questList.GetComponent<CanvasGroup>();

        mainQuestTitleCanvasGroup.alpha = 0f;
        questListCanvasGroup.alpha = 0f;

        StartCoroutine(ShowMainQuestTitle());
    }

    private IEnumerator ShowMainQuestTitle()
    {
        yield return StartCoroutine(FadeIn(mainQuestTitleCanvasGroup, 2f));
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(FadeOut(mainQuestTitleCanvasGroup, 2f));
        yield return StartCoroutine(FadeIn(questListCanvasGroup, 2f));
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            canvasGroup.alpha = (Time.time - startTime) / duration;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            canvasGroup.alpha = 1f - (Time.time - startTime) / duration;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
