using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ICommand
    {
        bool Execute();
        bool AbleToExecute();
        string CommandName();
        bool Undo();
    }
}
