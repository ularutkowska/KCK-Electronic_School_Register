using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        string Surname { get; }

        string GetDisplayName();
        int GetId();
    }
}

