using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
using UnityEngine.SceneManagement; 

public class Globe  
{  
	public static string nextSceneName;  
} 


public class LoadScene : MonoBehaviour {

    public Slider processBar;
    public Text processNum;

    private AsyncOperation async;

    private int nowProcess;

    void Start()
    {
        processNum.text = "0%";
        StartCoroutine(loadScene());
    }
    /// <summary>  
    /// 加载完场景后就会跳转  
    /// </summary>  
    /// <returns></returns>  
    IEnumerator loadScene()
    {
        async = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        async.allowSceneActivation = false;
        yield return async;
    }

    void Update()
    {
        if (async == null)
        {
            return;
        }

        int toProcess;
        // async.progress 你正在读取的场景的进度值  0---0.9    
        // 如果当前的进度小于0.9，说明它还没有加载完成，就说明进度条还需要移动    
        // 如果，场景的数据加载完毕，async.progress 的值就会等于0.9  
        if (async.progress < 0.9f)
        {
            toProcess = (int)async.progress * 100;
        }
        else
        {
            toProcess = 100;
        }

        // 如果滑动条的当前进度，小于，当前加载场景的方法返回的进度   
        if (nowProcess < toProcess)
        {
            nowProcess++;
        }
        processNum.text = nowProcess + "%";
        processBar.value = nowProcess / 100f;
        // 设置为true的时候，如果场景数据加载完毕，就可以自动跳转场景   
        if (nowProcess == 100)
        {
            async.allowSceneActivation = true;
        }
    }
}
