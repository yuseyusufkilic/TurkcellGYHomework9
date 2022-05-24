using Microsoft.EntityFrameworkCore;
using RetroShirtDAL.Data;
using RetroShirtEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroShirtDAL.Repositories
{
    public class EFCategoryTeamRepository : ICategoryTeamRepository
    {
        private RetroShirtDBContext dbContext;

        public EFCategoryTeamRepository(RetroShirtDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task addMany2Many(Product product)
        {
            CategoryTeam catTeam = new CategoryTeam { CategoryId = product.CategoryId, TeamId = (int)product.TeamId };
            // categoryTeam.Team.TeamId = product.Team.TeamId;          
            //categoryTeam.Category.CategoryId = product.Category.CategoryId;
            await dbContext.CategoryTeams.AddAsync(catTeam);
            await dbContext.SaveChangesAsync();
        }
    }
}
