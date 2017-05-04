﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public interface IFormDataPager
    {
        void OnBindData(ref SerachData SerachEntity, bool Append);
        SerachData SerachEntity { get; set; }
        SerachData TempSerachData { get; set; }
    }
}
