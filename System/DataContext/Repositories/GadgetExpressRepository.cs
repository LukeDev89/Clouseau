using DataContext.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories
{
    public class GadgetExpressRepository : IGadgetExpressRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public GadgetExpressRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task Add(GadgetExpress model)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.GadgetExpresses.AddAsync(new GadgetExpress()
            {
                Title = model.Title,
                Description = model.Description,
                Message = model.Message,
                Icon = model.Icon,

                StartDay = model.StartDay,
                StartTime = model.StartTime,
                EndDay = model.EndDay,
                EndTime = model.EndTime,

                IntervalHours = model.IntervalHours,
                Executions = model.Executions,

                ActiveMonday = model.ActiveMonday,
                ActiveTuesday = model.ActiveTuesday,
                ActiveWednesday = model.ActiveWednesday,
                ActiveThursday = model.ActiveThursday,
                ActiveFriday = model.ActiveFriday,
            });

            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var gadgetExpress = await _context.GadgetExpresses.FirstAsync(x => x.Id == id);

            _context.GadgetExpresses.Remove(gadgetExpress);

            await _context.SaveChangesAsync();
        }

        public async Task<List<GadgetExpress>> GetAll()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.GadgetExpresses
                .ToListAsync();
        }

        public async Task Send(GadgetExpress model)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var gadgetExpress = await _context.GadgetExpresses.FirstAsync(x => x.Id == model.Id);
    
            gadgetExpress.Executions += 1;

            _context.Update(gadgetExpress);

            await _context.SaveChangesAsync();
        }

        public async Task Update(GadgetExpress model)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var gadgetExpress = await _context.GadgetExpresses.FirstAsync(x => x.Id == model.Id);

            gadgetExpress.Title = model.Title;
            gadgetExpress.Description = model.Description;
            gadgetExpress.Message = model.Message;
            gadgetExpress.Icon = model.Icon;

            gadgetExpress.StartDay = model.StartDay;
            gadgetExpress.StartTime = model.StartTime;
            gadgetExpress.EndDay = model.EndDay;
            gadgetExpress.EndTime = model.EndTime;

            gadgetExpress.IntervalHours = model.IntervalHours;
            gadgetExpress.Executions = model.Executions;

            gadgetExpress.ActiveMonday = model.ActiveMonday;
            gadgetExpress.ActiveTuesday = model.ActiveTuesday;
            gadgetExpress.ActiveWednesday = model.ActiveWednesday;
            gadgetExpress.ActiveThursday = model.ActiveThursday;
            gadgetExpress.ActiveFriday = model.ActiveFriday;

            gadgetExpress.NextExecution = model.NextExecution;

            _context.Update(gadgetExpress);

            await _context.SaveChangesAsync();
        }
    }
}
