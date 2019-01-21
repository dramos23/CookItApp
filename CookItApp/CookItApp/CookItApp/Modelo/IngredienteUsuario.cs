using CookItWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class IngredienteUsuario : IngredienteConCantidad
    {        
        public string _Email { set; get; }

        public Usuario _UserInfo { set; get; }

        [JsonIgnore]
        public string _SourceImagen {
            get
            {
                string source = "";
                int nombreId = this._Ingrediente._TipoIngrediente._Nombre;
                switch (nombreId)
                {
                    case 1:
                        source = "tipoAceite.png";
                        break;
                    case 2:
                        source = "tipoCarne.png";
                        break;
                    case 3:
                        source = "tipoCereal.png";
                        break;
                    case 4:
                        source = "tipoCrema.png";
                        break;
                    case 5:
                        source = "tipoEspecia.png";
                        break;
                    case 6:
                        source = "tipoFiambre.png";
                        break;
                    case 7:
                        source = "tipoFrutaVerdura.png";
                        break;
                    case 8:
                        source = "tipoFrutoSeco.png";
                        break;
                    case 9:
                        source = "tipoHornear.png";
                        break;
                    case 10:
                        source = "tipoLacteo.png";
                        break;
                    case 11:
                        source = "tipoPasta.png";
                        break;
                    case 12:
                        source = "tipoPescado.png";
                        break;
                    case 13:
                        source = "tipoSalsa.png";
                        break;
                }
                return source;
            }
        }
    }
}
