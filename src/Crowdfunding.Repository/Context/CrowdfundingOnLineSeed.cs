using System;
using System.Collections.Generic;
using System.Linq;

using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Context
{
    public class CrowdfundingOnLineSeed
    {
        public static void Seed(CrowdfundingOnLineDBContext crowdfundingOnLineDBContext)
        {
            if (!crowdfundingOnLineDBContext.Causes.Any())
            {
                crowdfundingOnLineDBContext.AddRange(new List<Cause> {
                    new Cause(Guid.NewGuid(), "Santa Casa", "Araraquara", "SP"),
                    new Cause(Guid.NewGuid(), "Amigos do bem", "Araraquara", "SP"),
                    new Cause(Guid.NewGuid(), "A passos", "São Carlos", "SP")
                });
                crowdfundingOnLineDBContext.SaveChanges();
            }
        }
    }
}