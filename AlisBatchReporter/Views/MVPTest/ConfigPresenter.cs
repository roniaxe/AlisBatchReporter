using System;
using System.Collections.Generic;
using System.Linq;

namespace AlisBatchReporter.Views.MVPTest
{
    class ConfigPresenter
    {
        private readonly IConfigView _view;
        private readonly EnvRepository _envRepository;

        public ConfigPresenter(IConfigView view, EnvRepository envRepository)
        {
            _view = view;
            _envRepository = envRepository;
            _view.EnvSelected += OnEnvSelected;
            _view.Saved += OnSave;
            var envs = _envRepository.GetAllEnvs();
            _view.LoadEnvs(envs);
            if (envs != null)
            {
                _view.LoadEnv(envs.First());
            }
        }

        private void OnSave()
        {
            
        }

        private void OnEnvSelected()
        {
            if (_view.SelectedEnv != null)
            {
                var name = _view.SelectedEnv.Name;
                var env = _envRepository.GetEnv(name);
                _view.LoadEnv(env);
            }
        }
    }

    internal class EnvRepository
    {
        readonly IList<Env> _envs = new List<Env>()
        {
            new Env
            {
                Name = "Prod",
                HostAddress = "10.134.5.30"
            },
            new Env
            {
                Name = "White SIT",
                HostAddress = "876630-sqldev.fblife.com"
            },
            new Env
            {
                Name = "Rackspace",
                HostAddress = "756027-LSQLDEV1.FBLIFE.COM"
            },new Env
            {
                Name = "Sapiens",
                HostAddress = "alis-db-sql3"
            }
        };

        public IList<Env> GetAllEnvs() => _envs;

        public Env GetEnv(string name)
        {
            foreach (var env in _envs)
            {
                if (env.Name.Equals(name))
                {
                    return env;
                }
            }
            return null;
        }
    }
}
