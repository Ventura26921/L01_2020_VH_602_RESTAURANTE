﻿using Microsoft.EntityFrameworkCore;

namespace L01_2020_VH_602.Models
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options): base(options)
        { }

        public DbSet<pedidos> pedidos { get; set; }
        public DbSet<platos> platos { get; set; }
        public DbSet<clientes> clientes { get; set; }
    }
}
