﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachineTask.Abstract
{
    interface IDialogService
    {
        void ShowDialog<TVIewModel>(IDialogParametrs dialogParametrs, Action<bool?, object> callback);
    }
}