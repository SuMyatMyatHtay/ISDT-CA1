using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad { 
public class Keypad : MonoBehaviour
{
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (9 Numbers Max)")]
        public int keypadCombo = 123;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted";
        [SerializeField] private string accessDeniedText = "Denied";

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0,5)]
        [SerializeField] private float screenIntensity = 2.5f;
        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new (0.98f, 0.50f, 0.032f, 1f); //orangy
        [SerializeField] private Color screenDeniedColor = new(1f, 0f, 0f, 1f); //red
        [SerializeField] private Color screenGrantedColor = new(0f, 0.62f, 0.07f); //greenish
        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;


        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;

        [Header("Extra GameObjects for the connection. ><")]
        public GameObject theDoorGO; 
        public Animator _aniDoor;
        public GameObject JolleenReliveGO;
        public GameObject EntryCheckTriggerGO;
        public GameObject EntryCheckTriggerGO2;

        private void Awake()
        {
            ClearInput();
            GenerateRandomPassword();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
            EntryCheckTriggerGO.SetActive(false);
            EntryCheckTriggerGO2.SetActive(false);
        }

        private void Start()
        {
            _aniDoor = theDoorGO.GetComponent<Animator>();
            
        }

        //Gets value from pressedbutton
        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
            if (displayingResult || accessWasGranted) return;
            switch (input)
            {
                case "enter":
                   CheckCombo();
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 4) // 4 max passcode size 
                    {
                        return;
                    }
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }
        
        }
        public void CheckCombo()
        {
            if(int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo;
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted));
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason..");
            }

        }

        //mainly for animations 
        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted) AccessGranted();
            else AccessDenied();

            yield return new WaitForSeconds(displayResultTime);
            displayingResult = false;
            if (granted) yield break;
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            onAccessDenied?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);
        }

        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            onAccessGranted?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource.PlayOneShot(accessGrantedSfx);
        
                _aniDoor.SetBool("character_nearby", true);
                Debug.Log("aniDoor : " + _aniDoor.GetBool("character_nearby"));
                EntryCheckTriggerGO.SetActive(true);
                EntryCheckTriggerGO2.SetActive(true);

        }

        public void GenerateRandomPassword()
        {
            keypadCombo = UnityEngine.Random.Range(1000, 10000);
            JolleenReliveGO.GetComponent<JollenRelive>().passcode.text = keypadCombo.ToString();
        }
}
}