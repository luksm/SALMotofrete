﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DAO;

namespace SALClassLib.Masterdata.Model.DAO
{
    public class GerenteDAO : DAO<Gerente>
    {
        public GerenteDAO(ISession sessao)
            : base(sessao)
        {

        }
    }
}
