using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SplashLogo : MonoBehaviour
{
    [SerializeField] private TweenSettings _scaleSetting;

    // Start is called before the first frame update
    void Start()
    {
        ScaleLogo();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ScaleLogo()
    {
        Tween.Scale(transform, 0.00001f, 0.002f, _scaleSetting);

    }
}
