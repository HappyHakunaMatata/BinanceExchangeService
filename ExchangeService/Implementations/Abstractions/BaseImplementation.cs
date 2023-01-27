using ExchangeService.Models.Structures;


namespace ExchangeService.Implementations.Abstractions
{
    public abstract class BaseImplementation<Model>
    {
        protected Model? model { get; set; }


        public void InitiateClass(string id = "")
        {
            HttpResponseMessage response = Authtorisation(id);
            Deserialize(response);
        }

        public abstract HttpResponseMessage Authtorisation(string id);

        protected void Deserialize(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    model = Task.Run(async () => await response.Content.ReadFromJsonAsync<Model>()).Result;                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception in Deserialize: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Exception: " + response.StatusCode.ToString());
                throw new HttpRequestException(response.StatusCode.ToString());
            }
            
        }
    }
}
