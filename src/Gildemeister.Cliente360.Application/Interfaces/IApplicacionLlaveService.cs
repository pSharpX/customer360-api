using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Application.Interfaces
{
    public interface IApplicacionLlaveService : IServiceBase<ApplicacionLlaveDTO>
    {
        ApplicacionLlaveDTO LlaveApplicacion(string llave);
    }
}
