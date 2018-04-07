﻿using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{

    public class ClienteVehiculoRepository : RepositoryBase<ClienteVehiculo>
    {
        public ClienteVehiculoRepository(Cliente360DbContext cliente360DbContext)
            :base(cliente360DbContext)
        {

        }
    }
}
