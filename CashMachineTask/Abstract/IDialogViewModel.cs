using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachineTask.Abstract
{
    internal interface IDialogViewModel
    {
        void OnDialogOpened(IDialogParametrs parametrs);

        object Result { get; }
    }
}
