namespace Identity.DAL
{
    public class DALConnection
    {
        protected static string strConnect;
        static DALConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            strConnect = connectionString;// ConfigurationSettings.AppSettings["SqlConnect"];
        }
    }
}
