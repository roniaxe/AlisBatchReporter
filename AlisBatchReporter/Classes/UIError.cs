using System;
using System.Windows.Forms;

namespace AlisBatchReporter.Classes
{
    class UiError
    {
        public UiError(Exception ex, string path_)
        {
            Msg = ex.Message; Path = path_; Result = DialogResult.Cancel;
        }
        public string Msg;
        public string Path;
        public DialogResult Result;
    }
}
