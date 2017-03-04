using System.Windows;
using InterManage.Repository.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InterManage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            using (var db = new InterManageDbContext())
            {
                db.Database.Migrate();
                db.Database.EnsureCreated();
            }
        }
    }
}
