using System.Collections.Generic;

public static partial class Game
{
    public static class Constants
    {

        #region Player Preferences Keys

        public static IReadOnlyDictionary<Preference, string> PreferencesKeys =>
            new Dictionary<Preference, string>()
            {
                { Preference.Difficulty, "DifficultyLevel" },
                { Preference.Character, "CharacterName" },
            };

        #endregion

        #region Tags

        public static IReadOnlyDictionary<Tag, string> GameObjectsTags =>
            new Dictionary<Tag, string>()
            {
                { Tag.Player, "Player" },
                { Tag.Enemy, "Enemy" },
                { Tag.Plataform, "Plataform" },
                { Tag.Sword, "Sword" },
                { Tag.Shield, "Shield" },
                { Tag.Throwable, "Throwable" },
            };

        #endregion

        #region Layers

        public static IReadOnlyDictionary<Layer, int> Layers =
            new Dictionary<Layer, int>()
            {
                { Layer.Player, 8 },
                { Layer.Enemy, 9 },
                { Layer.PlayerThrowable, 10 },
                { Layer.EnemyThrowable, 11 },
            };

        #endregion

    }
}
