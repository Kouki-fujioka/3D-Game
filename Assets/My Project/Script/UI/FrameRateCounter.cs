using UnityEngine;
using TMPro;

namespace Unity.Game.UI
{
    public class FrameRateCounter : MonoBehaviour
    {
        [Header("�Q��")]
        [SerializeField, Tooltip("FPS �e�L�X�g")] TextMeshProUGUI m_TextMeshProUGUI = default;

        [Header("�f�[�^")]
        [SerializeField, Tooltip("FPS �v������")] float m_PollingTime = 0.5f;

        public bool IsActive => m_TextMeshProUGUI.gameObject.activeSelf;    // �A�N�e�B�u��Ԃ��i�[
        float m_Time;   // �o�ߎ���
        int m_FrameCount;   // �t���[����

        public void Show(bool show)
        {
            m_TextMeshProUGUI.gameObject.SetActive(show);   // �A�N�e�B�u��ԕύX
        }

        void Update()
        {
            m_Time += Time.deltaTime;
            m_FrameCount++;

            if(m_Time >= m_PollingTime) // FPS �v�����Ԍo�ߌ�
            {
                // �e�L�X�g (FPS) �ݒ�
                int FPS = Mathf.RoundToInt(m_FrameCount / m_Time);
                m_TextMeshProUGUI.text = "FPS : " + FPS.ToString();

                // ���Z�b�g
                m_Time -= m_PollingTime;
                m_FrameCount = 0;
            }
        }
    }
}
