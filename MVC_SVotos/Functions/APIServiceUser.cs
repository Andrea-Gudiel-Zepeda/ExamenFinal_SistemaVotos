using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace MVC_Bicicletas.Functions
{
    public class APIServiceUser
    {
        private static int timeout = 30;
        private static string baseurl = "https://localhost:7180/";

        //METODOS PARA EL CRUD - GENERALES
        public static async Task<System.Net.Http.HttpResponseMessage> GetListMethod(string url, string accessToken)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + url);

            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> DeleteMethod(string url, int id, string accessToken)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.DeleteAsync(baseurl + $"{url}/{id}");
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> SetMethod(string url, string json_, string accessToken)
        {
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PostAsync(baseurl + url, content);
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> EditMethod(string url, int id, string json_, string accessToken)
        {
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PutAsync(baseurl + $"{url}/{id}", content);
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> GetByIDMethod(string url, int id, string accessToken)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + $"{url}/{id}");
            return response;
        }

        //METODOS DE LA CLASE USUARIO
        public static async System.Threading.Tasks.Task<IEnumerable<BD_Bicicletas.Voto>> UserGetList(string accessToken)
        {
            var response = await GetListMethod("User/GetList", accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<BD_Bicicletas.Voto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<BD_Bicicletas.GeneralResult> UserSet(BD_Bicicletas.Voto object_to_serialize, string accessToken)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await SetMethod("User/Set", json_, accessToken);//httpClient.PostAsync(baseurl + "User/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<BD_Bicicletas.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<BD_Bicicletas.Voto> GetUserByID(int id, string accessToken)
        {
            var response = await GetByIDMethod("User/GetByID", id, accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<BD_Bicicletas.Voto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<BD_Bicicletas.GeneralResult> UserEdit(BD_Bicicletas.Voto object_to_serialize, int id, string accessToken)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await EditMethod("User/Edit", id, json_, accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<BD_Bicicletas.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<BD_Bicicletas.GeneralResult> UserDelete(int id, string accessToken)
        {
            //var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await DeleteMethod("User/Delete", id, accessToken);//httpClient.PostAsync(baseurl + "Movies/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<BD_Bicicletas.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<BD_Bicicletas.Token> Login(BD_Bicicletas.Token object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "Login/Login", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<BD_Bicicletas.Token>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }
    }
}
