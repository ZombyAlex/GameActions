using System;
using System.Collections.Generic;
using System.Linq;

namespace GameActions
{
   

    public class GameAction
    {
        private bool isInit = false;
        private bool isDone = false;
        private List<GameAction> actions;
        private readonly IGameActionView view;

        public GameAction(IGameActionView view)
        {
            actions = new List<GameAction>();
            this.view = view;
        }

        private void Init()
        {
            
            OnInit();
            view.Init();
        }

        protected virtual void OnInit()
        {
        }

        public void Update(float dt)
        {
            if (!isInit)
            {
                isInit = true;
                Init();
            }

            foreach (GameAction action in actions)
            {
                action.Update(dt);
                if (action.IsBlock())
                    break;
            }

            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].IsDone())
                {
                    actions.RemoveAt(i);
                    i--;
                }
            }

            PrevUpdate(dt);
            OnUpdate(dt);
            view.Update(dt);
        }

        protected virtual void PrevUpdate(float dt)
        {
        }

        protected virtual void OnUpdate(float dt)
        {
        }

        public void Add(GameAction action)
        {
            actions.Add(action);
        }

        public bool IsDone()
        {
            return isDone && IsEmpty();
        }

        public virtual bool IsBlock()
        {
            return false;
        }

        protected void Done()
        {
            isDone = true;
            view.Done();
        }

        protected bool IsEmpty()
        {
            return actions.Count == 0;
        }
    }

    public class GameActionTime : GameAction
    {
        private float time;

        public GameActionTime(IGameActionView view, float time): base(view)
        {
            this.time = time;
        }

        protected override void PrevUpdate(float dt)
        {
            time -= dt;
            if (time <= 0)
                Done();
        }

        public override bool IsBlock()
        {
            return time > 0;
        }
    }

    public class GameActionBlock : GameAction
    {
        public GameActionBlock(IGameActionView view): base(view)
        {
        }

        public override bool IsBlock()
        {
            return true;
        }
    }
}
