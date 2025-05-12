using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.Game.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("�Q��")]
        [SerializeField, Tooltip("���j���[���")] GameObject m_Menu = default;
        [SerializeField, Tooltip("����������")] GameObject m_Controls = default;
        [SerializeField, Tooltip("�e�p�g�O��")] Toggle m_ShadowsToggle = default;
        [SerializeField, Tooltip("FPS �p�g�O��")] Toggle m_FrameRateCounterToggle = default;
        [SerializeField, Tooltip("���_�ړ����x�p�X���C�_")] Slider m_Sensitivity = default;

        FrameRateCounter m_FrameRateCounter;

        void Start()
        {
            m_FrameRateCounter = FindFirstObjectByType<FrameRateCounter>();

            if (m_FrameRateCounter == null)
            {
                Debug.LogError("FrameRateCounter is null");
            }

            m_Menu.SetActive(false);
            m_ShadowsToggle.SetIsOnWithoutNotify(QualitySettings.shadows != ShadowQuality.Disable);
            m_ShadowsToggle.onValueChanged.AddListener(OnShadowsChanged);
            m_FrameRateCounterToggle.SetIsOnWithoutNotify(m_FrameRateCounter.IsActive);
            m_FrameRateCounterToggle.onValueChanged.AddListener(OnFrameRateCounterChanged);
            var defaultSensitivity = PlayerPrefs.GetFloat("Sensitivity", 5.0f);
            m_Sensitivity.SetValueWithoutNotify(defaultSensitivity);
            m_Sensitivity.onValueChanged.AddListener(OnSensitivityChanged);
            OnSensitivityChanged(defaultSensitivity);
        }

        void OnShadowsChanged(bool value)   // �R�[���o�b�N���\�b�h
        {
            QualitySettings.shadows = value ? ShadowQuality.All : ShadowQuality.Disable;
        }

        void OnFrameRateCounterChanged(bool value)  // �R�[���o�b�N���\�b�h
        {
            m_FrameRateCounter.Show(value);
        }

        void OnSensitivityChanged(float sensitivity)    // �R�[���o�b�N���\�b�h
        {
            PlayerPrefs.SetFloat("Sensitivity", sensitivity);
            PlayerPrefs.Save();
            LookSensitivityUpdateEvent lookSensitivityUpdateEvent = Events.LookSensitivityUpdateEvent;
            lookSensitivityUpdateEvent.Value = sensitivity;
            EventManager.Broadcast(lookSensitivityUpdateEvent);
        }

        public void CloseMenu() // DoneButton ������
        {
            SetMenuActivation(false);
        }

        public void ToggleMenu()
        {
            SetMenuActivation(!(m_Menu.activeSelf || m_Controls.activeSelf));
        }

        void SetMenuActivation(bool active)
        {
#if !UNITY_EDITOR
            Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
#endif
            m_Menu.SetActive(active);
            m_Controls.SetActive(false);

            if (m_Menu.activeSelf)
            {
                Time.timeScale = 0f;
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Time.timeScale = 1f;
            }

            OptionsMenuEvent evt = Events.OptionsMenuEvent;
            evt.Active = active;
            EventManager.Broadcast(evt);
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire2"))    // InputManager����ݒ�
            {
                ToggleMenu();
            }
        }
    }
}
