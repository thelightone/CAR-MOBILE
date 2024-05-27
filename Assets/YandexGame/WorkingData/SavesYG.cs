
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

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        public int HighScore = 0;
        public bool car1 = true;
        public bool car2 = false;
        public bool car3 = false;
        public bool car4 = false;

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }

        public static SavesYG operator +(SavesYG savesYG, SaveData saveData)
        {
            savesYG.HighScore = saveData.HighScore;
            savesYG.car1 = saveData.car1;
            savesYG.car2 = saveData.car2;
            savesYG.car3 = saveData.car3;
            savesYG.car4= saveData.car4;

            return savesYG;
        }
    }
}
