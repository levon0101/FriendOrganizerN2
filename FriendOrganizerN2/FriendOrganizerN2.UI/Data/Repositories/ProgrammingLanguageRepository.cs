﻿using FirendOrganizerN2.DataAcces;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, FriendOrganizerDbContext>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(FriendOrganizerDbContext context) : base(context)
        {
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {

            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);

        }
    }
}
