using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween; 

public class JollenRelive : MonoBehaviour
{
    #region Check sections 
    [Header("Check Sections")]
    public GameObject firstCheck;
    public GameObject secondCheck;
    public GameObject thirdPartPw;

    public GameObject fixedCells;
    public GameObject nonFixedCells1;
    public GameObject nonFixedCells2;
    public GameObject nonFixedCells3;
    #endregion

    #region First Check Related 
    [Header("First Part")]
    public AudioClip audioClip; 
    public Button button;
    public AudioSource audioSource;
    [Header("Toggles [First Part]")]
    public Toggle topToggle; 
    public Toggle leftToggle;
    public Toggle rightToggle;
    #endregion

    #region Second part Loading Bar 
    [Header("Second Part Loading Bar")]
    public GameObject bar;
    public int time;
    public TextMeshProUGUI percentageText;
    public TextMeshProUGUI processText;
    public TextMeshProUGUI passcode;
    #endregion

    #region Ani and Target GO 
    [Header("Animator and Target GO")]
    private Animator _ani;
    public GameObject targetGameObject;
    private Animator _aniC;
    public GameObject targetGameObjectC;
    private Animator _aniJolleen; 
    public GameObject targetGameObjectJolleen;
    #endregion

    #region Heat Check Pad 
    [Header("Heat Check Pad")]
    public GameObject heatPadSocket;
    [SerializeField] private CanvasGroup glassLikePanel1;
    [SerializeField] private CanvasGroup glassLikePanel2;
    [SerializeField] private CanvasGroup glassLikePanel3;
    [SerializeField] private CanvasGroup glassLikePanel4;
    [SerializeField] private CanvasGroup glassLikePanel5;
    [SerializeField] private CanvasGroup glassLikePanel6;
     
    #endregion

    void Start()
    {
        firstCheck.SetActive(true); 
        secondCheck.SetActive(false);
        thirdPartPw.SetActive(false);

        fixedCells.SetActive(false);
        nonFixedCells1.SetActive(true);
        nonFixedCells2.SetActive(true);
        nonFixedCells3.SetActive(true);
        _ani = targetGameObject.GetComponent<Animator>();
        _aniC = targetGameObjectC.GetComponent<Animator>();
        _aniJolleen = targetGameObjectJolleen.GetComponent<Animator>();

        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource
        audioSource.clip = audioClip;
    }

    public void CheckToggle()
    {
        bool isOnTop = topToggle.isOn;
        bool isOnLeft = leftToggle.isOn;
        bool isOnRight = rightToggle.isOn;

        Debug.Log(isOnRight + " " + isOnLeft + " " + isOnTop); 
        if (isOnTop && isOnLeft && isOnRight)
        {
            
            TransitionBetween(); 
        }
        else
        {
            PlayAudio(); 
        }
    }
    public void PlayAudio()
    {
        Debug.Log("Button is clicked! Play Audio");
        audioSource.Play();
    }
    public void TransitionBetween()
    {
        Debug.Log("YAYYYY!");
        firstCheck.SetActive(false);
        secondCheck.SetActive(true); 
        nonFixedCells1.SetActive(false);
        nonFixedCells2.SetActive(false);
        nonFixedCells3.SetActive(false);
        fixedCells.SetActive(true);
        processText.text = "Processing...";
        _ani.SetTrigger("Close");
        _aniC.SetTrigger("Close");
        AnimateBar();
    }

    public void AnimateBar()
    {
        float percentage = 0f;
        Tween.ScaleX(bar.transform, 1f, time, Ease.OutQuad)
            .OnUpdate(target: bar, (target, tween) =>
            {
                percentage = tween.progress;
                percentageText.text = Mathf.RoundToInt(percentage * 100) + "%";
            })
            .OnComplete(() =>
            {
                // Handle completion logic here
                processText.text = "Completed!";
                _aniJolleen.SetTrigger("Jump");
                AnimateKeyCode(); 
            });

    }

    public void AnimateKeyCode()
    {
        Tween.Delay(duration: 5f, () =>
        {
            Tween.Alpha(processText, 0f, 1f)
                .OnComplete(() => 
                {
                    secondCheck.SetActive(false);

                    passcode.alpha = 0f;
                    thirdPartPw.SetActive(true);
                    Tween.Delay(duration: 5f, () =>
                    {
                        Tween.Alpha(passcode, 1f, 0f); 
                    }); 
                });
        });
    }
    public void FadeOutGlassLikePanel()
    {
        Tween.Alpha(glassLikePanel1, 0f, 1f);
        Tween.Alpha(glassLikePanel2, 0f, 1f);
        Tween.Alpha(glassLikePanel3, 0f, 1f);
        Tween.Alpha(glassLikePanel4, 0f, 1f);
        Tween.Alpha(glassLikePanel5, 0f, 1f);
        Tween.Alpha(glassLikePanel6, 0f, 1f).OnComplete(() => heatPadSocket.GetComponent<CheckHeatPad>().yesCorrectHeatPad());
    }

}
