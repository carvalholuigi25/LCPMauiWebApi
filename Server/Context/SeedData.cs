using LCPMauiWebApi.Server.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCPMauiWebApi.Server.Context
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder mb)
        {
            mb.Entity<MyUsers>().HasData(new List<MyUsers>() { });
            mb.Entity<MyUsersAuth>().HasData(new List<MyUsersAuth>() { });
            mb.Entity<Posts>().HasData(new List<Posts>() { });
            mb.Entity<Comments>().HasData(new List<Comments>() { });
            mb.Entity<Replies>().HasData(new List<Replies>() { });
            mb.Entity<Attachments>().HasData(new List<Attachments>() { });
            mb.Entity<Reactions>().HasData(new List<Reactions>() { });
            mb.Entity<Feedback>().HasData(new List<Feedback>() { });
            mb.Entity<Todo>().HasData(new List<Todo>() { });
        }
    }
}
