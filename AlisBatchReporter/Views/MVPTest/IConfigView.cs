﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlisBatchReporter.Views.MVPTest
{
    interface IConfigView
    {
        event Action EnvSelected;
        event Action DbSelected;
        event Action Saved;
        IList<Env> Envs { get; }
        Env SelectedEnv { get; }
        string Host { get; }
        string Password { get; }
        string UserName { get; }
        bool SaveForLater { get; }
        void LoadEnvs(IList<Env> envList);
        void LoadEnv(Env selectedEnv);
    }
}
