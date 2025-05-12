using UnityEngine;
using UnityEngine.UI;

namespace Unity.Game.UI
{
    [RequireComponent(typeof(RectTransform))]

    public class Objective : MonoBehaviour
    {
        [Header("�Q��")]
        [SerializeField, Tooltip("���� (����, �s�k) �^�C�g��")] TMPro.TextMeshPro m_Title = default;
        [SerializeField, Tooltip("���� (����, �s�k) ����")] TMPro.TextMeshPro m_Description = default;
        [SerializeField, Tooltip("���� (����, �s�k) �i��")] TMPro.TextMeshPro m_Progress = default;
        [SerializeField, Tooltip("���� (����, �s�k) �B���A�C�R��")] Image m_CompleteIcon = default;
        [SerializeField, Tooltip("�A�j���[�V�����J�[�u")] AnimationCurve m_MoveCurve = default;
        float m_Time;
        const int s_Margin = 25;
        const int s_Space = 4;
        RectTransform m_RectTransform;

        public void Initialize(string title, string description, string progress)
        {
            m_RectTransform = GetComponent<RectTransform>();

            // �e�L�X�g (�i��) ��ݒ�
            m_Progress.text = progress;
            m_Progress.ForceMeshUpdate();

            // �e�L�X�g (�^�C�g��) ��ݒ�
            Vector4 margin = m_Title.margin;
            margin.z = 4 + (string.IsNullOrEmpty(progress) ? 0 : m_Progress.renderedWidth + s_Space);
            m_Title.margin = margin;
            m_Title.text = title;

            // �e�L�X�g (����) ��ݒ�
            m_Description.text = description;
        }

        public void OnProgress(IObjective objective)
        {
            m_Progress.text = objective.GetProgress();

            if (objective.IsCompleted)  // ���� (����, �s�k) �B����
            {
                m_CompleteIcon.gameObject.SetActive(true);  // �A�C�R���\��
                objective.OnProgress -= OnProgress; // ���X�i����
            }
        }

        void Update()
        {
            m_Time += Time.deltaTime;
            var moving = m_MoveCurve.Evaluate(m_Time);
            m_RectTransform.anchoredPosition = new Vector2((m_RectTransform.sizeDelta.x + s_Margin) * moving, m_RectTransform.anchoredPosition.y);  // �X���C�h�C��
        }
    }
}
