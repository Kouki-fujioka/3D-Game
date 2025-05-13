using UnityEngine;

namespace Unity.Game.UI
{
    public class GameOverFade : MonoBehaviour
    {
        [Header("�Q��")]
        [SerializeField, Tooltip("�L�����o�X�O���[�v (�Q�[���I����ɉ�ʂ��t�F�[�h�A�E�g)")] CanvasGroup m_CanvasGroup = default;

        [Header("�f�[�^")]
        [SerializeField, Tooltip("�������Ă���t�F�[�h�A�E�g�J�n�܂ł̎���")] float m_WinDelay = 4.0f;
        [SerializeField, Tooltip("�s�k���Ă���t�F�[�h�A�E�g�J�n�܂ł̎���")] float m_LoseDelay = 2.0f;
        [SerializeField, Tooltip("�t�F�[�h�A�E�g�����܂ł̎���")] float m_Duration = 1.0f;

        float m_Time;
        bool m_GameOver;
        bool m_Won;

        void Start()
        {
            EventManager.AddListener<GameOverEvent>(OnGameOver);
        }

        void Update()
        {
            if (m_GameOver)
            {
                m_Time += Time.deltaTime;

                if (m_Won)
                {
                    m_CanvasGroup.alpha = Mathf.Clamp01((m_Time - m_WinDelay) / m_Duration);
                }
                else
                {
                    m_CanvasGroup.alpha = Mathf.Clamp01((m_Time - m_LoseDelay) / m_Duration);
                }
            }
        }

        void OnGameOver(GameOverEvent evt)
        {
            if (!m_GameOver)
            {
                m_CanvasGroup.gameObject.SetActive(true);
                m_GameOver = true;
                m_Won = evt.Win;
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<GameOverEvent>(OnGameOver); // ���X�i����
        }
    }
}
