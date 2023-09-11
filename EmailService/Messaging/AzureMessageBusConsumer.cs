using Azure.Messaging.ServiceBus;
using EmailService.Model;
using EmailService.Model.Dto;
using EmailService.Services;
using Newtonsoft.Json;
using System.Text;

namespace EmailService.Messaging
{
    public class AzureMessageBusConsumer : IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly EmailSendService _emailSend;
        private readonly EmailSaveService _saveToDb;
        public AzureMessageBusConsumer(IConfiguration configuration, EmailSaveService service)
        {
            _configuration= configuration;
            ConnectionString = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
            QueueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();

            //connecting to the service bus client
            var BusClient = new ServiceBusClient(ConnectionString);
            _registrationProcessor= BusClient.CreateProcessor(QueueName);
            //email
            _emailSend = new EmailSendService(_configuration);
            _saveToDb = service;

        }
        public async Task Start()
        {
            _registrationProcessor.ProcessMessageAsync += OnRegistration;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;

            await _registrationProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _registrationProcessor.StopProcessingAsync();
            await _registrationProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnRegistration(ProcessMessageEventArgs args)
        {
          var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var userMessage = JsonConvert.DeserializeObject<UserMessage>(body);
         
            try
            {
                StringBuilder Message = new StringBuilder();
                Message.Append("<h1> Hello" + userMessage.Name + "</h1>");
                Message.Append("<p>Welcome to my SocialMedia App</p>");
                var emailLogger = new EmailLogger()
                {
                    Email = userMessage.Email,
                    Message = Message.ToString()

                };
                //await _saveToDb.SaveData(emailLogger);
                await _emailSend.SendMail(userMessage, Message.ToString());
                await args.CompleteMessageAsync(message);
            }catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }


        }

        
    }
}
