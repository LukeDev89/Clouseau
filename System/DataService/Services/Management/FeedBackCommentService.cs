using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services.Management
{
    public class FeedBackCommentService : IFeedBackCommentService
    {
        private readonly IFeedBackCommentRepository _repository;

        public FeedBackCommentService(IFeedBackCommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedbackComment>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedbackComment comment) => await _repository.AddAsync(comment);

        public async Task EditAsync(FeedbackComment comment) => await _repository.EditAsync(comment);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

		public async Task DefinitiveDeleteAsync(long commentId) => await _repository.DefinitiveDeleteAsync(commentId);

	}
}
