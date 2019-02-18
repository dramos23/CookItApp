using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CookItApp.Models
{

    public class NotificacionAppCenter {
        [JsonIgnore]
        [JsonProperty("notification_id")]
        public string Notification_id { get; set; }
        [JsonProperty("notification_content")]
        public NotificacionContent Notificacion_content { get; set; }
        [JsonProperty("notification_target")]
        public NotificacionTarget Notification_target { get; set; }

        public NotificacionAppCenter()
        {
            Notificacion_content = new NotificacionContent();
            Notification_target = new NotificacionTarget();
        }

        public void CompletarInfo(Reto reto, Guid? uuid)
        {
            switch (reto._IdEstadoReto)
            {
                case 1:
                    Notificacion_content.Title = "CookIt!: " + reto._NomUsuOri + " te ha desafiado!!!";
                    Notificacion_content.Body = "Hola " + reto._NomUsuDes + ".\\n" + reto._NomUsuOri
                        + " te ha retado a preparar está exquicita receta: '" + reto._Receta._Titulo
                        + "'.\\nAprenda hacerla y acumulá " + reto._Puntaje + " puntos para subir de nivel.";
                    Notification_target.Devices.Add(uuid);
                    Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 2:
                    Notificacion_content.Title = "CookIt!: " + reto._NomUsuDes + " ha aceptado el desafío!!!";
                    Notificacion_content.Body = "Hola " + reto._NomUsuOri + ".\\nAcepto el desafío propuesto!!.";
                    Notification_target.Devices.Add(uuid);
                    Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 3:
                    Notificacion_content.Title = "CookIt!: " + reto._NomUsuDes + " ha pasado!";
                    Notificacion_content.Body = "Hola " + reto._NomUsuOri + ".\\nLo siento pero por está paso.";
                    Notification_target.Devices.Add(uuid);
                    Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 4:
                    Notificacion_content.Title = "CookIt!: " + reto._NomUsuDes + " ha terminado!";
                    Notificacion_content.Body = "Hola " + reto._NomUsuOri + ".\\nHe finalizado el desafío propuesto!!";
                    Notification_target.Devices.Add(uuid);
                    Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 5:
                    Notificacion_content.Title = "CookIt!: " + reto._NomUsuDes + " ha terminado!";
                    Notificacion_content.Body = "Hola " + reto._NomUsuDes + ".\\nFelicitaciones, desafío cumplido!!";
                    Notification_target.Devices.Add(uuid);
                    Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 6:
                    Notificacion_content.Title = "CookIt!: " + reto._NomUsuDes + " ha terminado!";
                    Notificacion_content.Body = "Hola " + reto._NomUsuDes + ".\\nLo sento pero no has logrado cumplir el desafío..";
                    Notification_target.Devices.Add(uuid);
                    Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
            }

            Notificacion_content.Body = (Notificacion_content.Body as string).Replace("\\n", Environment.NewLine + Environment.NewLine);            

        }



    }

    public class NotificacionContent
    {
        [JsonProperty("name")]
        public const string Name = "CookIt!";
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("custom_data")]
        public Dictionary<string, string> Custom_Data { get; set; }

        public NotificacionContent()
        {
            
            Custom_Data = new Dictionary<string, string>();
        }
    }

    public class NotificacionTarget {

        [JsonProperty("type")]
        public const string type = "devices_target";
        [JsonProperty("devices")]
        public List<System.Guid?> Devices { get; set; }

        public NotificacionTarget()
        {
            Devices = new List<Guid?>();
        }
    }
}
