
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2020_VH_602.Models
{
    public class Rest: DbContext
    {
        public Rest(DbContextOptions<Rest>options): base(options)
        {

        }
    }
}
