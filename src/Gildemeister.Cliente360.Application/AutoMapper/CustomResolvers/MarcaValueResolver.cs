using AutoMapper;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Application.AutoMapper.CustomResolvers
{
    class MarcaValueResolver : IValueResolver<CotizacionDTO, Cotizacion, Marca>
    {
        public Marca Resolve(CotizacionDTO source, Cotizacion destination, Marca destMember, ResolutionContext context)
        {
            return new Marca { CodigoMarca = source.CodigoMarca, NombreMarca = source.NombreMarca };
        }
    }
}
