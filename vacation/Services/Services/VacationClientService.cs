using IdentityModel.Client;
using Newtonsoft.Json;
using Services.Contracts;
using Services.DataModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using vacation.core;
using Vacations.Models.Models;
using Xamarin.Essentials;

namespace Services.Services
{
    public class VacationClientService : IVacationClientService
    {
        public readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(OAuth.BaseUri)
        };

        public async Task<bool> TrySetCredentialsAsync(string login, string password)
        {
            var authClient = new TokenClient(OAuth.TokenUrl, OAuth.ClientId, OAuth.ClientSecret);

            var userTokenResponse = await authClient?.RequestResourceOwnerPasswordAsync(
                login,
                password,
                OAuth.ResourceId);

            if (!string.IsNullOrEmpty(userTokenResponse.AccessToken))
            {
                await SecureStorage.SetAsync("Login", login);
                await SecureStorage.SetAsync("Password", password);
                await SecureStorage.SetAsync("AccessToken", userTokenResponse.AccessToken);
                await SecureStorage.SetAsync("TokenType", userTokenResponse.TokenType);

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(userTokenResponse.TokenType, userTokenResponse.AccessToken);

                return true;
            }
            return false;
        }

        public async Task<List<VacationDataModel>> GetVacationsAsync()
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(OAuth.VacationsUrl)
            };

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VacationModelRoot>(result).Result;
            }

            return null;
        }

        public async Task CreateVacationsAsync(VacationModel vacationModel)
        {
            var postVacationModel = new PostVacationModel
            {
                Id = vacationModel.Id,
                Start = vacationModel.StartDate,
                End = vacationModel.EndDate,
                VacationType = vacationModel.CurrentImageOnPage + 1,
                VacationStatus = vacationModel.VacationStatus,
                Created = DateTime.Now,
                CreatedBy = vacationModel.CreatedBy
            };

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(OAuth.VacationsUrl)
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(postVacationModel), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task UpdateVacationsAsync(VacationModel vacation)
        {
            var postVacationModel = new PostVacationModel
            {
                Id = vacation.Id,
                Start = vacation.StartDate,
                End = vacation.EndDate,
                VacationType = vacation.CurrentImageOnPage + 1,
                VacationStatus = vacation.VacationStatus,
                Created = DateTime.Now,
                CreatedBy = vacation.CreatedBy
            };

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(OAuth.VacationsUrl)

            };

            request.Content = new StringContent(JsonConvert.SerializeObject(postVacationModel), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task DeleteVacationsAsync(string vacationId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(OAuth.VacationsUrl+$"/{vacationId}")
            };
            await _httpClient.SendAsync(request);
        }
    }
}

