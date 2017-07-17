using System.Collections.Generic;

namespace AlisBatchReporter.Views.MVPTest
{
    public class Config
    {
        public List<Env> Envs { get; set; }
        public string HostAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RemmemberFlag { get; set; }
        public List<string> Dbs { get; set; }
        public override string ToString()
        {
            return $@"{HostAddress},{UserName},{Password},{RemmemberFlag}";
        }
    }
}
