using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;
using Practica3.Models;
namespace Practica3.Controllers
{
    public class MascotaController: Controller
    {
        private static listamascotas listaMascotasCompartida =new listamascotas();
        public IActionResult Index()
        {
            return View(listaMascotasCompartida);

        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Registrar(string nombre, double peso, string sexo, int edad, string propietario, string tipo, string raza, string tamanio, double envergadura, bool vuela, string caparazon, bool acuatica, bool esterilazado)
        {
            try
            {
                Mascota nuevaMascota =null;
                if (tipo=="perro")
                {
                    nuevaMascota=new Perro(nombre,peso,sexo,edad, propietario,raza,tamanio);
                }
                else if(tipo.ToLower()=="gato")
                {
                    nuevaMascota=new Gato(nombre, peso, sexo, edad, propietario, raza,esterilazado);

                }
                else if (tipo.ToLower()=="tortuga")
                {
                    nuevaMascota=new Tortuga(nombre,peso, sexo,edad,propietario,caparazon, acuatica);

                }
                else if(tipo.ToLower()=="ave")
                {
                    nuevaMascota=new Ave(nombre,peso, sexo, edad, propietario,envergadura,vuela);

                }

                if(nuevaMascota!= null)
                {
                    listaMascotasCompartida.agregarmascota(nuevaMascota);
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                
                ModelState.AddModelError("", "Error al registrar una mascota");
                return View();
            }
        }
        public IActionResult Detalle(string id)
        {
       if(string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        Mascota mascotaEncontrada=listaMascotasCompartida.busca(id);
        if(mascotaEncontrada==null)
            {
                return NotFound();
            }
            return View(mascotaEncontrada);
        }
        
        [HttpPost]
        public IActionResult CambiarEstado(string codigoUnico, string nuevoEstado)
        {
            Mascota mascota=listaMascotasCompartida.busca(codigoUnico);
            if(mascota!= null)
            {
                mascota.CambiarEstado(nuevoEstado);

            }
            return RedirectToAction("Detalle", new {id=codigoUnico});

        }
        [HttpPost]
        public IActionResult CalcularDosis(string codigoUnico,double dosisBase)
        {
            Mascota mascota =listaMascotasCompartida.busca(codigoUnico);
           double resultadoDosis=0;
           if(mascota!= null)
            {
                resultadoDosis=mascota.CalcularDosis(dosisBase);
                ViewBag.DosisCalculada = resultadoDosis;
                ViewBag.MascotaSeleccionada = mascota;
            }
            return View("Detalle", mascota);
        }

    }
}