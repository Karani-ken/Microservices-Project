using EmailService.Messaging;

namespace EmailService.Extensions
{
    public static class AzureServicesStarter
    {
        public static IAzureMessageBusConsumer ServiceBusConstumerInstance;

        public static IApplicationBuilder useAzure(this IApplicationBuilder app)
        {
            //gets an instance of the IAzureMessageBusConsumer service
            ServiceBusConstumerInstance = app.ApplicationServices.GetService<IAzureMessageBusConsumer>();

            var HostLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            HostLife.ApplicationStarted.Register(OnStart);
            HostLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
            ServiceBusConstumerInstance.Stop();
        }

        private static void OnStart()
        {
            ServiceBusConstumerInstance.Start();
        }

    }
}
