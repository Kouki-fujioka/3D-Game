using System;
using System.Collections.Generic;

namespace Unity.Game
{
    public class GameEvent { }
    public static class EventManager
    {
        static readonly Dictionary<Type, Action<GameEvent>> s_Events = new Dictionary<Type, Action<GameEvent>>();
        static readonly Dictionary<Delegate, Action<GameEvent>> s_EventLookups = new Dictionary<Delegate, Action<GameEvent>>();

        /// <summary>
        /// T �C�x���g�u���[�h�L���X�g���Ɏ��s���郁�\�b�h��o�^
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evt"></param>
        public static void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                Action<GameEvent> newAction = e => evt((T)e);
                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                {
                    s_Events[typeof(T)] = internalAction += newAction;
                }
                else
                {
                    s_Events[typeof(T)] = newAction;
                }
            }
        }

        /// <summary>
        /// T �C�x���g�u���[�h�L���X�g���Ɏ��s���郁�\�b�h���폜
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evt"></param>
        public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
        {
            if (s_EventLookups.TryGetValue(evt, out var action))
            {
                if (s_Events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;

                    if (tempAction == null)
                    {
                        s_Events.Remove(typeof(T));
                    }
                    else
                    {
                        s_Events[typeof(T)] = tempAction;
                    }
                }

                s_EventLookups.Remove(evt);
            }
        }

        /// <summary>
        /// �o�^�ς݂̃��\�b�h�����s
        /// </summary>
        /// <param name="evt"></param>
        public static void Broadcast(GameEvent evt)
        {
            if (s_Events.TryGetValue(evt.GetType(), out var action))
            {
                action.Invoke(evt);
            }
        }
    }
}
