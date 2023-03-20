/*using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MYCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Aggiorniamo un file per notificare al BrowserSync che deve aggiornare la pagina
                /*lifetime.ApplicationStarted.Register(()  =>
                {
                    
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                    
                });*/
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
                
            });
        }
    }

   
}
