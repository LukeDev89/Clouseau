using DataContext.Helper;
using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DataContext.Repositories.Management
{
    public class RagTemplatesRepository : IRagTemplatesRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public RagTemplatesRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<RagTemplate>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.RagTemplates
				.Include(x => x.RagTemplateFactors)
				.ThenInclude(x => x.Factor)
				.AsSplitQuery()
				.ToListAsync();
        }


		public async Task<List<RagFactor>> GetTemplateFactorById(long id)  
		{
			using var _context = _contextFactory.CreateDbContext();

			var templateDB = _context.RagTemplateFactors
			.Where(x=> x.TemplateId == id)
			.ToList();
			var aux = new List<RagFactor>();
			var factores = _context.RagFactors.ToList();
			foreach (var item in templateDB)
			{
				aux.Add(factores.FirstOrDefault(x => x.Id == item.FactorId));
			}
            return aux;
			
		}

		public async Task AddAsync(RagTemplate ragTemplate)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.RagTemplates.AddAsync(ragTemplate);

            await _context.SaveChangesAsync();
        }

		public async Task UpdateState(long id, string name, DateTime period) 
		{
			using var _context = _contextFactory.CreateDbContext();

			var objRagTamplate = await _context.RagTemplates.FirstAsync(x => x.Id == id);
			objRagTamplate.Name = name;
            objRagTamplate.Period = period;

			_context.RagTemplates.Update(objRagTamplate);

			await _context.SaveChangesAsync();
		}

        public async Task AddFactorsAsync(long templateId, long factorId)
        {
            using var _context = _contextFactory.CreateDbContext();
            var existInTemplate = await _context.RagTemplateFactors.AnyAsync(x => x.Id == templateId && x.FactorId == factorId);

            if (existInTemplate) return;

            await _context.RagTemplateFactors.AddAsync(new RagTemplateFactor()
            {
                TemplateId = templateId,
                FactorId = factorId,
            });

            await _context.SaveChangesAsync();

        }

		public async Task DeleteFactorAsync(long id, long factorId)
		{
			using var _context = _contextFactory.CreateDbContext();
			var factor = await _context.RagTemplateFactors.FirstAsync(x => x.TemplateId == id && x.FactorId == factorId);

			_context.RagTemplateFactors.Remove(factor);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteFactor(long id)
		{
			using var _context = _contextFactory.CreateDbContext();

			var factor = await _context.RagTemplateFactors.FirstAsync(x => x.Id == id);

			factor.Factor.Active = false;
			//team.Deleted = DateTime.Now;

			_context.RagTemplateFactors.Update(factor);

			await _context.SaveChangesAsync();
		}

        public async Task EditAsync(long id, string name, bool active)
        {
            using var _context = _contextFactory.CreateDbContext();

            var factores = await _context.RagFactors.FirstAsync(x => x.Id == id);

            factores.Id = id;
            factores.Name = name;
            factores.Active = active;

            _context.RagFactors.Update(factores);

            await _context.SaveChangesAsync();
        }
		public async Task DefinitiveDeleteAsync(long templateId)
		{
			using var _context = _contextFactory.CreateDbContext();

			var template = await _context.RagTemplates.FirstAsync(x => x.Id == templateId);

			_context.RagTemplates.Remove(template);

			await _context.SaveChangesAsync();
		}

		public async Task Execute(long templateId)
		{
            using var _context = _contextFactory.CreateDbContext();

            var template = await _context.RagTemplates
				.Include(x => x.RagTemplateFactors)
				.FirstAsync(x => x.Id == templateId);

			var projects = (await _context.Projects.ToListAsync()).Where(x => x.Active).ToList();

            var statuses = await _context.RagStatuses.ToListAsync();

            foreach (var p in projects)
			{
				foreach (var f in template.RagTemplateFactors)
				{
                    if (statuses.LastOrDefault(x => x.ProjectId == p.Id && x.FactorId == f.Id && x.TemplateId == templateId) != null) continue;

					await _context.RagStatuses.AddAsync(new RagStatus()
					{
						ProjectId = p.Id,
						FactorId = f.FactorId,
						TemplateId = f.TemplateId
					});
                }
			}

            await _context.SaveChangesAsync();
        }
    }
}
