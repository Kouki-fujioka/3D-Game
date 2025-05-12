using UnityEngine;
using UnityEngine.UI;

namespace Unity.Game.UI
{
    public class ObjectiveHUDManager : MonoBehaviour
    {
        [Header("�Q��")]
        [SerializeField, Tooltip("���� (����, �s�k) �\���p�p�l��")] RectTransform m_ObjectivePanel = default;
        [SerializeField, Tooltip("��������")] GameObject m_WinObjectivePrefab = default;
        [SerializeField, Tooltip("�s�k����")] GameObject m_LoseObjectivePrefab = default;

        const int s_TopMargin = 10;
        const int s_Space = 10;
        float m_NextY;

        void Awake()
        {
            EventManager.AddListener<ObjectiveAdded>(OnObjectiveAdded); // ���X�i�ǉ�
            EventManager.AddListener<GameOverEvent>(OnGameOver);    // ���X�i�ǉ�
        }

        void OnObjectiveAdded(ObjectiveAdded evt)   // �R�[���o�b�N���\�b�h
        {
            if(!evt.Objective.m_Hidden) // �\����
            {
                GameObject go = Instantiate(evt.Objective.m_Lose ? m_LoseObjectivePrefab : m_WinObjectivePrefab, m_ObjectivePanel); // �N���[�� (�s�k���� or ��������)
                Objective objective = go.GetComponent<Objective>();
                objective.Initialize(evt.Objective.m_Title, evt.Objective.m_Description, evt.Objective.GetProgress());
                LayoutRebuilder.ForceRebuildLayoutImmediate(objective.GetComponent<RectTransform>());
                RectTransform rectTransform = go.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, m_NextY - s_TopMargin);
                m_NextY -= rectTransform.sizeDelta.y + s_Space;
                evt.Objective.OnProgress += objective.OnProgress;
            }
        }

        void OnGameOver(GameOverEvent evt)  // �R�[���o�b�N���\�b�h
        {
            EventManager.RemoveListener<ObjectiveAdded>(OnObjectiveAdded);  // ���X�i����
            EventManager.RemoveListener<GameOverEvent>(OnGameOver); // ���X�i����
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ObjectiveAdded>(OnObjectiveAdded);  // ���X�i����
            EventManager.RemoveListener<GameOverEvent>(OnGameOver); // ���X�i����
        }
    }
}
