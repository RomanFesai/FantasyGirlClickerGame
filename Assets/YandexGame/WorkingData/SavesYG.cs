
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;


        // Ваши сохранения
        public int currentPlayerLvl;
        public float currentfillAmount;
        public float currentPointsDivider;
        public bool[] isUnlocked = new bool[8];
        public bool[] isCompleted = new bool[8];
        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            isUnlocked[0] = true;
            isCompleted[0] = false;
        }
    }
}
