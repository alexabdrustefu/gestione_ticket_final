﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gestione_ticket_final.Models;

namespace gestione_ticket.Data
{
    public class gestione_ticket_finalContext : DbContext
    {
        public gestione_ticket_finalContext(DbContextOptions<gestione_ticket_finalContext> options)
            : base(options)
        {
        }

        public DbSet<gestione_ticket_final.Models.User> Users { get; set; } = default!;
        public DbSet<gestione_ticket_final.Models.Prodotto> Prodotto { get; set; } = default!;
        public DbSet<gestione_ticket_final.Models.Ticket> Ticket { get; set; } = default!;
        public DbSet<gestione_ticket_final.Models.LavorazioneTicket> LavorazioneTicket { get; set; } = default!;
        public DbSet<gestione_ticket_final.Models.TipologiaProdotto> TipologiaProdotto { get; set; } = default!;
    }
}
