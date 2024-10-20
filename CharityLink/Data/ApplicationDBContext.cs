using Microsoft.EntityFrameworkCore;

namespace CharityLink.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 

        }
        

        
    }
}
