using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
	public Image scrollHandle;

	public TextMeshProUGUI loadingText;

	public void TitleSceneLoad()
	{
		StartCoroutine(Co_LoadScene(0));
	}

	public void IntroSceneLoad()
	{
		StartCoroutine(Co_LoadScene(1));
	}

	public void GameSceneLoad()
	{
		StartCoroutine(Co_LoadScene(2));
	}

	public void CaughtSceneLoad()
	{
		StartCoroutine(Co_LoadScene(3));
	}

	public void TimeEndingScene()
	{
		StartCoroutine(Co_LoadScene(4));
	}

	public void EndingSceneLoad()
	{
		StartCoroutine(Co_LoadScene(5));
	}

	private IEnumerator Co_LoadScene(int SceneNum)
	{
		AsyncOperation asyncOp = SceneManager.LoadSceneAsync(SceneNum);
		loadingText.DOKill();
		loadingText.text = "";
		loadingText.DOText("Loading...", 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
		float timer = 0f;
		float loadTime = 0f;
		while (!asyncOp.isDone || loadTime < 5f)
		{
			timer += Time.deltaTime;
			loadTime += Time.deltaTime;
			if (asyncOp.progress < 0.9f)
			{
				scrollHandle.fillAmount = Mathf.Lerp(asyncOp.progress, 1f, timer);
				if (scrollHandle.fillAmount >= asyncOp.progress)
				{
					timer = 0f;
				}
			}
			else
			{
				scrollHandle.fillAmount = Mathf.Lerp(scrollHandle.fillAmount, 1f, timer);
				if (scrollHandle.fillAmount >= 0.96f)
				{
					asyncOp.allowSceneActivation = asyncOp.progress > 0.96f;
				}
			}
			SceneManager.LoadScene(SceneNum);
			StopCoroutine(Co_LoadScene(SceneNum));
			yield return null;
		}
		base.gameObject.SetActive(value: false);
	}
}
