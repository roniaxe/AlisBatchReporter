namespace AlisBatchReporter.Classes
{
    public class ParamObject
    {
        public ParamObject(
            string param1 = "", 
            string param2 = "", 
            string param3 = "", 
            string param4 = "", 
            string param5 = "")
        {
            Param1 = param1;
            Param2 = param2;
            Param3 = param3;
            Param4 = param4;
            Param5 = param5;
        }

        public string Param1 { get; }

        public string Param2 { get; }

        public string Param3 { get; }

        public string Param4 { get; }

        public string Param5 { get; }
    }
}