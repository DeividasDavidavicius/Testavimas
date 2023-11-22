using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Commands
{
    internal class DenyCommand : ICommand
    {
        public bool Execute()
        {
            return false;
        }

        public bool AbleToExecute()
        {
            return false;
        }

        public string CommandName()
        {
            return "Deny move";
        }

        public bool Undo()
        {
            return false;
        }
    }
}
