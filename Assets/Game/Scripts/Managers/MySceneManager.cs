using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class MySceneManager : MonoSingleton<MySceneManager>
{
    //Will change later
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private ProgressBar progressBar;
    public int CurrentLevel => (currentLevelIndex - (int)SceneIndexes.LEVELS) + 1;
    private static int currentLevelIndex = (int)SceneIndexes.LEVELS;
    private List<AsyncOperation> scenesUnLoading = new List<AsyncOperation>();
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    void Start()
    {
        if (SceneManager.sceneCount != System.Enum.GetNames(typeof(SceneIndexes)).Length) FirstLoad();
    }
    [Button]
    public void RestartActiveScene()
    {

        UnloadCurrentLevel();
        var sceneToLoad = LoadLevel(currentLevelIndex);
        ObjectPooler.Instance.SetActiveSpheres();
        StartCoroutine(WaitForAllScenes(sceneToLoad));
    }

    [Button]
    public void LoadNextLevel()
    {
        if (currentLevelIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            RestartActiveScene();
            return;
        }
        UnloadCurrentLevel();
        currentLevelIndex++;
        var sceneToLoad = LoadLevel(currentLevelIndex);
        ObjectPooler.Instance.SetActiveSpheres();
        StartCoroutine(WaitForAllScenes(sceneToLoad));
    }

    private void UnloadCurrentLevel(bool restartEnv = false)
    {
        //RESET UI, GAMEMANAGER ETC
        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentLevelIndex));
      

    }
    private AsyncOperation LoadLevel(int index)
    {
        //SETUP UI, GAMEMANAGER ETC
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        scenesLoading.Add(asyncLoadScene);
        return asyncLoadScene;
    }
    private void FirstLoad()
    {
        for(int i = 1;i < (int)SceneIndexes.LEVELS; i++)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive));
        }
        var gameScene = SceneManager.LoadSceneAsync(currentLevelIndex, LoadSceneMode.Additive);
        //var uiScene = SceneManager.LoadSceneAsync((int)SceneIndexes.UI, LoadSceneMode.Additive);
        //var envScene = SceneManager.LoadSceneAsync((int)SceneIndexes.ENVIRONMENT, LoadSceneMode.Additive);
        scenesLoading.Add(gameScene);
        //scenesLoading.Add(uiScene);
        //scenesLoading.Add(envScene);
        StartCoroutine(WaitForAllScenes(gameScene));
    }

    private IEnumerator WaitForAllScenes(AsyncOperation sceneToSetActive)
    {
        float totalSceneProgress = 0;

        loadingScreen.SetActive(true);
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                progressBar.SetCurrentFill(Mathf.RoundToInt(totalSceneProgress));
                yield return null;
            }
        }
        loadingScreen.SetActive(false);
        Debug.Log("Setting active scene..");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevelIndex));
    }
}