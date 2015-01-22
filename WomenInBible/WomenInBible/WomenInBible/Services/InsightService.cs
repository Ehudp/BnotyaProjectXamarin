using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.ViewModels;

namespace WomenInBible.Services
{
    public class InsightService
    {
        public async Task<IEnumerable<Insight>> GetFavoriteInsights()
        {
            return await IoC.Resolve<DatabaseManager>().QuerySelectedAsync<Insight, string>(x => x.IsFavorite, x => x.Name);
        }
    }
}
