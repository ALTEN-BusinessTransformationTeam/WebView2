using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactWebView2_Template.Models;


/**
* Controlador para todo lo relacionado con el control de la aplicación
*/
[ApiController]
[Route("[controller]")]
public class AppController : ControllerBase
{

    private readonly ILogger<AppController> _logger;

    public AppController(ILogger<AppController> logger)
    {
        _logger = logger;
    }

    /**
    * Función para cerrar aplicación
    */
    [HttpGet("closeapp")]
    public void CloseApp()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Application.Current.Shutdown();
        });
    }

}

