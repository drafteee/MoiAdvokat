using LawyerService.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LawyerService.BL.Entity
{
    public static class EntityManager
    {
        public static IEnumerable<string> GetDeclaredPropertiesForType(this LawyerDbContext _db, Type entityType)
        {
            var navigations = _db.Model.FindEntityType(entityType)
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct()
                    .Select(n => n.Name);

            return navigations;
        }
    }
}
