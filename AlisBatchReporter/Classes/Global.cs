namespace AlisBatchReporter.Classes
{
    static class Global
    {
        public static string Env { get; set; } = "Prod";

        public static string ChosenConnection { get; set; } =
            "Data Source=10.134.5.30;Initial Catalog=alis_db_prod;Persist Security Info=True;User ID=Ebachmeir;Password=9Ke2n!47T";
    }
}
