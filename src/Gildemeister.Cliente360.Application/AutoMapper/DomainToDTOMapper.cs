using AutoMapper;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Domain.SGAPROD;
//using Gildemeister.Cliente360.Domain.Model;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Transport.SGAPROD;

namespace Gildemeister.Cliente360.Application
{
    public class DomainToDTOMapper : Profile
    {
        public DomainToDTOMapper()
        {
            CreateMap<TablaGeneral, TablaGeneralDTO>()
                .ReverseMap();

            CreateMap<ApplicacionLlave, ApplicacionLlaveDTO>()
             .ReverseMap();

            CreateMap<Cliente, ClienteDTO>()
              .ReverseMap();

            CreateMap<Cotizacion, CotizacionDTO>()
                .ReverseMap();

            CreateMap<TestDrive, TestDriveDTO>()
                .ReverseMap();
        
            CreateMap<Venta, VentaDTO>()
                .ReverseMap();

            CreateMap<Servicio, ServicioDTO>()
                .ReverseMap();

            CreateMap<Ubigeo, UbigeoDTO>()
               .ReverseMap();

            CreateMap<Marca, MarcaDTO>()
                .ReverseMap();

            CreateMap<Modelo, ModeloDTO>()
                .ReverseMap();

            CreateMap<PersonaAsesor, PersonaAsesorDTO>()
                .ReverseMap();

            CreateMap<PuntoVenta, PuntoVentaDTO>()
                .ReverseMap();
        
        }
    }
}
