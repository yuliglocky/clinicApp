using clinicApp.Conexion;

namespace clinicApp
{
    public partial class App : Application
    {
        public static string? DatabasePath { get; private set; }
        public static Database? Database { get; private set; }
        public App()
        {
         
            InitializeComponent();
            // Set the database path
            var dbName = "clinica.db3";
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);
            DatabasePath = dbPath;
            Database = new Database(dbPath);

            MainPage = new AppShell();
        }
    }
}
