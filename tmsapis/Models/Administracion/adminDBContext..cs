using Microsoft.EntityFrameworkCore;

namespace Commons.Models.Administracion
{
    public class adminDBContext: DbContext
    {
        public adminDBContext()
        {
        }

        public adminDBContext(DbContextOptions<adminDBContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseNpgsql("Host=vpn.exikhan.com.mx:5432; Database=Administracion; Username=keycloak; Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
