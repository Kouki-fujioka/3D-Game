namespace Unity.Game
{
    public class Events
    {
        public static OptionsMenuEvent OptionsMenuEvent = new OptionsMenuEvent();
        public static GameOverEvent GameOverEvent = new GameOverEvent();
        public static LookSensitivityUpdateEvent LookSensitivityUpdateEvent = new LookSensitivityUpdateEvent();
    }

    public class OptionsMenuEvent : GameEvent
    {
        public bool Active;
    }

    public class GameOverEvent : GameEvent
    {
        public bool Win;
    }

    public class LookSensitivityUpdateEvent : GameEvent
    {
        public float Value;
    }
}
