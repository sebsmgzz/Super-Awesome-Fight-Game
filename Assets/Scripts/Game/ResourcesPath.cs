using System.Collections.Generic;

public static partial class Game
{
    public static class ResourcesPath
    {

        public static IReadOnlyDictionary<CharacterName, string> Heads => new Dictionary<CharacterName, string>()
        {
            { CharacterName.Sebas, "Heads/Sebastian" },
            { CharacterName.Camacho, "Heads/Camacho" },
            { CharacterName.Chavarria, "Heads/Chavarria" },
            { CharacterName.Ramon, "Heads/Ramon" },
            { CharacterName.Joaquin, "Heads/Joaquin" },
            { CharacterName.Maritza, "Heads/Maritza" },
            { CharacterName.Majo, "Heads/Majo" },
            { CharacterName.Lucia, "Heads/Lucia" }
        };

        public static Dictionary<PoolItem, string> PoolItems =>
            new Dictionary<PoolItem, string>()
            {
                { PoolItem.Bottle, "Throwable/Bottle" },
                { PoolItem.BMW, "Throwable/BMW" },
                { PoolItem.Car, "Throwable/Car" },
                { PoolItem.PlayController, "Throwable/PlayController" },
                { PoolItem.Anime, "Throwable/Anime" },
                { PoolItem.Cat, "Throwable/Cat" },
                { PoolItem.Leaf, "Throwable/Leaf" },
                { PoolItem.Tulip, "Throwable/Tulip" },
            };

        public static IReadOnlyDictionary<AudioClipName, string> AudioClips => new Dictionary<AudioClipName, string>()
        {
            { AudioClipName.Attack, "Audio/Attack" },
            { AudioClipName.Jump, "Audio/Jump" },
            { AudioClipName.Die, "Audio/Die" },
            { AudioClipName.Victory, "Audio/Victory" },
        };

    }
}
