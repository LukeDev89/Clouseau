using DataContext.Interfaces;
using DataService.Interfaces.GadgetExpress;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services.GadgetExpress
{
    public class GadgetExpressService : IGadgetExpressService
    {
        public IGadgetExpressRepository _gadgetExpressRepository;

        public GadgetExpressService(IGadgetExpressRepository gadgetExpressRepository)
        {
            _gadgetExpressRepository = gadgetExpressRepository;
        }

        public async Task Add(DataContext.GadgetExpress model) => await _gadgetExpressRepository.Add(model);

        public async Task Delete(long id) => await _gadgetExpressRepository.Delete(id);

        public async Task<List<DataContext.GadgetExpress>> GetAll() => await _gadgetExpressRepository.GetAll();

        public async Task Update(DataContext.GadgetExpress model) => await _gadgetExpressRepository.Update(model);

        public async Task Send(DataContext.GadgetExpress model)
        {
            var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7163/SendMessage", new
                {
                    Title = model.Title,
                    Message = model.Message,
                    Icon = model.Icon
                });

                if (response.IsSuccessStatusCode)
                {
                    _gadgetExpressRepository.Send(model);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }
}
