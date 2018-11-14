using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameActions
{
    public interface IGameActionView
    {
        void Init();
        void Update(float dt);
        void Done();
    }
}
