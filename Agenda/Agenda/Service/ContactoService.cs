using Agenda.Entidades;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace Agenda.Service
{
    public class ContactoService
    {
        public List<Contacto> ObtenerContactos()
        {
            var contactosJson = Preferences.Get("Contactos", string.Empty);

            if (string.IsNullOrEmpty(contactosJson))
            {
                return new List<Contacto>();
            }

            return JsonConvert.DeserializeObject<List<Contacto>>(contactosJson);
        }

        public void AgregarContacto(Contacto nuevoContacto)
        {
            var contactos = ObtenerContactos();
            contactos.Add(nuevoContacto);
            GuardarContactos(contactos);
        }

        public void ActualizarContacto(Contacto contacto)
        {
            var contactos = ObtenerContactos();
            var contactoExistente = contactos.FirstOrDefault(c => c.Id == contacto.Id);

            if (contactoExistente != null)
            {
                contactoExistente.Nombre = contacto.Nombre;
                contactoExistente.NumeroTelefono = contacto.NumeroTelefono;

                GuardarContactos(contactos);
            }
        }

        public void EliminarContacto(Contacto contacto)
        {
            var contactos = ObtenerContactos();
            var contactoExistente = contactos.FirstOrDefault(c => c.Id == contacto.Id);

            if (contactoExistente != null)
            {
                contactos.Remove(contactoExistente);
                GuardarContactos(contactos);
            }
        }

        private void GuardarContactos(List<Contacto> contactos)
        {
            var contactosJson = JsonConvert.SerializeObject(contactos);
            Preferences.Set("Contactos", contactosJson);
        }
    }
}

