using System.Diagnostics;

namespace GameActions
{
    
    public class GameActionManager
    {
        private static GameActionManager instance;

        private GameActionManager()
        {
        }

        public static GameActionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameActionManager();
                return instance;
            }
        }
  
        public void Run(GameAction action)
        {
            Stopwatch sw = new Stopwatch();
            float dt = 0;
            sw.Start();
            while (true)
            {
                sw.Stop();
                //action.Update(sw.ElapsedMilliseconds / 1000.0f);
                action.Update(1);
                sw.Restart();
                if (action.IsDone())
                    break;
            }
        }

      
    }

    
}
