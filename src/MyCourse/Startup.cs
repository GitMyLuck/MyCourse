using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastructure;

namespace MYCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // usare l'istanziamento dell'interfaccia AddTransient
            // crea per ogni chiamata un'istanza del servizio
            // che viene distrutta dal GarbageCollector appene non serve più
            //***       USO
            // si usa AddTransient quando i servizi sono "veloci da costruire" e quindi
            // non ci sono problemi prestazionali se ne vengono costruite più istanze.

            services.AddTransient<ICourseService, AdoNetCourseService>();
            //services.AddTransient<ICourseService, CourseService>();

            // servizio ottenuto con SQLite
            //services.AddTransient<IDBAccess,SqliteDBAccess>();

            // servizio ottenuto con SQLServer Management 
            services.AddTransient<IDBAccess,SQLServerDBAccess>();

            // usare l'istanziamento dell'interfaccia AddScoped
            // il Core crea una nuova istanza del servizio e la riutilizza finché siamo nel contesto
            // della stessa richiesta HTTP. Poi viene Distrutta dal GarbageCollector
            //***       USO
            // si usa AddScoped quando il servizio è "costoso da costruire" e perciò non vogliamo "pagare"
            // più volte i tempi di costruzione
            //services.AddScoped<ICourseService, CourseService>();

            // usare l'istanziamento dell'interfaccia AddSigleton
            // il Core crea un istanza del servizio e la inietta in tutti i componenti che ne 
            // hanno bisogno, anche in richieste HTTP diverse e concorrenti
            //***       USO
            // si usa AddSingleton quando abbiamo servizi che funzionano al di fuori della singola
            // richiesta HTTP.(Per esempio Un servizio che spedisce e-mail agli utenti una alla volta)
            //services.AddSingleton<ICourseService, CourseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            // middleware di routing invocato per Application
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //  definizione pattern per chiamare le varie action 
                //endpoints.MapControllerRoute("default", "{controller}/{action}/{id}");
                //  usiamo ora valori di default, in caso non vengano passati con il pattern
                //  il punto interrogativo ? all'interno della sezione {id} significa che il
                //  parametro è opzionale
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=index}/{id?}");

                // creiamo route controller alternativo per l'ordine di visualizzazione dei corsi
                endpoints.MapControllerRoute("orderMap", "{controller=Home}/{action=index}/{order?}/{search?}");
                
            });
        }
    }

   
}
