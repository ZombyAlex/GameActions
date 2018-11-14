using System;

namespace GameActions
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionEmpty action = new ActionEmpty(new ViewAction("main action start", "main action end"));

            action.Add(new GameActionTime(new ViewTime(), 3));
            action.Add(new Move(new ViewAction("start move", "end move")));
            action.Add(new Move(new ViewAction("start move", "end move")));
            action.Add(new GameActionTime(new ViewTime(), 5));


            GameActionManager.Instance.Run(action);

            Console.WriteLine("done...");
            Console.Read();

        }
    }

    public class ViewTime : IGameActionView
    {
        public void Init()
        {
            Console.WriteLine("start time...");
        }

        public void Update(float dt)
        {
            Console.WriteLine("delta time " + dt);
        }

        public void Done()
        {
            Console.WriteLine("time done");
        }
    }

    public class ViewAction : IGameActionView
    {
        private string textStart;
        private string textEnd;

        public ViewAction(string textStart, string textEnd)
        {
            this.textStart = textStart;
            this.textEnd = textEnd;
        }

        public void Init()
        {
            Console.WriteLine(textStart);
        }

        public void Update(float dt)
        {
            
        }

        public void Done()
        {
            Console.WriteLine(textEnd);
        }
    }

    public class Move : GameActionBlock
    {
        public Move(IGameActionView view) : base(view)
        {
        }

        protected override void OnUpdate(float dt)
        {
            Random random = new Random();
            if(random.Next(0, 5) == 3)
                Done();
        }
    }

    public class ActionEmpty : GameAction
    {
        public ActionEmpty(IGameActionView view) : base(view)
        {
        }

        protected override void OnUpdate(float dt)
        {
            if(IsEmpty())
                Done();
        }
    }
}
